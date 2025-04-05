using CloudHRMS.Models;
using CloudHRMS.Models.ViewModels;
using CloudHRMS.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace CloudHRMS.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;
        private readonly IPayrollService _payrollService;
        private readonly IEmployeeService _employeeService;

        public HomeController(ILogger<HomeController> logger, IPayrollService payrollService, IEmployeeService employeeService) {
            _logger = logger;
            this._payrollService = payrollService;
            this._employeeService = employeeService;
        }

        public async Task<IActionResult> Index() {
            var loginUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);// will give the user's userId
            var allEmployees = await _employeeService.GetAll("dcaf3494-26f5-4f09-94a8-822ab5a6dcf9");
            var dashboardViewModel = new DashboardViewModel() {
                NewEmployeesOfCurrentMonth = allEmployees.Where(w => w.DOE.Month == DateTime.Now.Month)
            };
            // Retrieve sales data from the database (or your data source)
            var payrolls = _payrollService.GetAll(); // Assuming your data source is '_context.Sales'
            // Group sales data by week and calculate total cash amount per week
            var groupedPayData = payrolls
                .GroupBy(s => new { s.DepartmentInfo, s.NetPay }) // Assuming 'WeekStart' and 'WeekEnd' are DateTime fields
                .Select(g => new {
                    Department = $"{g.Key.DepartmentInfo}",
                    TotalNetPay = g.Sum(s => s.NetPay)
                }).ToList();

            // Prepare the data for the chart
            var chartData = new {
                Labels = groupedPayData.Select(sd => sd.Department).ToArray(), // x-axis labels
                Data = groupedPayData.Select(sd => sd.TotalNetPay).ToArray() // y-axis
            };

            ViewData["chartData"] = chartData; // Passing chartData to the view
            return View(dashboardViewModel);
        }

        public IActionResult Privacy() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult AccessDenied() {
            return View();
        }
    }
}
