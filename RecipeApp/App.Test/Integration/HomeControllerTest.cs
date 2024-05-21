using Microsoft.AspNetCore.Mvc.Testing;

namespace App.Test.Integration;

[Collection("NonParallel")]
public class HomeControllerTest : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Program> _factory;
    
    public HomeControllerTest(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }
    
    [Fact]
    public async Task Index()
    {
        // Act
        HttpResponseMessage response = await _client.GetAsync("/");

        // Assert
        response.EnsureSuccessStatusCode();
    }
}