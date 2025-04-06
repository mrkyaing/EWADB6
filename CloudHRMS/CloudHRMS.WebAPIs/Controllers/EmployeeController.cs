using CloudHRMS.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudHRMS.WebAPIs.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService) {
            this._employeeService = employeeService;
        }
        // GET: api/Employee/GetAll
        //https://localhost:5000/api/Employee/GetAll
        [HttpGet(Name = "GetAll")]
        public IActionResult GetAll(int page, int to) {
            var employees = _employeeService.GetAll("");
            if (employees == null) {
                return NotFound();//404
            }
            return Ok(employees.Result.Take(page));
        }
    }
}
