using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbdulAkinCengiz_222132128.Entity.Dtos.OrderItem;

public sealed class OrderItemRowDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = "";
    public byte Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Total => UnitPrice * Quantity;
}
