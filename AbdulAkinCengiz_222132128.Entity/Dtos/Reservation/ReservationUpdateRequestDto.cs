using Core.Entity;

namespace AbdulAkinCengiz_222132128.Entity.Dtos.Reservation;

public sealed record ReservationUpdateRequestDto : IUpdateDto
{
    public int Id { get; set; }
    public DateTime StartAt { get; set; }
    public DateTime EndAt { get; set; }
    public byte GuestCount { get; set; }
    public bool IsConfirm { get; set; } = false;
    public int TableId { get; set; }
    public int CustomerId { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
}