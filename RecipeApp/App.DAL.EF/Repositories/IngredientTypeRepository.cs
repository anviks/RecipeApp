using App.Contracts.DAL.Repositories;
using AutoMapper;
using DAL_DTO = App.DAL.DTO;
using Base.DAL.EF;
using Helpers;

namespace App.DAL.EF.Repositories;

public class IngredientTypeRepository(AppDbContext dbContext, IMapper mapper)
    : BaseEntityRepository<Domain.IngredientType, DAL_DTO.IngredientType, AppDbContext>(dbContext,
        new EntityMapper<Domain.IngredientType, DAL_DTO.IngredientType>(mapper)), IIngredientTypeRepository;