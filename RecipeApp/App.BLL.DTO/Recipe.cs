using System.ComponentModel.DataAnnotations;
using App.Domain.Identity;
using Base.Contracts.Domain;

namespace App.BLL.DTO;

public class Recipe : IDomainEntityId
{
    public Guid Id { get; set; }
    
    [MaxLength(128)]
    [Display(ResourceType = typeof(App.Resources.App.Domain.Recipe), Name = nameof(Title))]
    public string Title { get; set; } = default!;
    
    [MaxLength(512)]
    [Display(ResourceType = typeof(App.Resources.App.Domain.Recipe), Name = nameof(Description))]
    public string Description { get; set; } = default!;
    
    public string ImageFileName { get; set; } = default!;
    
    [Display(ResourceType = typeof(App.Resources.App.Domain.Recipe), Name = nameof(Instructions))]
    public string Instructions { get; set; } = default!;
    
    [Display(ResourceType = typeof(App.Resources.App.Domain.Recipe), Name = nameof(PreparationTime))]
    public int PreparationTime { get; set; }
    
    [Display(ResourceType = typeof(App.Resources.App.Domain.Recipe), Name = nameof(CookingTime))]
    public int CookingTime { get; set; }
    
    [Display(ResourceType = typeof(App.Resources.App.Domain.Recipe), Name = nameof(Servings))]
    public int Servings { get; set; }
    
    [Display(ResourceType = typeof(App.Resources.App.Domain.Recipe), Name = nameof(IsVegetarian))]
    public bool IsVegetarian { get; set; }
    
    [Display(ResourceType = typeof(App.Resources.App.Domain.Recipe), Name = nameof(IsVegan))]
    public bool IsVegan { get; set; }
    
    [Display(ResourceType = typeof(App.Resources.App.Domain.Recipe), Name = nameof(IsGlutenFree))]
    public bool IsGlutenFree { get; set; }
    
    [Display(ResourceType = typeof(App.Resources.App.Domain.Recipe), Name = nameof(CreatedAt))]
    public DateTime CreatedAt { get; set; }
    
    public Guid AuthorUserId { get; set; }
    [Display(ResourceType = typeof(App.Resources.App.Domain.Recipe), Name = nameof(AuthorUser))]
    public AppUser? AuthorUser { get; set; }
    
    [Display(ResourceType = typeof(App.Resources.App.Domain.Recipe), Name = nameof(UpdatedAt))]
    public DateTime? UpdatedAt { get; set; }
    
    public Guid? UpdatingUserId { get; set; }
    [Display(ResourceType = typeof(App.Resources.App.Domain.Recipe), Name = nameof(UpdatingUser))]
    public AppUser? UpdatingUser { get; set; }
}