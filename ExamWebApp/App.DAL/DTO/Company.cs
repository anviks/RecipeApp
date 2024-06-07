using Base.Domain;

namespace App.DAL.DTO;

public class Company : BaseEntityId
{
    public string CompanyName { get; set; } = default!;
}