using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ChannelEngine.MVC.Models;
using ChannelEngine.Application.BusinessLogics;
using ChannelEngine.Application.External.Orders;

namespace ChannelEngine.MVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IBusinessLogic _businessLogic;

    public HomeController(ILogger<HomeController> logger, IBusinessLogic businessLogic)
    {
        _logger = logger;
        _businessLogic = businessLogic;
    }

    public async Task<IActionResult> Index()
    {
        var orderStatuses = new List<OrderStatus>
        {
            OrderStatus.InProgress,
        };

        var orders = await _businessLogic.GetOrders(orderStatuses);
        return View(orders);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
