using CloudHRMS.Models.ViewModels;
using CloudHRMS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CloudHRMS.Controllers
{
    public class AttendanceMasterController : Controller
    {
        private readonly IAttendanceMasterService _attendanceMasterService;
        private readonly IShiftService _shiftService;
        private readonly IDepartmentService _departmentService;
        private readonly IEmployeeService _employeeService;
        private readonly IDailyAttendanceService _dailyAttendanceService;

        public AttendanceMasterController(IAttendanceMasterService attendanceMasterService, IShiftService shiftService, IDepartmentService departmentService, IEmployeeService employeeService, IDailyAttendanceService dailyAttendanceService)
        {
            this._attendanceMasterService = attendanceMasterService;
            _shiftService = shiftService;
            _departmentService = departmentService;
            _employeeService = employeeService;
            this._dailyAttendanceService = dailyAttendanceService;
        }
        public IActionResult List()
        {
            return View(_attendanceMasterService.GetAll());
        }
        [Authorize(Roles = "HR")]
        public async Task<IActionResult> DayEndProcess()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);// will give the user's userId
            var attendanceMaster = new AttendanceMasterViewModel()
            {
                Shifts = _shiftService.GetAll(),
                Departments = _departmentService.GetAll(),
                Employees = await _employeeService.GetAll(userId)
            };
            return View(attendanceMaster);
        }
        public JsonResult GetEmployeeList(string departmentId)
        {
            IList<EmployeeViewModel> employeeList = _dailyAttendanceService.GetEmployeeList(departmentId);
            DailyAttendanceViewModel dailyAttendance = new DailyAttendanceViewModel() { Employees = employeeList };
            return Json(employeeList);
        }
        [Authorize(Roles = "HR")]
        [HttpPost]
        public IActionResult DayEndProcess(AttendanceMasterViewModel ui)
        {
            try
            {
                _attendanceMasterService.Delete(ui.AttendanceDate, ui.ToDate, ui.DepartmentId, ui.EmployeeId);
                _attendanceMasterService.DayEndProcess(ui);
                ViewBag.Info = "successfully save a record to the system";
            }
            catch (Exception ex)
            {
                ViewBag.Info = "Error occur when  saving a record  to the system";
            }
            return RedirectToAction("list");
        }
    }
}
