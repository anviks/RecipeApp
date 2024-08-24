using AutoMapper;
using RecipeApp.Base.Application;
using RecipeApp.Base.Helpers;
using RecipeApp.Test.UnitTests.Base.Infrastructure;

namespace RecipeApp.Test.UnitTests.Base.Application;

public class TestEntityService(TestDbContext dbContext, IMapper mapper)
    : BaseEntityService<TestEntity, TestEntity, TestEntityRepository>
    (new TestEntityRepository(dbContext, mapper), new EntityMapper<TestEntity, TestEntity>(mapper));