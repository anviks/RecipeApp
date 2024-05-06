using App.Contracts.DAL.Repositories;
using App.Domain;
using AutoMapper;
using Base.Contracts.DAL;
using Base.DAL.EF;
using Helpers;

namespace App.DAL.EF.Repositories;

public class ReviewRepository(AppDbContext dbContext, IMapper mapper)
    : BaseEntityRepository<Review, Review, AppDbContext>(dbContext, new EntityMapper<Review, Review>(mapper)),
        IReviewRepository;