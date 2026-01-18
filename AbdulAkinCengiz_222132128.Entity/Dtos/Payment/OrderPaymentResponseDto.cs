using AbdulAkinCengiz_222132128.Entity.Dtos.OrderItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbdulAkinCengiz_222132128.Entity.Dtos.Payment;

public sealed class OrderPaymentResponseDto
{
    public int OrderId { get; set; }
    public decimal Total { get; set; }
    public bool IsPaid { get; set; }
    public bool IsActive { get; set; }

    public int ReservationId { get; set; }
    public string TableName { get; set; } = "";
    public string CustomerFullName { get; set; } = "";
    public DateTime StartAt { get; set; }
    public DateTime EndAt { get; set; }

    public List<OrderItemPaymentRowDto> Items { get; set; } = new();

    // Ödeme geçmişi (Payment tablonuz varsa)
    public List<PaymentRowDto> Payments { get; set; } = new();
}