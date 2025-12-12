using Core.Entity;

namespace AbdulAkinCengiz_222132128.Entity.Dtos.OrderItem;

public sealed record OrderItemCreateRequestDto : ICreateDto
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public byte Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    //public decimal LineTotal { get; set; }
}