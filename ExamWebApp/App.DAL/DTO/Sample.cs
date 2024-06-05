using Base.Domain;

namespace App.DAL.DTO;

public class Sample : BaseEntityId
{
    public string Field { get; set; } = default!;
}