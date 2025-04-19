using Asp.Versioning;
using CloudHRMS.Domain.Models.ViewModels;
using CloudHRMS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudHRMS.WebAPIs.Controllers.v1 {
    //specify to enable both v1 and v2
    [ApiVersion(1)]
    [ApiVersion(2)]
    [Route("api/v{v:apiVersion}/department")]
    [ApiController]
    public class DepartmentController : ControllerBase {
        private readonly IDepartmentService _departmentService;
        private readonly ILogger<DepartmentController> _logger;
        public DepartmentController(IDepartmentService departmentService, ILogger<DepartmentController> logger) {
            _departmentService = departmentService;
            _logger = logger;
        }
        [MapToApiVersion(1)]
        // GET: api/Department/v1/GetAll
        [HttpGet]
        public IActionResult GetAll() {
            var departments = _departmentService.GetAll();
            return Ok(departments);
        }
        [Authorize(Roles = "HR")]
        [MapToApiVersion(1)]
        // POST: api/Department/v1/Create
        [HttpPost]
        public IActionResult Create([FromBody] DepartmentViewModel departmentViewModel) {
            if (departmentViewModel == null) {
                return BadRequest("Department data is null");//400
            }
            _departmentService.Create(departmentViewModel);
            return Ok(new { message = "department is created", statusCode = "200" });//200
        }
        [Authorize(Roles = "HR")]
        [MapToApiVersion(1)]
        // POST: api/Department/v1/delete >> { "Id":"" }
        [HttpDelete]
        public IActionResult Delete([FromBody] DepartmentViewModelV3 departmentViewModelV3) {
            if (string.IsNullOrEmpty(departmentViewModelV3.Id)) {
                return BadRequest("Department Id is required.");//400
            }
            _departmentService.Delete(departmentViewModelV3.Id);
            return Ok(new { message = "department is deleted", statusCode = "200" });//200
        }
        [Authorize(Roles = "HR")]
        [MapToApiVersion(2)]
        // POST: api/Department/v2/Create
        [HttpPost]
        public IActionResult Create2([FromBody] DepartmentViewModelV2 departmentViewModel2) {
            _logger.LogInformation("Request Body:", departmentViewModel2);
            if (departmentViewModel2 == null) {
                return BadRequest("Department data is null");//400
            }
            _departmentService.Create(departmentViewModel2);
            return Ok(new { message = "department is created", statusCode = "200", version = "v2" });
        }
    }

}

