using Core.Entity;

namespace AbdulAkinCengiz_222132128.Entity.Dtos.Category;

public sealed record CategoryUpdateRequestDto : IUpdateDto
{
    public int Id { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
    public string Name { get; set; }
}