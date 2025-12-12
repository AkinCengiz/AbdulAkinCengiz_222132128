using Core.Entity;

namespace AbdulAkinCengiz_222132128.Entity.Dtos.Product;

public sealed record ProductUpdateRequestDto : IUpdateDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public int CategoryId { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
}