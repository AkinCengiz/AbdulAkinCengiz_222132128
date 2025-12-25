using AbdulAkinCengiz_222132128.Business.Abstract;
using AbdulAkinCengiz_222132128.WebUI.Models.Customers;
using AbdulAkinCengiz_222132128.WebUI.Models.Reservations;
using AbdulAkinCengiz_222132128.WebUI.Models.Tables;
using Microsoft.AspNetCore.Mvc;

namespace AbdulAkinCengiz_222132128.WebUI.ViewComponents;

public class _SearchFormComponentPartial : ViewComponent
{
    //public async Task<IViewComponentResult> InvokeAsync(int tableId, DateTime startAt, DateTime endAt, byte guestCount)
    //{
    //    var model = new ReservationCreateViewModel
    //    {
    //        TableId = tableId,
    //        StartAt = startAt,
    //        EndAt = endAt,
    //        GuestCount = guestCount,
    //        Customer = new CustomerCreateViewModel()
    //    };
    //    return View(model);
    //}

    private readonly IReservationService _reservationService;

    public _SearchFormComponentPartial(IReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var model = new ReservationSearchTableViewModel()
        {
            StartAt = DateTime.Now.AddHours(1),   // varsayılan
            EndAt = DateTime.Now.AddHours(2),
            GuestCount = 2
        };
        return View(model);
    }

}
