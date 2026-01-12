using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbdulAkinCengiz_222132128.Entity.Dtos.Order;

public sealed class OrderContextDto
{
    public int OrderId { get; set; }
    public int? ReservationId { get; set; }
    public int? TableId { get; set; }

    public string TableName { get; set; } = "";
    public DateTime? StartAt { get; set; }
    public DateTime? EndAt { get; set; }

    public int? CustomerId { get; set; }
    public string CustomerFullName { get; set; } = "";
}

