using AbdulAkinCengiz_222132128.Entity.Dtos.Category;
using Core.Entity;

namespace AbdulAkinCengiz_222132128.Entity.Dtos.Product;

public sealed record ProductDetailResponseDto : IDetailDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public CategoryResponseDto Category { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreateAt { get; set; }
    public DateTime? UpdateAt { get; set; }
}