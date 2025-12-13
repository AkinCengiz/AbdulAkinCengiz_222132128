using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbdulAkinCengiz_222132128.Entity.Dtos.Customer;
using Core.Entity;

namespace AbdulAkinCengiz_222132128.Entity.Dtos.Reservation;
public sealed record ReservationCreateWithCustomerRequestDto : ICreateDto
{
    public DateTime StartAt { get; set; }
    public DateTime EndAt { get; set; }
    public byte GuestCount { get; set; }
    public int TableId { get; set; }
    public CustomerCreateRequestDto Customer { get; set; } = null!;
}
