using Core.Entity;

namespace AbdulAkinCengiz_222132128.Entity.Dtos.Order;

public sealed record OrderUpdateRequestDto : IUpdateDto
{
    public int Id { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
    public int ReservationId { get; set; }
    public decimal Total { get; set; }
    public bool IsPaid { get; set; }
}