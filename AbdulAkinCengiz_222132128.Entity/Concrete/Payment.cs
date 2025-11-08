using Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbdulAkinCengiz_222132128.Entity.Concrete;
public sealed class Payment : BaseEntity
{
    public int OrderId { get; set; }
    public Order Order { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaidAt { get; set; }
    public string Method { get; set; }
}
