using Core.Entity;

namespace AbdulAkinCengiz_222132128.Entity.Dtos.Payment;

public sealed record PaymentUpdateRequestDto : IUpdateDto
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public decimal Amount { get; set; } = decimal.Zero;
    public string Method { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
}