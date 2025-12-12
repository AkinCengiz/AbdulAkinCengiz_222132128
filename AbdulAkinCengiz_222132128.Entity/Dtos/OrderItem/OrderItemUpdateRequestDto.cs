using Core.Entity;

namespace AbdulAkinCengiz_222132128.Entity.Dtos.OrderItem;

public sealed record OrderItemUpdateRequestDto : IUpdateDto
{
    public int Id { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public byte Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    //public decimal LineTotal { get; set; }
}