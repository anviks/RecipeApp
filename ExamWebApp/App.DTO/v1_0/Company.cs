using Base.Domain;

namespace App.DTO.v1_0;

public class Company : BaseEntityId
{
    public string CompanyName { get; set; } = default!;
}