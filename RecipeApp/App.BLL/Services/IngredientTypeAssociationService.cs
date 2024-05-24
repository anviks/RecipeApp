using App.Contracts.BLL.Services;
using App.Contracts.DAL.Repositories;
using AutoMapper;
using Base.BLL;
using Base.Contracts.DAL;
using Helpers;
using DAL_DTO = App.DAL.DTO;
using BLL_DTO = App.BLL.DTO;

namespace App.BLL.Services;

public class IngredientTypeAssociationService(
    IIngredientTypeAssociationRepository repository,
    IMapper mapper)
    : BaseEntityService<DAL_DTO.IngredientTypeAssociation, BLL_DTO.IngredientTypeAssociation, IIngredientTypeAssociationRepository>(repository,
            new EntityMapper<DAL_DTO.IngredientTypeAssociation, BLL_DTO.IngredientTypeAssociation>(mapper)),
        IIngredientTypeAssociationService;