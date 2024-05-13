using App.Contracts.BLL.Services;
using App.Contracts.DAL.Repositories;
using AutoMapper;
using Base.BLL;
using Base.Contracts.DAL;
using Helpers;
using DAL_DTO = App.DAL.DTO;
using BLL_DTO = App.BLL.DTO;

namespace App.BLL.Services;

public class ReviewService(
    IUnitOfWork unitOfWork,
    IReviewRepository repository,
    IMapper mapper)
    : BaseEntityService<DAL_DTO.Review, BLL_DTO.Review, IReviewRepository>(unitOfWork, repository,
            new EntityMapper<DAL_DTO.Review, BLL_DTO.Review>(mapper)),
        IReviewService;