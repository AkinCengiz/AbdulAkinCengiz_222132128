using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbdulAkinCengiz_222132128.Entity.Dtos.OrderItem;

public class OrderItemGridResponseDto
{
    public string ProductName { get; set; }
    public byte Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal LineTotal { get; set; }
}
