using System.ComponentModel.DataAnnotations;
using Base.Domain;
using AppResource = App.Resources.App.BLL.DTO;

namespace App.BLL.DTO;

public class Category : BaseEntityId
{
    [Display(ResourceType = typeof(AppResource.Category), Name = "Name")]
    public string Name { get; set; } = default!;
    
    [Display(ResourceType = typeof(AppResource.Category), Name = "Description")]
    public string? Description { get; set; }
}