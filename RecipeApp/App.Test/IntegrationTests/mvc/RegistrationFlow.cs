using System.Net;
using AngleSharp.Html.Dom;
using Helpers;
using Microsoft.AspNetCore.Mvc.Testing;

namespace App.Test.IntegrationTests.mvc;

[Collection("NonParallel")]
public class RegistrationFlow: IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Program> _factory;
    
    public RegistrationFlow(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }
    
    [Fact]
    public async Task RegisterUserAsync()
    {
        // ARRANGE
        const string registerUri = "/Identity/Account/Register";
        // ACT
        // this just gets the headers, body can be xxx length and is loaded later
        HttpResponseMessage getRegisterResponse = await _client.GetAsync(registerUri);
        // ASSERT
        getRegisterResponse.EnsureSuccessStatusCode();


        // ARRANGE
        // get the actual content from response
        IHtmlDocument getRegisterContent = await HtmlHelper.GetDocumentAsync(getRegisterResponse);


        // get the form element from page content
        var formRegister = (IHtmlFormElement) getRegisterContent.QuerySelector("#registerForm")!;
        // set up the form values - username, pwd, etc
        var formRegisterValues = new Dictionary<string, string>
        {
            ["Input_Username"] = "Username",
            ["Input_Email"] = "test@test.ee",
            ["Input_Password"] = "Foo.bar1",
            ["Input_ConfirmPassword"] = "Foo.bar1",
        };

        // ACT
        // send form with data to server, method (POST) is detected from form element
        HttpResponseMessage postRegisterResponse = await _client.SendAsync(formRegister, formRegisterValues);

        // ASSERT
        // found - 302 - ie user was created and we should redirect
        // https://en.wikipedia.org/wiki/HTTP_302
        Assert.Equal(HttpStatusCode.Found, postRegisterResponse.StatusCode);
        
        const string protectedUri = "/Contests";
        
        HttpResponseMessage getResponse = await _client.GetAsync(protectedUri);
        getResponse.EnsureSuccessStatusCode();
    }
    
    [Fact]
    public async Task LoadProtectedPageRedirects()
    {
        const string protectedUri = "/Contests";
        
        var getResponse = await _client.GetAsync(protectedUri);

        Assert.Equal(HttpStatusCode.Found, getResponse.StatusCode);
    }
}