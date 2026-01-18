using AbdulAkinCengiz_222132128.Entity.Concrete;
using Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbdulAkinCengiz_222132128.Entity.Dtos.OrderItem;

namespace AbdulAkinCengiz_222132128.Entity.Dtos.Order;
public sealed record OrderResponseDto : IResponseDto
{
    public int Id { get; set; }
    public int ReservationId { get; set; }
    public Concrete.Reservation Reservation { get; set; }
    public IReadOnlyCollection<OrderItemResponseDto>? OrderItems { get; set; }
    public decimal Total { get; set; }
    public bool IsPaid { get; set; }
}