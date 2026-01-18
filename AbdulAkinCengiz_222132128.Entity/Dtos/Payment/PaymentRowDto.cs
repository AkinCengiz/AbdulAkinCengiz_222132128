namespace AbdulAkinCengiz_222132128.Entity.Dtos.Payment;

public sealed class PaymentRowDto
{
    public int Id { get; set; }
    public DateTime PaidAt { get; set; }
    public decimal Amount { get; set; }
    public string Method { get; set; } = ""; // Nakit/Kart
    public string? Note { get; set; }
}