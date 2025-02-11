using CloudHRMS.Models.ViewModels;
using CloudHRMS.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CloudHRMS.Controllers {
    public class PayrollController : Controller {
        private readonly IPayrollService _payrollService;
        private readonly IDailyAttendanceService _dailyAttendanceService;
        private readonly IDepartmentService _departmentService;
        private readonly IEmployeeService _employeeService;

        public PayrollController(IPayrollService payrollService, IDailyAttendanceService dailyAttendanceService, IDepartmentService departmentService, IEmployeeService employeeService) {
            this._payrollService = payrollService;
            this._dailyAttendanceService = dailyAttendanceService;
            _departmentService = departmentService;
            _employeeService = employeeService;
        }
        public IActionResult List() {
            return View(_payrollService.GetAll());
        }
        public async Task<IActionResult> PayrollProcess() {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);// will give the user's userId
            var payrollProcessViewModel = new PayrollProcessViewModel() {
                Departments = _departmentService.GetAll(),
                Employees = await _employeeService.GetAll(userId)
            };
            return View(payrollProcessViewModel);
        }
        [HttpPost]
        public IActionResult PayrollProcess(PayrollProcessViewModel ui) {
            try {
                _payrollService.PayrollProcess(ui);
                TempData["Info"] = "successfully save a record to the system";
            }
            catch (Exception ex) {
                TempData["Info"] = "Error occur when  saving a record  to the system";
            }
            return RedirectToAction("list");
        }
        public JsonResult GetEmployeeList(string departmentId) {
            IList<EmployeeViewModel> employeeList = _dailyAttendanceService.GetEmployeeList(departmentId);
            DailyAttendanceViewModel dailyAttendance = new DailyAttendanceViewModel() { Employees = employeeList };
            return Json(employeeList);
        }
    }
}
