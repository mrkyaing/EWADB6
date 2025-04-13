using Asp.Versioning;
using CloudHRMS.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudHRMS.WebAPIs.Controllers.v1 {
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")] // Specify the API version
    [ApiController]
    public class EmployeeController : ControllerBase {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger) {
            _employeeService = employeeService;
            _logger = logger;
        }
        // GET: api/Employee/GetAll
        //https://localhost:5000/api/Employee/GetAll
        [HttpGet]
        public async Task<IActionResult> GetAll(int page, int to) {
            var employees = await _employeeService.GetAll("");
            if (employees == null) {
                return NotFound();//404
            }
            return Ok(employees.Take(page));
        }
        // GET: api/Employee/GetAll
        //https://localhost:5000/api/Employee/getbycode?code=s101 >> queryString pattern
        //https://localhost:5000/api/Employee/getbycode/s101 >> path/route pattern
        [HttpGet("getbycode")]
        public async Task<IActionResult> GetByCode(string code) { //queryString pattern
            _logger.LogInformation($"Request code : {code}");
            var employee = await _employeeService.GetAll("");
            if (employee == null) {
                return NotFound(); // 404
            }
            return Ok(employee.Where(w => w.Code == code));
        }
    }
}

