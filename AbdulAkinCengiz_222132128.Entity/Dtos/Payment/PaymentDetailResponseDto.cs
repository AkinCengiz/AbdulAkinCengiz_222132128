using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbdulAkinCengiz_222132128.Entity.Dtos.Order;
using Core.Entity;

namespace AbdulAkinCengiz_222132128.Entity.Dtos.Payment;
public sealed record PaymentDetailResponseDto : IDetailDto
{
    public int Id { get; set; }
    public OrderResponseDto Order { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaidAt { get; set; }
    public string Method { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreateAt { get; set; }
    public DateTime? UpdateAt { get; set; }
}
