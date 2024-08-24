using AutoMapper;
using RecipeApp.Application.Contracts.Services;
using RecipeApp.Application.DTO;
using RecipeApp.Base.Application;
using RecipeApp.Base.Helpers;
using RecipeApp.Infrastructure.Contracts.Data.Repositories;
using DAL = RecipeApp.Infrastructure.Data.DTO;

namespace RecipeApp.Application.Services;

public class IngredientTypeAssociationService(
    IIngredientTypeAssociationRepository repository,
    IMapper mapper)
    : BaseEntityService<DAL.IngredientTypeAssociation, IngredientTypeAssociation, IIngredientTypeAssociationRepository>(repository,
            new EntityMapper<DAL.IngredientTypeAssociation, IngredientTypeAssociation>(mapper)),
        IIngredientTypeAssociationService;