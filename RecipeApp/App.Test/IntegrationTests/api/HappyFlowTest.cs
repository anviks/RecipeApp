using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Reflection;
using System.Text.Json;
using App.DAL.EF;
using App.Domain;
using App.DTO.v1_0.Identity;
using FluentAssertions;
using Helpers;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;
using v1_0 = App.DTO.v1_0;

namespace App.Test.IntegrationTests.api;

// [Collection("NonParallel")]
public class HappyFlowTest : IClassFixture<CustomWebApplicationFactory<Program>>, IDisposable
{
    private const string Email = "test-user@gmail.com";
    private const string Username = "test.user";
    private const string Password = "Test123!";
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Program> _factory;
    private readonly ITestOutputHelper _output;
    private readonly AppDbContext _dbContext;
    private readonly LoginResponse _loginData;

    public HappyFlowTest(CustomWebApplicationFactory<Program> factory, ITestOutputHelper output)
    {
        _factory = factory;
        _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
        _output = output;
        _dbContext = _factory.Services.GetRequiredService<AppDbContext>();
        
        _loginData = Login().Result;
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _loginData.JsonWebToken);
    }
    
    public void Dispose()
    {
        Logout(_loginData.RefreshToken).Wait();
        GC.SuppressFinalize(this);
    }

    [Fact]
    public async Task CreateRecipe()
    {
        // Get the current assembly
        var assembly = Assembly.GetExecutingAssembly();

        // Load the image from embedded resources
        await using Stream? stream = assembly.GetManifestResourceStream("App.Test.IntegrationTests.api.image.png");
        
        if (stream == null)
        {
            throw new InvalidOperationException($"Failed to load resource 'image.jpg' from assembly '{assembly.FullName}'");
        }

        var formData = new MultipartFormDataContent();
        formData.Add(new StreamContent(stream), "ImageFile", "image.jpg");
        formData.Add(new StringContent("Title"), "Title");
        formData.Add(new StringContent("Description"), "Description");
        formData.Add(new StringContent("Instruction 1"), "Instructions");
        formData.Add(new StringContent("Instruction 2"), "Instructions");
        formData.Add(new StringContent("Instruction 3"), "Instructions");
        formData.Add(new StringContent("30"), "PreparationTime");
        formData.Add(new StringContent("60"), "CookingTime");
        formData.Add(new StringContent("4"), "Servings");
        formData.Add(new StringContent("true"), "IsVegetarian");
        formData.Add(new StringContent("true"), "IsVegan");
        formData.Add(new StringContent("false"), "IsGlutenFree");

        // Act
        HttpResponseMessage response = await _client.PostAsync("/api/v1.0/Recipes", formData);

        // Assert
        response.EnsureSuccessStatusCode();

        var contentStr = await response.Content.ReadAsStringAsync();
        var recipeResponse = JsonSerializer.Deserialize<v1_0.RecipeResponse>(contentStr, JsonHelper.CamelCase);

        recipeResponse.Should().NotBeNull();
        recipeResponse!.Id.Should().NotBeEmpty();
        recipeResponse.Title.Should().Be("Title");
        recipeResponse.Description.Should().Be("Description");
        recipeResponse.Instructions.Should().HaveCount(3);
        recipeResponse.PreparationTime.Should().Be(30);
        recipeResponse.CookingTime.Should().Be(60);
        recipeResponse.Servings.Should().Be(4);
        recipeResponse.IsVegetarian.Should().BeTrue();
        recipeResponse.IsVegan.Should().BeTrue();
        recipeResponse.IsGlutenFree.Should().BeFalse();
        recipeResponse.ImageFileUrl.Should().NotBeNullOrEmpty();
        
        Recipe? recipeInDb = await _dbContext.Recipes
            .Include(r => r.AuthorUser)
            .FirstOrDefaultAsync(r => r.Id == recipeResponse.Id);
        
        recipeInDb.Should().NotBeNull();
        recipeInDb!.Title.Should().ContainValue("Title");
        recipeInDb.AuthorUser.Should().NotBeNull();
        recipeInDb.AuthorUser!.Id.Should().NotBeEmpty();
        recipeInDb.AuthorUser.UserName.Should().Be(Username);
        recipeInDb.CreatedAt.Should().BeCloseTo(DateTime.Now.ToUniversalTime(), TimeSpan.FromSeconds(10));
    }

    [Fact]
    public async Task UpdateRecipe()
    {
        // Arrange
        Domain.Recipe addedRecipe = AddRecipe();
        Guid recipeId = addedRecipe.Id;
        
        var formData = new MultipartFormDataContent();
        
        formData.Add(new StringContent(recipeId.ToString()), "Id");
        formData.Add(new StringContent("Updated Title"), "Title");
        formData.Add(new StringContent("Updated Description"), "Description");
        formData.Add(new StringContent("Instruction 1"), "Instructions");
        formData.Add(new StringContent("Instruction 2"), "Instructions");
        formData.Add(new StringContent("Instruction 3"), "Instructions");
        formData.Add(new StringContent("30"), "PreparationTime");
        formData.Add(new StringContent("60"), "CookingTime");
        formData.Add(new StringContent("4"), "Servings");
        
        // Act
        HttpResponseMessage response = await _client.PutAsync($"/api/v1.0/Recipes/{recipeId}", formData);
        _dbContext.ChangeTracker.Clear();
        
        // Assert
        response.EnsureSuccessStatusCode();
        
        Recipe? recipeInDb = await _dbContext.Recipes
            .Include(r => r.AuthorUser)
            .Include(r => r.UpdatingUser)
            .FirstOrDefaultAsync(r => r.Id == recipeId);
        
        recipeInDb.Should().NotBeNull();
        recipeInDb!.Id.Should().Be(recipeId);
        recipeInDb.Title.Should().ContainValue("Updated Title");
        recipeInDb.Description.Should().Be("Updated Description");
        recipeInDb.ImageFileUrl.Should().Be(addedRecipe.ImageFileUrl);
        recipeInDb.CookingTime.Should().Be(addedRecipe.CookingTime);
        recipeInDb.UpdatingUser!.UserName.Should().Be(Username);
        recipeInDb.UpdatingUserId.Should().NotBeEmpty();
        recipeInDb.UpdatedAt.Should().BeCloseTo(DateTime.Now.ToUniversalTime(), TimeSpan.FromSeconds(10));
        recipeInDb.UpdatedAt.Should().BeAfter(recipeInDb.CreatedAt);
    }
    
    [Fact]
    public async Task DeleteRecipe()
    {
        // Arrange
        Domain.Recipe addedRecipe = AddRecipe();
        Guid recipeId = addedRecipe.Id;
        
        // Act
        HttpResponseMessage response = await _client.DeleteAsync($"/api/v1.0/Recipes/{recipeId}");
        _dbContext.ChangeTracker.Clear();
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        
        Recipe? recipeInDb = await _dbContext.Recipes.FindAsync(recipeId);
        recipeInDb.Should().BeNull();
    }

    private Domain.Recipe AddRecipe()
    {
        Recipe recipe = _dbContext.Recipes.Add(new Recipe
        {
            Title = "Title",
            Description = "Description",
            ImageFileUrl = "~/uploads/images/test-image.png",
            Instructions =
            [
                "Instruction 1",
                "Instruction 2",
                "Instruction 3"
            ],
            PreparationTime = 30,
            CookingTime = 60,
            Servings = 4,
            IsVegetarian = true,
            IsVegan = true,
            IsGlutenFree = false,
            AuthorUserId = _dbContext.Users.First(u => u.UserName == Username).Id,
            CreatedAt = DateTime.Now
        }).Entity;
        
        _dbContext.SaveChanges();
        
        return recipe;
    }
    
    private async Task<LoginResponse> Register()
    {
        HttpResponseMessage response =
            await _client.PostAsJsonAsync("/api/v1.0/Account/Register",
                new RegisterRequest { Email = Email, Username = Username, Password = Password });
        response.EnsureSuccessStatusCode();
        var contentStr = await response.Content.ReadAsStringAsync();

        var registerData = JsonSerializer.Deserialize<LoginResponse>(contentStr, JsonHelper.CamelCase);

        registerData.Should().NotBeNull();
        registerData!.JsonWebToken.Should().NotBeNullOrEmpty();

        return registerData;
    }

    private async Task<LoginResponse> Login()
    {
        HttpResponseMessage response =
            await _client.PostAsJsonAsync("/api/v1.0/Account/Login",
                new LoginRequest { UsernameOrEmail = Username, Password = Password });
        response.EnsureSuccessStatusCode();
        var contentStr = await response.Content.ReadAsStringAsync();

        var loginData = JsonSerializer.Deserialize<LoginResponse>(contentStr, JsonHelper.CamelCase);

        loginData.Should().NotBeNull();
        loginData!.JsonWebToken.Should().NotBeNullOrEmpty();

        return loginData;
    }

    private async Task<LogoutResponse> Logout(string refreshToken)
    {
        HttpResponseMessage response = await _client.PostAsJsonAsync("/api/v1.0/Account/Logout", new LogoutRequest{ RefreshToken = refreshToken });
        response.EnsureSuccessStatusCode();
        var contentStr = await response.Content.ReadAsStringAsync();

        var logoutData = JsonSerializer.Deserialize<LogoutResponse>(contentStr, JsonHelper.CamelCase);

        logoutData.Should().NotBeNull();
        logoutData!.DeletedTokens.Should().Be(1);

        return logoutData;
    }

    // private void SeedData()
    // {
    //     _dbContext.Categories.AddRange(
    //         new Category
    //         {
    //             Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
    //             Name = new LangStr("Category 1", "en-GB"),
    //             Description = new LangStr("Category description 1", "en-GB")
    //         },
    //         new Category
    //         {
    //             Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
    //             Name = new LangStr("Category 2", "en-GB"),
    //             Description = new LangStr("Category description 2", "en-GB")
    //         }
    //     );
    //     
    //     _dbContext.SaveChanges();
    // }
}