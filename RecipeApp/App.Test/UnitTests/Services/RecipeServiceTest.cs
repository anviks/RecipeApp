using System.ComponentModel.DataAnnotations;
using System.Reflection;
using App.BLL.DTO;
using App.BLL.Exceptions;
using App.BLL.Services;
using App.DAL.EF;
using App.DAL.EF.Repositories;
using FluentAssertions;
using Helpers;
using Microsoft.AspNetCore.Http;
using NSubstitute;

// using Telerik.JustMock;

namespace App.Test.UnitTests.Services;

public class RecipeServiceTest(TestDatabaseFixture fixture) : IClassFixture<TestDatabaseFixture>, IDisposable
{
    private readonly IFormFile _formFileMock = Substitute.For<IFormFile>();

    public void Dispose()
    {
        DeleteUploadDirectory(TestDatabaseFixture.WebRootPath);
        GC.SuppressFinalize(this);
    }

    [Fact]
    public async Task AddAsync_ShouldThrowError_WhenImageIsNull()
    {
        // Arrange
        (AppDbContext context, RecipeService? service) = SetupDependencies();
        var recipeRequest = new RecipeRequest
        {
            Title = "Test Recipe",
            Description = "Test Description",
            ImageFile = null
        };

        // Act
        Func<Task> action = async () =>
            await service.AddAsync(recipeRequest, TestDatabaseFixture.UserId, TestDatabaseFixture.WebRootPath);

        // Assert
        await action.Should().ThrowAsync<MissingImageException>();
        context.Recipes.Should().BeEmpty();
    }

    [Fact]
    public async Task AddAsync_ShouldAddRecipe_WhenImageIsNotNull()
    {
        // Arrange
        (AppDbContext context, RecipeService? service) = SetupDependencies();
        MockFileUpload("non-existing-image.jpg");

        // Act
        RecipeResponse addedRecipe = await service.AddAsync(new RecipeRequest
        {
            Title = "Test Recipe",
            Description = "Test Description",
            ImageFile = _formFileMock
        }, TestDatabaseFixture.UserId, TestDatabaseFixture.WebRootPath);

        // Assert
        Domain.Recipe? addedRecipeInDb = await context.Recipes.FindAsync(addedRecipe.Id);
        addedRecipeInDb.Should().NotBeNull();
        addedRecipe.Should().NotBeNull();
        addedRecipe.Id.Should().Be(addedRecipeInDb!.Id).And.NotBe(Guid.Empty);
        addedRecipe.Title.Should().Be(addedRecipeInDb.Title).And.Be("Test Recipe");
        addedRecipe.Description.Should().Be(addedRecipeInDb.Description).And.Be("Test Description");
        addedRecipe.ImageFileUrl.Should().Be(addedRecipeInDb.ImageFileUrl).And.NotBeNullOrEmpty();

        // Clean up
        DeleteUploadDirectory(TestDatabaseFixture.WebRootPath);
    }

    [Fact]
    public async Task UpdateAsync_ShouldKeepOldImage_WhenImageIsNull()
    {
        // Arrange
        (AppDbContext context, RecipeService? service) = SetupDependencies();
        Domain.Recipe addedRecipe = await AddRecipe(context, "non-existing-image.jpg");

        // Act
        RecipeResponse updatedRecipe = await service.UpdateAsync(new RecipeRequest
        {
            Id = addedRecipe.Id,
            Title = "Updated Test Recipe",
            Description = "Updated Test Description",
            Instructions = ["Updated Test Instruction 1", "Updated Test Instruction 2"],
            ImageFile = null
        }, TestDatabaseFixture.UserId, TestDatabaseFixture.WebRootPath);

        // Assert
        Domain.Recipe? updatedRecipeInDb = await context.Recipes.FindAsync(addedRecipe.Id);
        updatedRecipeInDb.Should().NotBeNull();
        updatedRecipe.Should().NotBeNull();
        updatedRecipe.Id.Should().Be(updatedRecipeInDb!.Id).And.Be(addedRecipe.Id);
        updatedRecipe.Title.Should().Be(updatedRecipeInDb.Title).And.Be("Updated Test Recipe");
        updatedRecipe.Description.Should().Be(updatedRecipeInDb.Description).And.Be("Updated Test Description");
        updatedRecipe.ImageFileUrl.Should().Be(updatedRecipeInDb.ImageFileUrl).And.Be(addedRecipe.ImageFileUrl);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateRecipe_WhenImageIsNotNull()
    {
        // Arrange
        (AppDbContext context, RecipeService? service) = SetupDependencies();
        MockFileUpload("non-existing-image-2.jpg");
        Domain.Recipe addedRecipe = await AddRecipe(context, "non-existing-image.jpg");

        // Act
        RecipeResponse updatedRecipe = await service.UpdateAsync(new RecipeRequest
        {
            Id = addedRecipe.Id,
            Title = "Updated Test Recipe",
            Description = "Updated Test Description",
            ImageFile = _formFileMock
        }, TestDatabaseFixture.UserId, TestDatabaseFixture.WebRootPath);

        // Assert
        Domain.Recipe? updatedRecipeInDb = await context.Recipes.FindAsync(addedRecipe.Id);
        updatedRecipeInDb.Should().NotBeNull();
        updatedRecipe.Should().NotBeNull();
        updatedRecipe.Id.Should().Be(updatedRecipeInDb!.Id).And.Be(addedRecipe.Id);
        updatedRecipe.Title.Should().Be(updatedRecipeInDb.Title).And.Be("Updated Test Recipe");
        updatedRecipe.Description.Should().Be(updatedRecipeInDb.Description).And.Be("Updated Test Description");
        updatedRecipe.ImageFileUrl.Should().Be(updatedRecipeInDb.ImageFileUrl).And.NotBe(addedRecipe.ImageFileUrl);

        // Clean up
        DeleteUploadDirectory(TestDatabaseFixture.WebRootPath);
    }

    [Fact]
    public async Task RemoveAsync_ShouldReturnZero_WhenRecipeDoesNotExist()
    {
        // Arrange
        (_, RecipeService? service) = SetupDependencies();

        // Act
        var removedCount = await service.RemoveAsync(Guid.NewGuid(), TestDatabaseFixture.WebRootPath);

        // Assert
        removedCount.Should().Be(0);
    }

    [Fact]
    public async Task RemoveAsync_ShouldRemoveRecipe_WhenRecipeExists()
    {
        // Arrange
        (AppDbContext context, RecipeService? service) = SetupDependencies();
        Domain.Recipe addedRecipe = await AddRecipe(context, "existing-image.jpg", createActualFile: true);

        // Act
        var removedCount = await service.RemoveAsync(addedRecipe.Id, TestDatabaseFixture.WebRootPath);
        await context.SaveChangesAsync();

        // Assert
        removedCount.Should().Be(1);
        Domain.Recipe? removedRecipe = await context.Recipes.FindAsync(addedRecipe.Id);
        removedRecipe.Should().BeNull();
    }

    private static async Task<Domain.Recipe> AddRecipe(AppDbContext context, string imageFileName,
        bool createActualFile = false)
    {
        // TODO: Find a better way to join paths
        var imageFileUrl = string.Join('/', TestDatabaseFixture.WebRootPath, string.Join('/', GetUploadPath()),
            imageFileName);
        if (createActualFile)
        {
            var uploadDirectory = CreateUploadDirectory(TestDatabaseFixture.WebRootPath);
            File.Create(Path.Combine(uploadDirectory, imageFileName)).Close();
        }
        Domain.Recipe addedRecipe = context.Recipes.Add(new Domain.Recipe
        {
            Id = Guid.NewGuid(),
            Title = "Test Recipe",
            Description = "Test Description",
            Instructions = ["Test Instruction 1", "Test Instruction 2"],
            ImageFileUrl = imageFileUrl,
            AuthorUserId = TestDatabaseFixture.UserId,
            CreatedAt = DateTime.Now
        }).Entity;
        await context.SaveChangesAsync();
        context.ChangeTracker.Clear();

        return addedRecipe;
    }

    private (AppDbContext, RecipeService) SetupDependencies()
    {
        AppDbContext context = fixture.CreateContext();
        var repository = new RecipeRepository(context, fixture.Mapper);
        var service = new RecipeService(repository, fixture.Mapper);
        context.Database.BeginTransaction();

        return (context, service);
    }

    private void MockFileUpload(string fakeFileName)
    {
        _formFileMock.FileName.Returns(fakeFileName);
        // Mock the CopyToAsync method to do nothing
        _formFileMock.When(f => f.CopyToAsync(Arg.Any<Stream>()))
            .Do(_ => { });
    }

    private static void DeleteUploadDirectory(string webRootPath)
    {
        var uploadPath = GetUploadPath();
        var directoryInfo = new DirectoryInfo(Path.Combine(webRootPath, uploadPath[0]));

        if (directoryInfo.Exists)
        {
            directoryInfo.Delete(true);
        }
    }

    private static string CreateUploadDirectory(string webRootPath)
    {
        var uploadPath = Path.Combine(webRootPath, Path.Combine(GetUploadPath()));
        var directoryInfo = new DirectoryInfo(uploadPath);

        if (!directoryInfo.Exists)
        {
            directoryInfo.Create();
        }

        return uploadPath;
    }

    private static string[] GetUploadPath()
    {
        FieldInfo? fieldInfo =
            typeof(RecipeService).GetField("UploadPathFromWebroot", BindingFlags.NonPublic | BindingFlags.Static);
        var uploadPath = (string[])fieldInfo!.GetValue(null)!;

        return uploadPath;
    }
}