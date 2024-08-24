using System.Net;
using System.Reflection;
using AngleSharp.Html.Dom;
using FluentAssertions;
using Helpers;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using RecipeApp.Infrastructure.Data.EntityFramework;
using RecipeApp.Infrastructure.Data.EntityFramework.Entities;
using Xunit.Abstractions;

namespace RecipeApp.Test.IntegrationTests.Mvc;

[Collection("NonParallel")]
public class HappyFlowTest : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private const string RegisterUri = "/Identity/Account/Register";
    private const string LoginUri = "/Identity/Account/Login";
    private const string ProtectedUri = "/Recipes/Create";
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Program> _factory;
    private readonly ITestOutputHelper _output;
    private readonly AppDbContext _context;

    public HappyFlowTest(CustomWebApplicationFactory<Program> factory, ITestOutputHelper output)
    {
        _factory = factory;
        _output = output;
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
        _context = factory.Services.GetRequiredService<AppDbContext>();
    }

    [Fact]
    public async Task RegisterUserAsync()
    {
        // Arrange
        HttpResponseMessage getRegisterResponse = await _client.GetAsync(RegisterUri);
        getRegisterResponse.EnsureSuccessStatusCode();
        // Get the actual content from response
        IHtmlDocument registerPageContent = await HtmlHelper.GetDocumentAsync(getRegisterResponse);
        // get the form element from page content
        var formRegister = (IHtmlFormElement)registerPageContent.QuerySelector("#registerForm")!;
        // set up the form values - username, pwd, etc
        var formRegisterValues = new Dictionary<string, string>
        {
            ["Input_Username"] = "Username",
            ["Input_Email"] = "test@test.ee",
            ["Input_Password"] = "Foo.bar1",
            ["Input_ConfirmPassword"] = "Foo.bar1",
        };

        // Act
        // Send form with data to server, method (POST) is detected from form element
        HttpResponseMessage postRegisterResponse = await _client.SendAsync(formRegister, formRegisterValues);

        // Assert
        // Found - 302 - ie user was created and we should redirect
        // https://en.wikipedia.org/wiki/HTTP_302
        postRegisterResponse.StatusCode.Should().Be(HttpStatusCode.Found);

        // Should have access to a protected page
        HttpResponseMessage getResponse = await _client.GetAsync(ProtectedUri);
        getResponse.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task LoadProtectedPageRedirects()
    {
        // Act
        HttpResponseMessage getResponse = await _client.GetAsync(ProtectedUri);

        // Assert
        getResponse.StatusCode.Should().Be(HttpStatusCode.Found);
    }

    [Fact]
    public async Task LoginUserAsync_UsingEmail()
    {
        // Act
        HttpResponseMessage postLoginResponse = await PerformLoginAsync(_factory.GetEmail, _factory.GetPassword);

        // Assert
        postLoginResponse.StatusCode.Should().Be(HttpStatusCode.Found);

        HttpResponseMessage getResponse = await _client.GetAsync(ProtectedUri);
        getResponse.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task LoginUserAsync_UsingUsername()
    {
        // Act
        HttpResponseMessage postLoginResponse = await PerformLoginAsync(_factory.GetUsername, _factory.GetPassword);

        // Assert
        postLoginResponse.StatusCode.Should().Be(HttpStatusCode.Found);

        HttpResponseMessage getResponse = await _client.GetAsync(ProtectedUri);
        getResponse.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task LogoutUserAsync()
    {
        // Arrange
        await PerformLoginAsync(_factory.GetUsername, _factory.GetPassword);
        HttpResponseMessage homePageResponse = await _client.GetAsync("/");
        IHtmlDocument pageContent = await HtmlHelper.GetDocumentAsync(homePageResponse);
        var logoutForm = (IHtmlFormElement)pageContent.QuerySelector("#logoutForm")!;

        // Act
        HttpResponseMessage getLogoutResponse = await _client.SendAsync(logoutForm, new Dictionary<string, string>());

        // Assert
        getLogoutResponse.StatusCode.Should().Be(HttpStatusCode.Found);

        HttpResponseMessage getResponse = await _client.GetAsync(ProtectedUri);
        getResponse.StatusCode.Should().Be(HttpStatusCode.Found);
    }

    [Fact]
    public async Task CreateRecipeAsync()
    {
        // Arrange
        var result = await PerformLoginAsync(_factory.GetUsername, _factory.GetPassword);
        _output.WriteLine(result.StatusCode.ToString());

        HttpResponseMessage getResponse = await _client.GetAsync(ProtectedUri);
        IHtmlDocument pageContent = await HtmlHelper.GetDocumentAsync(getResponse);
        var formRecipe = (IHtmlFormElement)pageContent.QuerySelector("#recipeForm")!;

        var assembly = Assembly.GetExecutingAssembly();
        await using Stream stream = assembly.GetManifestResourceStream("RecipeApp.Test.IntegrationTests.image.png")!;

        var formRecipeValues = new Dictionary<string, string>
        {
            ["Title"] = "Test Recipe",
            ["Description"] = "Test Description",
            ["ImageFile"] = "image.png",
            ["PreparationTime"] = "10",
            ["CookingTime"] = "20",
            ["Servings"] = "4",
            ["IsVegetarian"] = "true",
            ["Instructions[0]"] = "Step 1",
            ["Instructions[1]"] = "Step 2"
        };

        // Act
        HttpResponseMessage postRecipeResponse =
            await _client.SendAsync(formRecipe, formRecipeValues, isMultipart: true, stream);

        // Assert
        postRecipeResponse.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task UpdateRecipeAsync()
    {
        // Arrange
        _context.Recipes.Add(new Recipe
        {
            Title = "Test Recipe",
            Description = "Test Description",
            ImageFileUrl = "image.png",
            PreparationTime = 10,
            CookingTime = 20,
            Servings = 4,
            IsVegetarian = true,
            Instructions = ["Step 1", "Step 2"],
            AuthorUserId = _factory.GetUserId,
            CreatedAt = DateTime.Now.ToUniversalTime()
        });
        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();

        var result = await PerformLoginAsync(_factory.GetUsername, _factory.GetPassword);

        HttpResponseMessage getResponse = await _client.GetAsync(ProtectedUri);
        IHtmlDocument pageContent = await HtmlHelper.GetDocumentAsync(getResponse);
        var formRecipe = (IHtmlFormElement)pageContent.QuerySelector("#recipeForm")!;

        var formRecipeValues = new Dictionary<string, string>
        {
            ["Title"] = "Updated Recipe",
            ["Description"] = "Updated Description",
            ["PreparationTime"] = "15",
            ["CookingTime"] = "25",
            ["Servings"] = "6",
            ["IsVegetarian"] = "false",
            ["Instructions[0]"] = "Step 1",
            ["Instructions[1]"] = "Step 2",
            ["Instructions[2]"] = "Step 3"
        };
        
        // Act
        HttpResponseMessage postRecipeResponse = await _client.SendAsync(formRecipe, formRecipeValues);
        
        // Assert
        postRecipeResponse.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    private async Task<HttpResponseMessage> PerformLoginAsync(string usernameOrEmail, string password)
    {
        HttpResponseMessage getLoginResponse = await _client.GetAsync(LoginUri);
        getLoginResponse.EnsureSuccessStatusCode();
        IHtmlDocument loginPageContent = await HtmlHelper.GetDocumentAsync(getLoginResponse);
        var formLogin = (IHtmlFormElement)loginPageContent.QuerySelector("#account")!;
        var formLoginValues = new Dictionary<string, string>
        {
            ["Input_UsernameOrEmail"] = usernameOrEmail,
            ["Input_Password"] = password,
        };
        HttpResponseMessage postLoginResponse = await _client.SendAsync(formLogin, formLoginValues);

        return postLoginResponse;
    }
}