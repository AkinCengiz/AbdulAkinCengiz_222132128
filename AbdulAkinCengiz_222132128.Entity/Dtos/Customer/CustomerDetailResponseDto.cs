using Core.Entity;

namespace AbdulAkinCengiz_222132128.Entity.Dtos.Customer;

public sealed record CustomerDetailResponseDto : IDetailDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Email { get; set; }
    public string Phone { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreateAt { get; set; }
    public DateTime? UpdateAt { get; set; }
}