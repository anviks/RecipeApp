using AutoMapper;
using Base.Test.Domain;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit.Abstractions;

namespace Base.Test.DAL;

public class BaseRepositoryTest
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly TestDbContext _ctx;
    private readonly TestEntityRepository _testEntityRepository;
    private static readonly Random Random = new();

    public BaseRepositoryTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
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

        _testEntityRepository =
            new TestEntityRepository(
                _ctx,
                mapper
            );
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
    
    [Fact]
    public async Task Update_ShouldUpdateEntity()
    {
        // Arrange
        TestEntity entity = await AddRandomEntity();

        // Act
        entity.Value = "Quuz";
        TestEntity data = _testEntityRepository.Update(entity);
        await _ctx.SaveChangesAsync();
        TestEntity? data2 = await _testEntityRepository.FindAsync(data.Id);

        // Assert
        data.Should().NotBeNull();
        data.Id.Should().Be(entity.Id);
        data.Value.Should().Be("Quuz");
        
        data2.Should().NotBeNull();
        data2!.Id.Should().Be(entity.Id);
        data2.Value.Should().Be("Quuz");
    }

    [Fact]
    public async Task Remove_ShouldRemoveEntity()
    {
        // Arrange
        TestEntity entity = await AddRandomEntity();

        // Act
        var removedCount = await _testEntityRepository.RemoveAsync(entity);
        await _ctx.SaveChangesAsync();
        TestEntity? entityInDb = await _testEntityRepository.FindAsync(entity.Id);
        
        // Assert
        removedCount.Should().Be(1);
        entityInDb.Should().BeNull();
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