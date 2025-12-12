using Core.Entity;

namespace AbdulAkinCengiz_222132128.Entity.Dtos.Order;

public sealed record OrderCreateRequestDto : ICreateDto
{
    public int ReservationId { get; set; }
}