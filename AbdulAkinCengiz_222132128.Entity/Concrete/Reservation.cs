using Core.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbdulAkinCengiz_222132128.Entity.Concrete;
public sealed class Reservation : BaseEntity
{
    public DateTime StartAt { get; set; }
    public DateTime EndAt { get; set; }
    public byte GuestCount { get; set; }
    public bool IsConfirm { get; set; } = false;
    public int TableId { get; set; }
    public Table Table { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
}

