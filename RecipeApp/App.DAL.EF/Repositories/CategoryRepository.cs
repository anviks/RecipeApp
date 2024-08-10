using App.Contracts.DAL.Repositories;
using AutoMapper;
using Base.DAL.EF;
using Base.Domain;
using Helpers;
using Microsoft.EntityFrameworkCore;
using DAL_DTO = App.DAL.DTO;

namespace App.DAL.EF.Repositories;

public class CategoryRepository(AppDbContext dbContext, IMapper mapper)
        : BaseEntityRepository<Domain.Category, DAL_DTO.Category, AppDbContext>(
                dbContext,
                new EntityMapper<Domain.Category, DAL_DTO.Category>(mapper)),
            ICategoryRepository
{
    public override DAL_DTO.Category Update(DAL_DTO.Category entity)
    {
        Domain.Category category = Mapper.Map(entity)!;
        Domain.Category existingCategory = DbSet.AsNoTracking().First(c => c.Id == category.Id);
        
        LangStr name = category.Name;
        category.Name = existingCategory.Name;
        category.Name.SetTranslation(name);
        
        LangStr? description = category.Description;
        category.Description = existingCategory.Description;
        category.Description?.SetTranslation(description);
        
        var entry = DbContext.Update(category);
        return Mapper.Map(entry.Entity)!;
    }

    public override void UpdateRange(IEnumerable<DAL_DTO.Category> entities)
    {
        throw new NotImplementedException();
    }
}