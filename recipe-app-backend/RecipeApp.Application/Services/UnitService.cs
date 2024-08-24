using AutoMapper;
using RecipeApp.Application.Contracts.Services;
using RecipeApp.Application.DTO;
using RecipeApp.Base.Application;
using RecipeApp.Base.Helpers;
using RecipeApp.Infrastructure.Contracts.Data.Repositories;
using DAL = RecipeApp.Infrastructure.Data.DTO;

namespace RecipeApp.Application.Services;

public class UnitService(
    IUnitRepository repository,
    IMapper mapper)
    : BaseEntityService<DAL.Unit, Unit, IUnitRepository>(repository,
            new EntityMapper<DAL.Unit, Unit>(mapper)),
        IUnitService;