using CloudHRMS.Domain.Models.ViewModels;
using CloudHRMS.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudHRMS.Controllers {
    public class DailyAttendanceController : Controller {
        private readonly IDailyAttendanceService _dailyAttendanceService;
        public DailyAttendanceController(IDailyAttendanceService dailyAttendanceService) {
            this._dailyAttendanceService = dailyAttendanceService;
        }

        public IActionResult Entry() {
            IList<DepartmentViewModel> departmentList = _dailyAttendanceService.GetDepartments();
            IList<EmployeeViewModel> employees = _dailyAttendanceService.GetEmployees();
            DailyAttendanceViewModel dailyAttendance = new DailyAttendanceViewModel() { Departments = departmentList, Employees = employees };
            return View(dailyAttendance);
        }
        public IActionResult List() {
            return View(_dailyAttendanceService.GetAll());
        }
        [HttpPost]
        public IActionResult Entry(DailyAttendanceViewModel dailyAttendanceViewModel) {
            try {
                _dailyAttendanceService.Create(dailyAttendanceViewModel);
                TempData["Msg"] = "DailyAttendance record is created successfully.";
            }
            catch (Exception) {
                TempData["Msg"] = "Error occurs when DailyAttendance record is created.";
            }
            return RedirectToAction("List");
        }
        public JsonResult GetEmployeeList(string departmentId) {
            IList<EmployeeViewModel> employeeList = _dailyAttendanceService.GetEmployeeList(departmentId);
            DailyAttendanceViewModel dailyAttendance = new DailyAttendanceViewModel() { Employees = employeeList };
            return Json(employeeList);
        }
        public IActionResult DeletebyId(string id) {
            try {
                _dailyAttendanceService.Delete(id);
                TempData["Msg"] = "DailyAttendance record is deleted successfully.";
            }
            catch (Exception ex) {
                TempData["Msg"] = "Error occurs when DailyAttendance record is deleted.";
            }
            return RedirectToAction("List");
        }
        public IActionResult Edit(string Id) {
            IList<DepartmentViewModel> departmentList = _dailyAttendanceService.GetDepartments();
            IList<EmployeeViewModel> employees = _dailyAttendanceService.GetEmployees();
            DailyAttendanceViewModel dailyAttendance = _dailyAttendanceService.GetBy(Id);
            dailyAttendance.Departments = departmentList;
            dailyAttendance.Employees = employees;
            return View(dailyAttendance);
        }
        [HttpPost]
        public IActionResult Update(DailyAttendanceViewModel dailyAttendanceViewModel) {
            try {
                _dailyAttendanceService.Update(dailyAttendanceViewModel);
                TempData["Msg"] = "DailyAttendance record is updated SUCCESSFULLY.";
            }
            catch (Exception ex) {
                TempData["Msg"] = "Error occurs when DailyAttendance record is updated.";
            }
            return RedirectToAction("List");
        }
    }
}