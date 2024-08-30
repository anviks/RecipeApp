using AutoMapper;
using Microsoft.AspNetCore.Http;
using RecipeApp.Application.Contracts.Services;
using RecipeApp.Application.DTO;
using RecipeApp.Application.Exceptions;
using RecipeApp.Base.Application;
using RecipeApp.Base.Helpers;
using RecipeApp.Infrastructure.Contracts.Data;
using RecipeApp.Infrastructure.Contracts.Data.Repositories;
using DAL = RecipeApp.Infrastructure.Data.DTO;

namespace RecipeApp.Application.Services;

public class RecipeService(
    IAppUnitOfWork unitOfWork,
    IMapper mapper)
    : BaseEntityService<DAL.Recipe, RecipeResponse, IRecipeRepository>(
            unitOfWork.Recipes,
            new EntityMapper<DAL.Recipe, RecipeResponse>(mapper)),
        IRecipeService
{
    private readonly EntityMapper<RecipeRequest, DAL.Recipe> _recipeMapper = new(mapper);
    private readonly EntityMapper<Category, DAL.Category> _categoryMapper = new(mapper);
    private readonly EntityMapper<Ingredient, DAL.Ingredient> _ingredientMapper = new(mapper);
    private static readonly string[] UploadPathFromWebroot = ["uploads", "images"];

    public override async Task<RecipeResponse?> FindAsync(Guid id, bool tracking = false)
    {
        DAL.Recipe? recipe = await Repository.FindAsync(id, tracking);
        RecipeResponse? recipeResponse = Mapper.Map(recipe!);

        recipeResponse!.Categories = new List<Category>();
        recipeResponse.Ingredients = new List<Ingredient>();

        foreach (DAL.RecipeCategory recipeCategory in recipe!.RecipeCategories!)
        {
            DAL.Category? category = await unitOfWork.Categories.FindAsync(recipeCategory.CategoryId);
            recipeResponse.Categories.Add(_categoryMapper.Map(category)!);
        }
        
        foreach (DAL.RecipeIngredient recipeIngredient in recipe.RecipeIngredients!)
        {
            DAL.Ingredient? ingredient = await unitOfWork.Ingredients.FindAsync(recipeIngredient.IngredientId);
            recipeResponse.Ingredients.Add(_ingredientMapper.Map(ingredient)!);
        }
        
        return recipeResponse;
    }

    public async Task<RecipeResponse> AddAsync(RecipeRequest recipeRequest, Guid userId,
        string localWebRootPath)
    {
        if (recipeRequest.ImageFile == null) throw new MissingImageException();

        DAL.Recipe dalRecipe = _recipeMapper.Map(recipeRequest)!;
        dalRecipe.CreatedAt = DateTime.Now.ToUniversalTime();
        dalRecipe.AuthorUserId = userId;

        var uploadUrl = await SaveImage(recipeRequest.ImageFile, localWebRootPath);
        dalRecipe.ImageFileUrl = uploadUrl;
        DAL.Recipe addedRecipe = Repository.Add(dalRecipe);
        return Mapper.Map(addedRecipe)!;
    }

    public async Task<RecipeResponse> UpdateAsync(RecipeRequest recipeRequest, Guid userId,
        string localWebRootPath)
    {
        DAL.Recipe existingRecipe = (await Repository.FindAsync(recipeRequest.Id))!;
        DAL.Recipe dalRecipe = _recipeMapper.Map(recipeRequest)!;
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

        DAL.Recipe updatedRecipe = Repository.Update(dalRecipe);
        return Mapper.Map(updatedRecipe)!;
    }
    
    public async Task<int> RemoveAsync(Guid id, string localWebRootPath)
    {
        DAL.Recipe? existingRecipe = await Repository.FindAsync(id);
        if (existingRecipe == null) return 0;
        DeleteImage(localWebRootPath, existingRecipe.ImageFileUrl);
        return await Repository.RemoveAsync(id);
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