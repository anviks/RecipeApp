using App.Contracts.DAL.Repositories;
using AutoMapper;
using DAL_DTO = App.DAL.DTO;
using Base.DAL.EF;
using Helpers;

namespace App.DAL.EF.Repositories;

public class RecipeCategoryRepository(AppDbContext dbContext, IMapper mapper)
    : BaseEntityRepository<Domain.RecipeCategory, DAL_DTO.RecipeCategory, AppDbContext>(dbContext,
        new EntityMapper<Domain.RecipeCategory, DAL_DTO.RecipeCategory>(mapper)), IRecipeCategoryRepository;