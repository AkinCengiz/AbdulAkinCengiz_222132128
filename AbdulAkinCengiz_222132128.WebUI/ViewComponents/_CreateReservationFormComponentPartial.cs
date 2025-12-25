using AbdulAkinCengiz_222132128.WebUI.Models.Customers;
using AbdulAkinCengiz_222132128.WebUI.Models.Reservations;
using Microsoft.AspNetCore.Mvc;

namespace AbdulAkinCengiz_222132128.WebUI.ViewComponents;

public class _CreateReservationFormComponentPartial : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(int tableId, DateTime startAt, DateTime endAt, byte guestCount)
    {
        var model = new ReservationCreateViewModel()
        {
            TableId = tableId,
            StartAt = startAt,
            EndAt = endAt,
            GuestCount = guestCount,
            Customer = new CustomerCreateViewModel()
        };
        return View(model);
    }
}
