using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbdulAkinCengiz_222132128.Entity.Concrete;

namespace AbdulAkinCengiz_222132128.Entity.Dtos.Table;

public sealed class TableCardDto
{
    public int TableId { get; set; }
    public string Name { get; set; } = "";
    public int Seats { get; set; }

    public TableStatus Status { get; set; }

    // Sizde sipariş ilişkisi varsa doldurulur; yoksa null kalır
    public int? ActiveOrderId { get; set; }

    // Opsiyonel: UI’da “xx dk sonra” göstermek isterseniz
    public DateTime? NextReservationStartAt { get; set; }
}
