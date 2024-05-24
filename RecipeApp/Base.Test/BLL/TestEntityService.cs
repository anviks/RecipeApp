using AutoMapper;
using Base.BLL;
using Base.Test.DAL;
using Base.Test.Domain;
using Helpers;

namespace Base.Test.BLL;

public class TestEntityService(TestDbContext dbContext, IMapper mapper)
    : BaseEntityService<TestEntity, TestEntity, TestEntityRepository>
    (new TestEntityRepository(dbContext, mapper), new EntityMapper<TestEntity, TestEntity>(mapper));