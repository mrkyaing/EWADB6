using CloudHRMS.Domain.Models.ViewModels;
using CloudHRMS.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudHRMS.Controllers {
    public class ShiftController : Controller {
        private readonly IShiftService _shiftService;

        public ShiftController(IShiftService shiftService) {
            this._shiftService = shiftService;
        }
        public IActionResult Entry() {
            IList<AttendancePolicyViewModel> attendancePolicyViewModels = _shiftService.GetAttendancePolicyViewModelsList();
            ShiftViewModel shiftViewModel = new ShiftViewModel() { AttendancePolicies = attendancePolicyViewModels };
            return View(shiftViewModel);
        }
        [HttpPost]
        public IActionResult Entry(ShiftViewModel shiftViewModel) {
            try {
                _shiftService.Create(shiftViewModel);
                TempData["Msg"] = "Shift record is created successfully.";
            }
            catch (Exception) {
                TempData["Msg"] = "Error occurs when Shift record is created.";
            }
            return RedirectToAction("List");
        }
        public IActionResult List() {
            return View(_shiftService.GetAll());
        }
        public IActionResult DeletebyId(string id) {
            try {
                _shiftService.Delete(id);
                TempData["Msg"] = "Shift record is deleted successfully.";

            }
            catch (Exception ex) {
                TempData["Msg"] = "Error occurs when Shift record is deleted.";
            }
            return RedirectToAction("List");
        }
        public IActionResult Edit(string Id) {
            ShiftViewModel shiftViewModel = _shiftService.GetBy(Id);
            shiftViewModel.AttendancePolicies = _shiftService.GetAttendancePolicyViewModelsList();
            return View(shiftViewModel);
        }
        [HttpPost]
        public IActionResult Update(ShiftViewModel shiftViewModel) {
            try {
                _shiftService.Update(shiftViewModel);
                TempData["Msg"] = "Shift record is updated SUCCESSFULLY.";
            }
            catch (Exception ex) {
                TempData["Msg"] = "Error occurs when Shift record is updated.";
            }
            return RedirectToAction("List");
        }
    }
}