using AbdulAkinCengiz_222132128.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using AbdulAkinCengiz_222132128.Business.Abstract;

namespace AbdulAkinCengiz_222132128.WebUI.Controllers;
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ITableService _tableService;

    public HomeController(ILogger<HomeController> logger, ITableService tableService)
    {
        _logger = logger;
        _tableService = tableService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var result = _tableService.GetAllAsync();
        return View(result);
    }
}
