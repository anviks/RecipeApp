using AutoMapper;
using RecipeApp.Base.Infrastructure.Data;

namespace RecipeApp.Test.UnitTests.Base.Infrastructure;

public class TestEntityRepository(TestDbContext dbContext, IMapper mapper)
    : BaseEntityRepository<TestEntity, TestEntity, TestDbContext>(dbContext, mapper);