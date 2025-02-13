using System.Diagnostics;
using CloudHRMS.Models;
using CloudHRMS.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudHRMS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPayrollService _payrollService;

        public HomeController(ILogger<HomeController> logger, IPayrollService payrollService)
        {
            _logger = logger;
            this._payrollService = payrollService;
        }

        public IActionResult Index()
        {
            // Retrieve sales data from the database (or your data source)
            var payrolls = _payrollService.GetAll(); // Assuming your data source is '_context.Sales'
            // Group sales data by week and calculate total cash amount per week
            var groupedPayData = payrolls
                .GroupBy(s => new { s.DepartmentInfo, s.NetPay }) // Assuming 'WeekStart' and 'WeekEnd' are DateTime fields
                .Select(g => new
                {
                    Department = $"{g.Key.DepartmentInfo}",
                    TotalNetPay = g.Sum(s => s.NetPay)
                }).ToList();

            // Prepare the data for the chart
            var chartData = new
            {
                Labels = groupedPayData.Select(sd => sd.Department).ToArray(), // x-axis labels
                Data = groupedPayData.Select(sd => sd.TotalNetPay).ToArray() // y-axis
            };

            ViewData["chartData"] = chartData; // Passing chartData to the view
            return View();
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
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
