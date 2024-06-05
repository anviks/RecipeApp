using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Sample : BaseEntityId
{
    [MaxLength(128)]
    public string Field { get; set; } = default!;
}