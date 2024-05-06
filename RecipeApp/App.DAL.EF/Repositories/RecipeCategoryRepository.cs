using App.Contracts.DAL.Repositories;
using App.Domain;
using AutoMapper;
using Base.Contracts.DAL;
using Base.DAL.EF;
using Helpers;

namespace App.DAL.EF.Repositories;

public class RecipeCategoryRepository(AppDbContext dbContext, IMapper mapper)
    : BaseEntityRepository<RecipeCategory, RecipeCategory, AppDbContext>(dbContext,
        new EntityMapper<RecipeCategory, RecipeCategory>(mapper)), IRecipeCategoryRepository;