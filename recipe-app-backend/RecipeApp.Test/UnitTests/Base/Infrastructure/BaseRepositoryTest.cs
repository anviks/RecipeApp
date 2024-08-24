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
        TestEntity addedEntity = _testEntityRepository.Add(entity);
        await _ctx.SaveChangesAsync();
        TestEntity? entityInDb = await _testEntityRepository.FindAsync(addedEntity.Id);

        // Assert
        addedEntity.Should().NotBeNull();
        addedEntity.Id.Should().NotBeEmpty();
        addedEntity.Value.Should().Be(entity.Value);

        entityInDb.Should().NotBeNull();
        entityInDb!.Id.Should().Be(addedEntity.Id);
        entityInDb.Value.Should().Be(entity.Value);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(5)]
    public async Task AddRange_ShouldAddEntities(int count)
    {
        // Arrange
        var entities = Enumerable.Range(0, count).Select(_ => CreateRandomEntity()).ToList();

        // Act
        _testEntityRepository.AddRange(entities);
        await _ctx.SaveChangesAsync();
        var entitiesInDb = _ctx.TestEntities.ToList();

        // Assert
        entitiesInDb.Should().HaveCount(count);
        entitiesInDb.Should().OnlyContain(e => entities.Any(e2 => e2.Value == e.Value));
    }

    [Fact]
    public async Task Update_ShouldUpdateEntity()
    {
        // Arrange
        TestEntity entity = await AddRandomEntity();
        _ctx.ChangeTracker.Clear();

        // Act
        entity.Value = "Quuz";
        TestEntity updatedEntity = _testEntityRepository.Update(entity);
        await _ctx.SaveChangesAsync();
        TestEntity? entityInDb = await _testEntityRepository.FindAsync(updatedEntity.Id);

        // Assert
        updatedEntity.Should().NotBeNull();
        updatedEntity.Id.Should().Be(entity.Id);
        updatedEntity.Value.Should().Be("Quuz");

        entityInDb.Should().NotBeNull();
        entityInDb!.Id.Should().Be(entity.Id);
        entityInDb.Value.Should().Be("Quuz");
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(5)]
    public async Task UpdateRange_ShouldUpdateEntities(int count)
    {
        // Arrange
        var entities = Enumerable.Range(0, count).Select(_ => CreateRandomEntity()).ToList();
        _ctx.TestEntities.AddRange(entities);
        await _ctx.SaveChangesAsync();
        _ctx.ChangeTracker.Clear();

        // Act
        entities.ForEach(e => e.Value = "Quuz" + e.Id);
        _testEntityRepository.UpdateRange(entities);
        await _ctx.SaveChangesAsync();
        var entitiesInDb = _ctx.TestEntities.ToList();

        // Assert
        entitiesInDb.Should().HaveCount(count);
        entitiesInDb.Should().OnlyContain(e => e.Value == "Quuz" + e.Id);
    }

    [Fact]
    public async Task Remove_ShouldRemoveEntity()
    {
        // Arrange
        TestEntity entity = await AddRandomEntity();
        TestEntity entity2 = await AddRandomEntity();

        // Act
        var removedCount = _testEntityRepository.Remove(entity);
        var removedCount2 = _testEntityRepository.Remove(entity2.Id);
        await _ctx.SaveChangesAsync();

        // Assert
        removedCount.Should().Be(1);
        removedCount2.Should().Be(1);
        _ctx.TestEntities.Should().HaveCount(0);
    }

    [Fact]
    public async Task Remove_ShouldReturnZero_WhenEntityNotFound()
    {
        // Arrange
        await AddRandomEntity();
        await AddRandomEntity();

        // Act
        var removedCount = _testEntityRepository.Remove(new TestEntity { Id = Guid.NewGuid(), Value = "Foo" });
        var removedCount2 = _testEntityRepository.Remove(Guid.NewGuid());
        await _ctx.SaveChangesAsync();

        // Assert
        removedCount.Should().Be(0);
        removedCount2.Should().Be(0);
        _ctx.TestEntities.Should().HaveCount(2);
    }

    [Fact]
    public async Task RemoveAsync_ShouldRemoveEntity()
    {
        // Arrange
        TestEntity entity = await AddRandomEntity();
        TestEntity entity2 = await AddRandomEntity();

        // Act
        var removedCount = await _testEntityRepository.RemoveAsync(entity);
        var removedCount2 = await _testEntityRepository.RemoveAsync(entity2.Id);
        await _ctx.SaveChangesAsync();

        // Assert
        removedCount.Should().Be(1);
        removedCount2.Should().Be(1);
        _ctx.TestEntities.Should().HaveCount(0);
    }
    
    [Fact]
    public async Task RemoveAsync_ShouldReturnZero_WhenEntityNotFound()
    {
        // Arrange
        await AddRandomEntity();
        await AddRandomEntity();

        // Act
        var removedCount = await _testEntityRepository.RemoveAsync(new TestEntity { Id = Guid.NewGuid(), Value = "Foo" });
        var removedCount2 = await _testEntityRepository.RemoveAsync(Guid.NewGuid());
        await _ctx.SaveChangesAsync();

        // Assert
        removedCount.Should().Be(0);
        removedCount2.Should().Be(0);
        _ctx.TestEntities.Should().HaveCount(2);
    }

    [Fact]
    public async Task RemoveRange_ShouldRemoveEntities()
    {
        // Arrange
        TestEntity entity1 = await AddRandomEntity();
        TestEntity entity2 = await AddRandomEntity();
        TestEntity entity3 = await AddRandomEntity();
        TestEntity entity4 = await AddRandomEntity();
        TestEntity entity5 = await AddRandomEntity();

        // Act
        var removedCount = _testEntityRepository.RemoveRange(new[] { entity1, entity2 });
        var removedCount2 = _testEntityRepository.RemoveRange(new[] { entity4.Id, entity5.Id });
        await _ctx.SaveChangesAsync();
        var entitiesInDb = _ctx.TestEntities.ToList();

        // Assert
        removedCount.Should().Be(2);
        removedCount2.Should().Be(2);
        entitiesInDb.Should().HaveCount(1);
        entitiesInDb.Should().ContainSingle(e => e.Value == entity3.Value);
    }

    [Fact]
    public async Task Find_ShouldReturnEntity()
    {
        // Arrange
        TestEntity entity = await AddRandomEntity();

        // Act
        TestEntity? data = _testEntityRepository.Find(entity.Id);

        // Assert
        data.Should().NotBeNull();
        data!.Value.Should().Be(entity.Value);
    }

    [Fact]
    public async Task FindAsync_ShouldReturnEntity()
    {
        // Arrange
        TestEntity entity = await AddRandomEntity();

        // Act
        TestEntity? data = await _testEntityRepository.FindAsync(entity.Id);

        // Assert
        data.Should().NotBeNull();
        data!.Value.Should().Be(entity.Value);
    }

    [Fact]
    public async Task FindAll_ShouldReturnAllEntities()
    {
        // Arrange
        TestEntity entity1 = await AddRandomEntity();
        TestEntity entity2 = await AddRandomEntity();

        // Act
        var data = _testEntityRepository.FindAll().ToList();

        // Assert
        data.Should().HaveCount(2);
        data.Should().ContainSingle(e => e.Value == entity1.Value);
        data.Should().ContainSingle(e => e.Value == entity2.Value);
    }

    [Fact]
    public async Task FindAllAsync_ShouldReturnAllEntities()
    {
        // Arrange
        TestEntity entity1 = await AddRandomEntity();
        TestEntity entity2 = await AddRandomEntity();

        // Act
        var data = (await _testEntityRepository.FindAllAsync()).ToList();

        // Assert
        data.Should().HaveCount(2);
        data.Should().ContainSingle(e => e.Value == entity1.Value);
        data.Should().ContainSingle(e => e.Value == entity2.Value);
    }

    [Fact]
    public async Task Exists_ShouldReturnTrue()
    {
        // Arrange
        TestEntity entity = await AddRandomEntity();

        // Act
        var exists = _testEntityRepository.Exists(entity.Id);

        // Assert
        exists.Should().BeTrue();
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