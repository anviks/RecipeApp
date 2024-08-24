using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Base;
using RecipeApp.Base.Helpers;
using RecipeApp.Base.Infrastructure.Data;
using RecipeApp.Infrastructure.Contracts.Data.Repositories;

namespace RecipeApp.Infrastructure.Data.EntityFramework.Repositories;

public class CategoryRepository(AppDbContext dbContext, IMapper mapper)
    : BaseEntityRepository<Entities.Category, DTO.Category, AppDbContext>(
            dbContext,
            new EntityMapper<Entities.Category, DTO.Category>(mapper)
        ),
        ICategoryRepository
{
    public override DTO.Category Update(DTO.Category entity)
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
        return Mapper.Map(entry.Entity)!;
    }

    public override void UpdateRange(IEnumerable<DTO.Category> entities)
    {
        throw new NotImplementedException();
    }
}