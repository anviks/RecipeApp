using App.BLL.Exceptions;
using App.Contracts.BLL.Services;
using App.Contracts.DAL.Repositories;
using DAL_DTO = App.DAL.DTO;
using BLL_DTO = App.BLL.DTO;
using App.DAL.EF.Repositories;
using AutoMapper;
using Base.BLL;
using Base.Contracts.BLL;
using Base.Contracts.DAL;
using Base.Resources;
using Helpers;
using Microsoft.AspNetCore.Http;

namespace App.BLL.Services;

public class RecipeService(
    IUnitOfWork unitOfWork,
    IRecipeRepository repository,
    IMapper mapper)
    : BaseEntityService<DAL_DTO.Recipe, BLL_DTO.RecipeResponse, IRecipeRepository>(unitOfWork, repository,
            new EntityMapper<DAL_DTO.Recipe, BLL_DTO.RecipeResponse>(mapper)),
        IRecipeService
{
    private readonly EntityMapper<BLL_DTO.RecipeRequest, DAL_DTO.Recipe> _recipeMapper = new(mapper);
    private readonly string[] _uploadPathFromWebroot = ["uploads", "images"];

    public async Task<BLL_DTO.RecipeResponse> AddAsync(BLL_DTO.RecipeRequest recipeRequest, Guid userId,
        string webRootPath)
    {
        if (recipeRequest.ImageFile == null) throw new MissingImageException();

        DAL_DTO.Recipe dalRecipe = _recipeMapper.Map(recipeRequest)!;
        dalRecipe.CreatedAt = DateTime.Now;
        dalRecipe.AuthorUserId = userId;

        var uploadUrl = await SaveImage(recipeRequest.ImageFile, webRootPath);
        dalRecipe.ImageFileUrl = uploadUrl;
        DAL_DTO.Recipe addedRecipe = Repository.Add(dalRecipe);
        return Mapper.Map(addedRecipe)!;
    }

    public async Task<BLL_DTO.RecipeResponse> UpdateAsync(BLL_DTO.RecipeRequest recipeRequest, Guid userId,
        string webRootPath)
    {
        DAL_DTO.Recipe existingRecipe = (await Repository.FindAsync(recipeRequest.Id))!;
        DAL_DTO.Recipe dalRecipe = _recipeMapper.Map(recipeRequest)!;
        dalRecipe.CreatedAt = existingRecipe.CreatedAt;
        dalRecipe.AuthorUserId = existingRecipe.AuthorUserId;
        dalRecipe.UpdatedAt = DateTime.Now;
        dalRecipe.UpdatingUserId = userId;
        
        if (recipeRequest.ImageFile != null)
        {
            var uploadUrl = await SaveImage(recipeRequest.ImageFile, webRootPath);
            dalRecipe.ImageFileUrl = uploadUrl;
        }
        else
        {
            dalRecipe.ImageFileUrl = existingRecipe.ImageFileUrl;
        }

        DAL_DTO.Recipe updatedRecipe = Repository.Update(dalRecipe);
        return Mapper.Map(updatedRecipe)!;
    }

    private async Task<string> SaveImage(IFormFile file, string webRootPath)
    {
        var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
        var uploadPath = Path.Combine(new[] { webRootPath }.Concat(_uploadPathFromWebroot)
            .Concat(new[] { fileName }).ToArray());
        await using (var stream = new FileStream(uploadPath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        var uploadUrl = "~/" + string.Join("/", _uploadPathFromWebroot.Concat(new[] { fileName }));
        return uploadUrl;
    }
}