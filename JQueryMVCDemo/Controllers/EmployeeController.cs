using JQueryMVCDemo.Models;
using Microsoft.AspNetCore.Mvc;
namespace JQueryMVCDemo.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ILogger<EmployeeController> _logger;
        //Constructor Injectin in here for ILogger Interface 
        public EmployeeController(ILogger<EmployeeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Entry() => View();

        [HttpPost]
        public IActionResult Entry(EmployeeModel employeeModel)
        {
            try
            {
                _logger.LogInformation("Your Information In here !");
                _logger.LogInformation("Id:"+employeeModel.Id);
                _logger.LogInformation("First Name:"+employeeModel.FirstName);
                _logger.LogInformation("Last Name:"+employeeModel.LastName);
                _logger.LogInformation("Street:" + employeeModel.HomeAddress.Street);
                _logger.LogInformation("Country :" + employeeModel.HomeAddress.CountryCode);
                _logger.LogInformation("Postal Code:" + employeeModel.HomeAddress.PostalCode);
            }
            catch (Exception e)
            { 
                _logger.LogError(e.Message);
            }
            return View();
        }
        public IActionResult EmployeeList() => View();
        [HttpPost]
        public IActionResult EmployeeList(List<EmployeeModel> employees)
        {
            _logger.LogInformation("Total Employee Count:"+employees.Count.ToString());
            foreach (var employee in employees)
            {
                _logger.LogInformation($"Full Name:{employee.FirstName} {employee.LastName}");
            }
            ViewBag.TotalCount=employees.Count;
            return View();
        }
    }
}