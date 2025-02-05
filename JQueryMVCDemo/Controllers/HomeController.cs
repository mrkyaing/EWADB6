using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using JQueryMVCDemo.Models;

namespace JQueryMVCDemo.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult AjaxPratice() => View();

    [HttpGet]
    public string TellMeNow() => DateTime.Now.ToString();


    public IActionResult MakeOrder() => View();

   
    [HttpPost]
    public JsonResult MakeOrder(int orderId, int unitPrice, int quantity)
    {
        decimal total = unitPrice * quantity;
        return Json(total);
    }

    [HttpPost]
    public JsonResult MakeOrder2(OrderModel orderModel)
    {
        decimal total =orderModel.UnitPrice * orderModel.Quantity;
        total/=0;
        return Json(total);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
