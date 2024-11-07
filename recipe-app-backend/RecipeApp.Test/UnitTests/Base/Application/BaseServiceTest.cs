using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Test.UnitTests.Base.Infrastructure;

namespace RecipeApp.Test.UnitTests.Base.Application;

public class BaseServiceTest
{
    private readonly TestDbContext _ctx;
    private readonly TestEntityService _testEntityService;
    private static readonly Random Random = new();

    public BaseServiceTest()
    {
        // set up mock database - inmemory
        var optionsBuilder = new DbContextOptionsBuilder<TestDbContext>();

        // use random guid as db instance id
        optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
        _ctx = new TestDbContext(optionsBuilder.Options);

        // reset db
        _ctx.Database.EnsureDeleted();
        _ctx.Database.EnsureCreated();

        var config = new MapperConfiguration(cfg => cfg.CreateMap<TestEntity, TestEntity>());
        IMapper? mapper = config.CreateMapper();

        _testEntityService = new TestEntityService(_ctx, mapper);
    }
    
        [Fact]
    public async Task Add_ShouldAddEntity()
    {
        // Arrange
        TestEntity entity = CreateRandomEntity();

        // Act
        TestEntity addedEntity = await _testEntityService.AddAsync(entity);
        await _ctx.SaveChangesAsync();
        TestEntity? entityInDb = await _testEntityService.GetByIdAsync(addedEntity.Id);

        // Assert
        addedEntity.Should().NotBeNull();
        addedEntity.Id.Should().NotBeEmpty();
        addedEntity.Value.Should().Be(entity.Value);

        entityInDb.Should().NotBeNull();
        entityInDb!.Id.Should().Be(addedEntity.Id);
        entityInDb.Value.Should().Be(entity.Value);
    }

    [Fact]
    public async Task Update_ShouldUpdateEntity()
    {
        // Arrange
        TestEntity entity = await AddRandomEntity();
        _ctx.ChangeTracker.Clear();

        // Act
        entity.Value = "Quuz";
        TestEntity updatedEntity = await _testEntityService.UpdateAsync(entity);
        await _ctx.SaveChangesAsync();
        TestEntity? entityInDb = await _testEntityService.GetByIdAsync(updatedEntity.Id);

        // Assert
        updatedEntity.Should().NotBeNull();
        updatedEntity.Id.Should().Be(entity.Id);
        updatedEntity.Value.Should().Be("Quuz");

        entityInDb.Should().NotBeNull();
        entityInDb!.Id.Should().Be(entity.Id);
        entityInDb.Value.Should().Be("Quuz");
    }

    [Fact]
    public async Task RemoveAsync_ShouldRemoveEntity()
    {
        // Arrange
        TestEntity entity = await AddRandomEntity();
        await AddRandomEntity();

        // Act
        await _testEntityService.DeleteAsync(entity);
        await _ctx.SaveChangesAsync();

        // Assert
        _ctx.TestEntities.Should().HaveCount(1);
    }

    [Fact]
    public async Task FindAsync_ShouldReturnEntity()
    {
        // Arrange
        TestEntity entity = await AddRandomEntity();

        // Act
        TestEntity? data = await _testEntityService.GetByIdAsync(entity.Id);

        // Assert
        data.Should().NotBeNull();
        data!.Value.Should().Be(entity.Value);
    }

    [Fact]
    public async Task FindAllAsync_ShouldReturnAllEntities()
    {
        // Arrange
        TestEntity entity1 = await AddRandomEntity();
        TestEntity entity2 = await AddRandomEntity();

        // Act
        var data = (await _testEntityService.GetAllAsync()).ToList();

        // Assert
        data.Should().HaveCount(2);
        data.Should().ContainSingle(e => e.Value == entity1.Value);
        data.Should().ContainSingle(e => e.Value == entity2.Value);
    }

    [Fact]
    public async Task ExistsAsync_ShouldReturnTrue()
    {
        // Arrange
        TestEntity entity = await AddRandomEntity();

        // Act
        var exists = await _testEntityService.ExistsAsync(entity.Id);

        // Assert
        exists.Should().BeTrue();
    }

    private async Task<TestEntity> AddRandomEntity()
    {
        TestEntity entity = CreateRandomEntity();
        _ctx.TestEntities.Add(entity);
        await _ctx.SaveChangesAsync();

        return entity;
    }

    private static TestEntity CreateRandomEntity()
    {
        return new TestEntity { Value = RandomString(10) };
    }

    private static string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[Random.Next(s.Length)]).ToArray());
    }
}