using AbdulAkinCengiz_222132128.Entity.Dtos.Product;
using Core.Entity;

namespace AbdulAkinCengiz_222132128.Entity.Dtos.Category;

public sealed record CategoryDetailResponseDto : IDetailDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
    public ICollection<ProductResponseDto> Products { get; set; }
}