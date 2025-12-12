using Core.Entity;

namespace AbdulAkinCengiz_222132128.Entity.Dtos.Product;

public sealed record ProductCreateRequestDto : ICreateDto
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public int CategoryId { get; set; }
}