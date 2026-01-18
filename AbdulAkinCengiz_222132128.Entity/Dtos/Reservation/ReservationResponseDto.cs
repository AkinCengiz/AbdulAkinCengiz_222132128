using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbdulAkinCengiz_222132128.Entity.Dtos.Customer;
using AbdulAkinCengiz_222132128.Entity.Dtos.Order;
using AbdulAkinCengiz_222132128.Entity.Dtos.Table;
using Core.Entity;

namespace AbdulAkinCengiz_222132128.Entity.Dtos.Reservation;
public sealed record ReservationResponseDto : IResponseDto
{
    public int Id { get; set; }
    public DateTime StartAt { get; set; }
    public DateTime EndAt { get; set; }
    public byte GuestCount { get; set; }
    public bool IsConfirm { get; set; }
    public TableResponseDto Table { get; set; }
    public CustomerResponseDto Customer { get; set; }
    public OrderResponseDto Order { get; set; }
    public string TableName => Table?.Name ?? "";
    public string CustomerFullName => Customer != null ? $"{Customer.FirstName} {Customer.LastName}" : "";
}