using Base.Domain;

namespace App.BLL.DTO;

public class IngredientType : BaseEntityId
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
}