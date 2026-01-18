using AbdulAkinCengiz_222132128.Entity.Dtos.Customer;
using AbdulAkinCengiz_222132128.Entity.Dtos.Order;
using AbdulAkinCengiz_222132128.Entity.Dtos.Table;
using Core.Entity;

namespace AbdulAkinCengiz_222132128.Entity.Dtos.Reservation;

public sealed record ReservationDetailResponseDto : IDetailDto
{
    public int Id { get; set; }
    public DateTime StartAt { get; set; }
    public DateTime EndAt { get; set; }
    public byte GuestCount { get; set; }
    public bool IsConfirm { get; set; }
    public TableResponseDto Table { get; set; }
    public CustomerResponseDto Customer { get; set; }
    public OrderResponseDto Order { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreateAt { get; set; }
    public DateTime? UpdateAt { get; set; }
}