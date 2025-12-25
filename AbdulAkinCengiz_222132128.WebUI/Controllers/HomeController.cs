using AbdulAkinCengiz_222132128.Business.Abstract;
using AbdulAkinCengiz_222132128.Entity.Dtos.Customer;
using AbdulAkinCengiz_222132128.Entity.Dtos.Reservation;
using AbdulAkinCengiz_222132128.WebUI.Models;
using AbdulAkinCengiz_222132128.WebUI.Models.Reservations;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AbdulAkinCengiz_222132128.Entity.Dtos.Table;
using AbdulAkinCengiz_222132128.WebUI.Models.Customers;
using AbdulAkinCengiz_222132128.WebUI.Models.Tables;
using AutoMapper;

namespace AbdulAkinCengiz_222132128.WebUI.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IReservationService _reservationService;
    private readonly ITableService _tableService;
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;

    public HomeController(
        ILogger<HomeController> logger,
        ITableService tableService,
        ICategoryService categoryService,
        IReservationService reservationService, IMapper mapper)
    {
        _logger = logger;
        _tableService = tableService;
        _categoryService = categoryService;
        _reservationService = reservationService;
        _mapper = mapper;
    }

    //// 1) GET: tarih/saat formu + boþ (veya varsayýlan) model
    //[HttpGet]
    //public IActionResult Index()
    //{
    //    var model = new ReservationPageViewModel()
    //    {
    //        Search = new ReservationSearchTableViewModel()
    //        {
    //            StartAt = DateTime.Now.AddHours(1),   // varsayýlan
    //            EndAt = DateTime.Now.AddHours(2),
    //            GuestCount = 2
    //        },
    //        Create = new ReservationCreateViewModel()
    //        {
    //            Customer = new CustomerCreateViewModel()
    //        }

    //    };

    //    return View(model);
    //}

    //// 2) POST: girilen tarih/saat ve misafir sayýsýna göre uygun masalarý getir
    //[HttpPost]
    //public async Task<IActionResult> Index(ReservationSearchTableViewModel model)
    //{
    //    if (!ModelState.IsValid)
    //    {
    //        // doðrulama hatalarý varsa ayný view'e geri dön
    //        return View(model);
    //    }

    //    var result = await _reservationService
    //        .GetAvailableTablesAsync(model.StartAt, model.EndAt, model.GuestCount);

    //    if (!result.Success)
    //    {
    //        ModelState.AddModelError(string.Empty, result.Message);
    //        return View(model);
    //    }

    //    model.AvailableTables = result.Data.ToList();
    //    var response = new ReservationPageViewModel()
    //    {
    //        Search = _mapper.Map<ReservationSearchTableViewModel>(result.Data)
    //    };

    //    return View(response);
    //}

    //// 3) POST: seçilen masa ve müþteri bilgileriyle rezervasyon oluþtur
    //[HttpPost]
    //public async Task<IActionResult> CreateReservation(ReservationCreateViewModel model)
    //{
    //    if (!ModelState.IsValid)
    //    {
    //        // Hata varsa tekrar ana sayfaya dönebilir, ya da ayrý bir view kullanabilirsin.
    //        // Basitleþtirmek için Index'e dönüp hata gösterelim:
    //        var searchModel = new ReservationSearchTableViewModel()
    //        {
    //            StartAt = model.StartAt,
    //            EndAt = model.EndAt,
    //            GuestCount = model.GuestCount
    //        };

    //        var availableResult = await _reservationService
    //            .GetAvailableTablesAsync(model.StartAt, model.EndAt, model.GuestCount);

    //        if (availableResult.Success)
    //        {
    //            searchModel.AvailableTables = availableResult.Data.ToList();
    //        }

    //        return View("Index", searchModel);
    //    }

    //    // Web tarafýnda ReservationCreateWithCustomerRequestDto kullanarak servise gideriz
    //    var createModel = new ReservationCreateViewModel()
    //    {
    //        TableId = model.TableId,
    //        StartAt = model.StartAt,
    //        EndAt = model.EndAt,
    //        GuestCount = model.GuestCount,
    //        Customer = new CustomerCreateViewModel()
    //        {
    //            FirstName = model.Customer.FirstName,
    //            LastName = model.Customer.LastName,
    //            Phone = model.Customer.Phone,
    //            Email = model.Customer.Email
    //        }
    //    };


    //    var dto = _mapper.Map<ReservationCreateWithCustomerRequestDto>(createModel);
    //    var result = await _reservationService.CreateWithCustomerAsync(dto);

    //    if (!result.Success)
    //    {
    //        ModelState.AddModelError(string.Empty, result.Message);

    //        // tekrar uygun masalarý yükleyip Index'e dönelim
    //        var searchModel = new ReservationSearchTableViewModel()
    //        {
    //            StartAt = model.StartAt,
    //            EndAt = model.EndAt,
    //            GuestCount = model.GuestCount
    //        };

    //        var availableResult = await _reservationService
    //            .GetAvailableTablesAsync(model.StartAt, model.EndAt, model.GuestCount);

    //        if (availableResult.Success)
    //        {
    //            searchModel.AvailableTables = availableResult.Data.ToList();
    //        }

    //        return View("Index", searchModel);
    //    }

    //    TempData["Success"] = "Rezervasyonunuz baþarýyla oluþturuldu.";
    //    return RedirectToAction(nameof(Index));
    //}
    [HttpGet]
    public IActionResult Index()
    {
        var model = new ReservationPageViewModel
        {
            Search = new ReservationSearchTableViewModel
            {
                StartAt = DateTime.Now.AddHours(1),
                EndAt = DateTime.Now.AddHours(2),
                GuestCount = 2,
                AvailableTables = new List<TableResponseDto>()
            },
            Create = new ReservationCreateViewModel
            {
                Customer = new CustomerCreateViewModel()
            }
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Index(ReservationSearchTableViewModel searchModel)
    {
        if (!ModelState.IsValid)
        {
            var vm = new ReservationPageViewModel
            {
                Search = searchModel,
                Create = new ReservationCreateViewModel { Customer = new CustomerCreateViewModel() }
            };
            return View(vm);
        }

        var result = await _reservationService.GetAvailableTablesAsync(
            searchModel.StartAt, searchModel.EndAt, searchModel.GuestCount);

        if (!result.Success)
        {
            ModelState.AddModelError(string.Empty, result.Message);
        }
        else
        {
            searchModel.AvailableTables = result.Data.ToList();
        }

        var response = new ReservationPageViewModel
        {
            Search = searchModel,
            Create = new ReservationCreateViewModel { Customer = new CustomerCreateViewModel() }
        };

        return View(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateReservation(ReservationCreateViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return await ReloadIndexWithTables(model);
        }

        var dto = _mapper.Map<ReservationCreateWithCustomerRequestDto>(model);
        var result = await _reservationService.CreateWithCustomerAsync(dto);

        if (!result.Success)
        {
            ModelState.AddModelError(string.Empty, result.Message);
            return await ReloadIndexWithTables(model);
        }

        TempData["Success"] = "Rezervasyonunuz baþarýyla oluþturuldu.";
        return RedirectToAction(nameof(Index));
    }

    // Yardýmcý metot: hata durumunda Index view’i doðru modelle doldurur
    private async Task<IActionResult> ReloadIndexWithTables(ReservationCreateViewModel model)
    {
        var searchModel = new ReservationSearchTableViewModel
        {
            StartAt = model.StartAt,
            EndAt = model.EndAt,
            GuestCount = model.GuestCount
        };

        var availableResult = await _reservationService
            .GetAvailableTablesAsync(model.StartAt, model.EndAt, model.GuestCount);

        if (availableResult.Success)
        {
            searchModel.AvailableTables = availableResult.Data.ToList();
        }

        var vm = new ReservationPageViewModel
        {
            Search = searchModel,
            Create = new ReservationCreateViewModel { Customer = new CustomerCreateViewModel() }
        };

        return View("Index", vm);
    }

}
