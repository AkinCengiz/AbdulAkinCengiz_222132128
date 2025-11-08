using Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbdulAkinCengiz_222132128.Entity.Concrete;
public sealed class Order : BaseEntity
{
    public int ReservationId { get; set; }
    public Reservation Reservation { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; }
    public decimal Total { get; set; }
    public bool IsPaid { get; set; }
}