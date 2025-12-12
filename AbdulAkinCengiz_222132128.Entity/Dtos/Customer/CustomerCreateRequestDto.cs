using Core.Entity;

namespace AbdulAkinCengiz_222132128.Entity.Dtos.Customer;

public sealed record CustomerCreateRequestDto : ICreateDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Email { get; set; }
    public string Phone { get; set; }
}