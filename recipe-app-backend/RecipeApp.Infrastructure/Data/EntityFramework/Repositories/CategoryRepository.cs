using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Base;
using RecipeApp.Base.Infrastructure.Data;
using RecipeApp.Infrastructure.Contracts.Data.Repositories;

namespace RecipeApp.Infrastructure.Data.EntityFramework.Repositories;

public class CategoryRepository(AppDbContext dbContext, IMapper mapper)
    : BaseEntityRepository<Entities.Category, DTO.Category, AppDbContext>(dbContext, mapper),
        ICategoryRepository
{
    public override Task<DTO.Category> UpdateAsync(DTO.Category entity)
    {
        Entities.Category category = Mapper.Map(entity)!;
        Entities.Category existingCategory = DbSet.AsNoTracking().First(c => c.Id == category.Id);
        
        LangStr name = category.Name;
        category.Name = existingCategory.Name;
        category.Name.SetTranslation(name);
        
        LangStr? description = category.Description;
        category.Description = existingCategory.Description;
        category.Description?.SetTranslation(description);
        
        var entry = DbContext.Update(category);
        return Task.FromResult(Mapper.Map(entry.Entity)!);
    }
}