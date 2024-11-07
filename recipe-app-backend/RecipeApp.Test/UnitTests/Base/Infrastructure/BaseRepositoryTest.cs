using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace RecipeApp.Test.UnitTests.Base.Infrastructure;

public class BaseRepositoryTest
{
    private readonly TestDbContext _ctx;
    private readonly TestEntityRepository _testEntityRepository;
    private static readonly Random Random = new();

    public BaseRepositoryTest()
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

        _testEntityRepository = new TestEntityRepository(_ctx, mapper);
    }

    [Fact]
    public async Task Add_ShouldAddEntity()
    {
        // Arrange
        TestEntity entity = CreateRandomEntity();

        // Act
        TestEntity addedEntity = await _testEntityRepository.AddAsync(entity);
        await _ctx.SaveChangesAsync();
        TestEntity? entityInDb = await _testEntityRepository.GetByIdAsync(addedEntity.Id);

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
        TestEntity updatedEntity = await _testEntityRepository.UpdateAsync(entity);
        await _ctx.SaveChangesAsync();
        TestEntity? entityInDb = await _testEntityRepository.GetByIdAsync(updatedEntity.Id);

        // Assert
        updatedEntity.Should().NotBeNull();
        updatedEntity.Id.Should().Be(entity.Id);
        updatedEntity.Value.Should().Be("Quuz");

        entityInDb.Should().NotBeNull();
        entityInDb!.Id.Should().Be(entity.Id);
        entityInDb.Value.Should().Be("Quuz");
    }

    [Fact]
    public async Task Remove_ShouldRemoveEntity()
    {
        // Arrange
        TestEntity entity = await AddRandomEntity();
        TestEntity entity2 = await AddRandomEntity();

        // Act
        _testEntityRepository.DeleteAsync(entity);
        _testEntityRepository.DeleteAsync(entity2);
        await _ctx.SaveChangesAsync();

        // Assert
        _ctx.TestEntities.Should().HaveCount(0);
    }

    [Fact]
    public async Task Find_ShouldReturnEntity()
    {
        // Arrange
        TestEntity entity = await AddRandomEntity();

        // Act
        TestEntity? data = await _testEntityRepository.GetByIdAsync(entity.Id);

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
        var data = (await _testEntityRepository.GetAllAsync()).ToList();

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
        var exists = await _testEntityRepository.ExistsAsync(entity.Id);

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