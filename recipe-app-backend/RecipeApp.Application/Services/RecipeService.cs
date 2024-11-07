using AutoMapper;
using Microsoft.AspNetCore.Http;
using RecipeApp.Application.Contracts.Services;
using RecipeApp.Application.Exceptions;
using RecipeApp.Infrastructure.Contracts.Data;
using DAL = RecipeApp.Infrastructure.Data.DTO;
using BLL = RecipeApp.Application.DTO;

namespace RecipeApp.Application.Services;

public class RecipeService(
    IAppUnitOfWork unitOfWork,
    IMapper mapper)
    : IRecipeService
{
    private static readonly string[] UploadPathFromWebroot = ["uploads", "images"];

    public async Task<BLL.RecipeResponse?> GetByIdAsync(Guid id, bool tracking = false)
    {
        DAL.Recipe? recipe = await unitOfWork.Recipes.GetByIdAsync(id, tracking);
        return mapper.Map<BLL.RecipeResponse>(recipe!);
    }

    public async Task<BLL.RecipeResponse?> GetByIdDetailedAsync(Guid id)
    {
        DAL.Recipe? recipe = await unitOfWork.Recipes.GetByIdDetailedAsync(id);
        var recipeResponse = mapper.Map<BLL.RecipeResponse>(recipe!);
        
        recipeResponse.Categories = mapper.Map<List<BLL.Category>>(recipe!.RecipeCategories!.Select(rc => rc.Category));
        recipeResponse.Ingredients = mapper.Map<List<BLL.Ingredient>>(recipe!.RecipeIngredients!.Select(ri => ri.Ingredient));
        
        return recipeResponse;
    }

    public async Task<IEnumerable<BLL.RecipeResponse>> GetAllAsync(bool tracking = false)
    {
        var recipes = await unitOfWork.Recipes.GetAllAsync(tracking);
        return recipes.Select(mapper.Map<BLL.RecipeResponse>);
    }

    public async Task<IEnumerable<BLL.RecipeResponse>> GetAllDetailedAsync()
    {
        var recipes = await unitOfWork.Recipes.GetAllDetailedAsync();
        var enumerable = recipes.ToList();

        return enumerable.Select(recipe =>
        {
            var recipeResponse = mapper.Map<BLL.RecipeResponse>(recipe);
            
            recipeResponse.Categories = mapper.Map<List<BLL.Category>>(recipe.RecipeCategories!.Select(rc => rc.Category));
            recipeResponse.Ingredients = mapper.Map<List<BLL.Ingredient>>(recipe.RecipeIngredients!.Select(ri => ri.Ingredient));

            return recipeResponse;
        });
    }

    public async Task<BLL.RecipeResponse> AddAsync(
        BLL.RecipeRequest recipeRequest, 
        Guid userId, 
        string localWebRootPath)
    {
        if (recipeRequest.ImageFile == null) throw new MissingImageException();

        var dalRecipe = mapper.Map<DAL.Recipe>(recipeRequest)!;
        dalRecipe.CreatedAt = DateTime.Now.ToUniversalTime();
        dalRecipe.AuthorUserId = userId;

        var uploadUrl = await SaveImage(recipeRequest.ImageFile, localWebRootPath);
        dalRecipe.ImageFileUrl = uploadUrl;
        DAL.Recipe addedRecipe = await unitOfWork.Recipes.AddAsync(dalRecipe);
        return mapper.Map<BLL.RecipeResponse>(addedRecipe);
    }

    public async Task<BLL.RecipeResponse> UpdateAsync(
        BLL.RecipeRequest recipeRequest, 
        Guid userId, 
        string localWebRootPath)
    {
        DAL.Recipe existingRecipe = (await unitOfWork.Recipes.GetByIdAsync(recipeRequest.Id))!;
        var dalRecipe = mapper.Map<DAL.Recipe>(recipeRequest)!;
        dalRecipe.CreatedAt = existingRecipe.CreatedAt;
        dalRecipe.AuthorUserId = existingRecipe.AuthorUserId;
        dalRecipe.UpdatedAt = DateTime.Now.ToUniversalTime();
        dalRecipe.UpdatingUserId = userId;
        
        if (recipeRequest.ImageFile != null)
        {
            var uploadUrl = await SaveImage(recipeRequest.ImageFile, localWebRootPath);
            dalRecipe.ImageFileUrl = uploadUrl;
            DeleteImage(localWebRootPath, existingRecipe.ImageFileUrl);
        }
        else
        {
            dalRecipe.ImageFileUrl = existingRecipe.ImageFileUrl;
        }

        DAL.Recipe updatedRecipe = await unitOfWork.Recipes.UpdateAsync(dalRecipe);
        return mapper.Map<BLL.RecipeResponse>(updatedRecipe);
    }

    public async Task DeleteAsync(BLL.RecipeResponse entity, string localWebRootPath)
    {
        var dalRecipe = mapper.Map<DAL.Recipe>(entity);
        await unitOfWork.Recipes.DeleteAsync(dalRecipe);
        DeleteImage(localWebRootPath, dalRecipe.ImageFileUrl);
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await unitOfWork.Recipes.ExistsAsync(id);
    }

    private static async Task<string> SaveImage(IFormFile file, string webRootPath)
    {
        var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
        var uploadPath = Path.Combine(webRootPath, Path.Combine(UploadPathFromWebroot), fileName);

        Directory.CreateDirectory(Path.GetDirectoryName(uploadPath)!);
        
        await using (var stream = new FileStream(uploadPath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        var uploadUrl = "~/" + string.Join("/", UploadPathFromWebroot.Concat(new[] { fileName }));
        return uploadUrl;
    }

    private static void DeleteImage(string localWebRootPath, string imageUrl)
    {
        var absoluteImagePath = Path.Combine(new []{localWebRootPath}.Concat(imageUrl.Replace("~/", "").Split('/')).ToArray());
        if (File.Exists(absoluteImagePath))
        {
            File.Delete(absoluteImagePath);
        }
    }
}