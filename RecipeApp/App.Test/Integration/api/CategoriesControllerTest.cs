using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using App.DAL.EF;
using App.Domain;
using App.DTO.v1_0.Identity;
using Base.Domain;
using FluentAssertions;
using Helpers;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;
using v1_0 = App.DTO.v1_0;

namespace App.Test.Integration.api;

[Collection("NonParallel")]
public class CategoriesControllerTest : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Program> _factory;
    private readonly ITestOutputHelper _output;
    private readonly AppDbContext _dbContext;

    public CategoriesControllerTest(CustomWebApplicationFactory<Program> factory, ITestOutputHelper output)
    {
        _factory = factory;
        _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
        _output = output;
        _dbContext = _factory.Services.GetRequiredService<AppDbContext>();
    }

    [Fact]
    public async Task IndexDoesNotRequireAuthorization()
    {
        // Act
        HttpResponseMessage response = await _client.GetAsync("/api/v1.0/Categories");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var contentStr = await response.Content.ReadAsStringAsync();
        var categories = JsonSerializer.Deserialize<v1_0.Category[]>(contentStr, JsonHelper.CamelCase);
        categories.Should().NotBeNull();
        categories.Should().HaveCount(2);
        categories![0].Name.Should().Be("Category 1");
        categories[1].Name.Should().Be("Category 2");
    }

    [Fact]
    public async Task DetailsDoesNotRequireAuthorization()
    {
        // Act
        HttpResponseMessage response =
            await _client.GetAsync("/api/v1.0/Categories/00000000-0000-0000-0000-000000000001");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var contentStr = await response.Content.ReadAsStringAsync();
        var category = JsonSerializer.Deserialize<v1_0.Category>(contentStr, JsonHelper.CamelCase);
        category.Should().NotBeNull();
        category!.Name.Should().Be("Category 1");
    }

    [Fact]
    public async Task DetailsNotFound()
    {
        // Act
        HttpResponseMessage response =
            await _client.GetAsync("/api/v1.0/Categories/10000000-0000-0000-0000-000000000000");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CreateCategoryRequiresAuthorization()
    {
        // Act
        HttpResponseMessage response = await _client.PostAsJsonAsync("/api/v1.0/Categories", new v1_0.Category
        {
            Name = "Category 3",
            Description = "Category description 3"
        });

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task CreateCategory()
    {
        // Arrange
        LoginResponse loginData = await Login();
        var newCategory = new v1_0.Category
        {
            Name = "Category 3",
            Description = "Category description 3"
        };

        // Act
        var msg = new HttpRequestMessage(HttpMethod.Post, "/api/v1.0/Categories");
        msg.Headers.Authorization = new AuthenticationHeaderValue("Bearer", loginData.JsonWebToken);
        msg.Content = JsonContent.Create(newCategory);

        HttpResponseMessage response = await _client.SendAsync(msg);
        response.EnsureSuccessStatusCode();

        var contentStr = await response.Content.ReadAsStringAsync();
        var responseCategory = JsonSerializer.Deserialize<v1_0.Category>(contentStr, JsonHelper.CamelCase);

        // Assert
        responseCategory.Should().NotBeNull();
        Category? categoryFromDb = await _dbContext.Categories.FindAsync(responseCategory!.Id);
        categoryFromDb.Should().NotBeNull();
        categoryFromDb!.Name.Values.Should().Contain(newCategory.Name);
        categoryFromDb.Description!.Values.Should().Contain(newCategory.Description);
    }
    
    [Fact]
    public async Task UpdateCategoryRequiresAuthorization()
    {
        // Arrange
        Guid categoryId = Guid.Parse("00000000-0000-0000-0000-000000000001");
        
        // Act
        HttpResponseMessage response = await _client.PutAsJsonAsync($"/api/v1.0/Categories/{categoryId}", new v1_0.Category
        {
            Id = categoryId,
            Name = "Category 1",
            Description = "Category description 1"
        });

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
    
    [Fact]
    public async Task UpdateCategory()
    {
        // Arrange
        LoginResponse loginData = await Login();
        var updatedCategory = new v1_0.Category
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
            Name = "Category 1 updated",
            Description = "Category description 1 updated"
        };

        // Act
        var msg = new HttpRequestMessage(HttpMethod.Put, $"/api/v1.0/Categories/{updatedCategory.Id}");
        msg.Headers.Authorization = new AuthenticationHeaderValue("Bearer", loginData.JsonWebToken);
        msg.Content = JsonContent.Create(updatedCategory);
        HttpResponseMessage response = await _client.SendAsync(msg);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        Category? categoryFromDb = await _dbContext.Categories.FindAsync(updatedCategory!.Id);
        categoryFromDb.Should().NotBeNull();
        categoryFromDb!.Name.Values.Should().Contain(updatedCategory.Name);
        categoryFromDb.Description!.Values.Should().Contain(updatedCategory.Description);
    }
    
    [Fact]
    public async Task DeleteCategoryRequiresAuthorization()
    {
        // Act
        HttpResponseMessage response = await _client.DeleteAsync("/api/v1.0/Categories/00000000-0000-0000-0000-000000000001");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
    
    [Fact]
    public async Task DeleteCategory()
    {
        var transaction = await _dbContext.Database.BeginTransactionAsync();
        
        // Arrange
        LoginResponse loginData = await Login();
        Guid categoryId = Guid.Parse("00000000-0000-0000-0000-000000000001");

        // Act
        var msg = new HttpRequestMessage(HttpMethod.Delete, $"/api/v1.0/Categories/{categoryId}");
        msg.Headers.Authorization = new AuthenticationHeaderValue("Bearer", loginData.JsonWebToken);
        
        HttpResponseMessage response = await _client.SendAsync(msg);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        Category? categoryFromDb = await _dbContext.Categories.FindAsync(categoryId);
        categoryFromDb.Should().BeNull();
        
        await transaction.RollbackAsync();
    }

    private async Task<LoginResponse> Login()
    {
        const string user = "admin";
        const string pass = "asdasd";

        HttpResponseMessage response =
            await _client.PostAsJsonAsync("/api/v1.0/Account/Login",
                new LoginRequest { UsernameOrEmail = user, Password = pass });
        response.EnsureSuccessStatusCode();
        var contentStr = await response.Content.ReadAsStringAsync();

        var loginData = JsonSerializer.Deserialize<LoginResponse>(contentStr, JsonHelper.CamelCase);

        loginData.Should().NotBeNull();
        loginData!.JsonWebToken.Should().NotBeNullOrEmpty();

        return loginData;
    }
}