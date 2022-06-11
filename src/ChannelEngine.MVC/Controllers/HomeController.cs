using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ChannelEngine.MVC.Models;
using ChannelEngine.Application.BusinessLogics;
using ChannelEngine.Application.External.Orders;
using ChannelEngine.Application.External.Requests;

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

        var displayProductsCount = 5;

        var products = await _businessLogic.GetTopProductsDesc(displayProductsCount);

        var productToUpdate = products.LastOrDefault();
        if (productToUpdate is null)
        {
            throw new Exception("fwa");
        }
        await _businessLogic.PatchProduct(productToUpdate.Id, new ProductPatchRequest
        {
            Stock = 1256,
        });

        var updatedProduct = await _businessLogic.GetProduct(productToUpdate.Id);

        var viewModel = new ResponseModel
        {
            Orders = orders,
            Products = products,
            UpdatedProduct = updatedProduct,
        };

        return View(viewModel);
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
