namespace AbdulAkinCengiz_222132128.Entity.Dtos.Payment;

public sealed class OrderItemPaymentRowDto
{
    public string ProductName { get; set; } = "";
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal LineTotal => UnitPrice * Quantity;
}