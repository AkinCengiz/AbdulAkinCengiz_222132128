using AbdulAkinCengiz_222132128.WebUI.Models.Customers;

namespace AbdulAkinCengiz_222132128.WebUI.Models.Reservations;

public sealed class ReservationCreateViewModel
{
    public DateTime StartAt { get; set; }
    public DateTime EndAt { get; set; }
    public byte GuestCount { get; set; }
    public int TableId { get; set; }

    public CustomerCreateViewModel Customer { get; set; } = new CustomerCreateViewModel();
}
