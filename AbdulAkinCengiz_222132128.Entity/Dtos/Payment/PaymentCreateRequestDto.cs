using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entity;

namespace AbdulAkinCengiz_222132128.Entity.Dtos.Payment;
public sealed record PaymentCreateRequestDto : ICreateDto
{
    public int OrderId { get; set; }
    public decimal Amount { get; set; } = decimal.Zero;
    public string Method { get; set; }
}