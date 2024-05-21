using AutoMapper;
using Base.DAL.EF;
using Base.Test.Domain;
using Helpers;

namespace Base.Test.DAL;

public class TestEntityRepository(TestDbContext dbContext, IMapper mapper)
    : BaseEntityRepository<TestEntity, TestEntity, TestDbContext>
        (dbContext, new EntityMapper<TestEntity, TestEntity>(mapper));