using CloudHRMS.Domain.Models.ViewModels;
using CloudHRMS.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudHRMS.Controllers {
    public class AttendancePolicyController : Controller {
        private readonly IAttendancePolicyService _attendancePolicyService;

        public AttendancePolicyController(IAttendancePolicyService attendancePolicyService) {
            this._attendancePolicyService = attendancePolicyService;
        }
        public IActionResult Entry() {
            return View();
        }
        [HttpPost]
        public IActionResult Entry(AttendancePolicyViewModel attendancePolicyViewModel) {
            try {
                _attendancePolicyService.Cerate(attendancePolicyViewModel);
                TempData["Msg"] = "The record of Attendance Policy is created successfully.";
            }
            catch (Exception) {
                TempData["Msg"] = "Error occurs when the record of Attendance Policy is created.";
            }
            return RedirectToAction("List");
        }
        public IActionResult List() {
            return View(_attendancePolicyService.GetAll());
        }
        public IActionResult DeletebyId(string id) {
            try {
                _attendancePolicyService.Delete(id);
                TempData["Msg"] = "The record of Attendance Policy is deleted successfully.";

            }
            catch (Exception ex) {
                TempData["Msg"] = "Error occurs when the record of Attendance Policy is deleted.";
            }
            return RedirectToAction("List");
        }
        public IActionResult Edit(string Id) {
            return View(_attendancePolicyService.GetById(Id));
        }
        [HttpPost]
        public IActionResult Update(AttendancePolicyViewModel attendancePolicyViewModel) {
            try {
                _attendancePolicyService.Update(attendancePolicyViewModel);
                ViewBag.Msg = "The record of Attendance Policy is updated SUCCESSFULLY.";
            }
            catch (Exception ex) {
                ViewBag.Msg = "Error occurs when The record of Attendance Policy is updated.";
            }
            return RedirectToAction("List");
        }
    }
}