using RecipeApp.Base.Contracts.Domain;

namespace RecipeApp.Base.Infrastructure.Data;

public abstract class BaseEntityId: IDomainEntityId
{
    public Guid Id { get; set; } = default!;
}