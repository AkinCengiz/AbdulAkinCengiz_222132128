using AbdulAkinCengiz_222132128.Entity.Concrete;
using Core.Entity;

namespace AbdulAkinCengiz_222132128.Entity.Dtos.Order;

public sealed record OrderDetailResponseDto : IDetailDto
{
    public int Id { get; set; }
    public int ReservationId { get; set; }
    public ICollection<Concrete.OrderItem>? OrderItems { get; set; }
    public decimal Total { get; set; }
    public bool IsPaid { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreateAt { get; set; }
    public DateTime? UpdateAt { get; set; }
}