using App.Contracts.BLL.Services;
using App.Contracts.DAL.Repositories;
using AutoMapper;
using Base.BLL;
using Base.Contracts.DAL;
using Helpers;
using DAL_DTO = App.DAL.DTO;
using BLL_DTO = App.BLL.DTO;

namespace App.BLL.Services;

public class CategoryService(
    IUnitOfWork unitOfWork,
    ICategoryRepository repository,
    IMapper mapper)
    : BaseEntityService<DAL_DTO.Category, BLL_DTO.Category, ICategoryRepository>(unitOfWork, repository,
            new EntityMapper<DAL_DTO.Category, BLL_DTO.Category>(mapper)),
        ICategoryService;