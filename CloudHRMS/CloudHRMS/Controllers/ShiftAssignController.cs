
using CloudHRMS.Domain.Models.ViewModels;
using CloudHRMS.Domain.Services;
using CloudHRMS.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CloudHRMS.Controllers {
    public class ShiftAssignController : Controller {
        private readonly IShiftAssignService _shiftAssignService;
        private readonly IEmployeeService _employeeService;
        private readonly IShiftService _shiftService;

        public ShiftAssignController(IShiftAssignService shiftAssignService, IEmployeeService employeeService, IShiftService shiftService) {
            this._shiftAssignService = shiftAssignService;
            this._employeeService = employeeService;
            this._shiftService = shiftService;
        }
        public IActionResult Entry() {
            ShiftAssignViewModel shiftAssignViewModel = new ShiftAssignViewModel();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            shiftAssignViewModel.Employees = _shiftAssignService.GetEmployeeViewModelsList();
            shiftAssignViewModel.Shifts = _shiftService.GetAll().Select(s => new ShiftViewModel {
                Id = s.Id,
                Name = s.Name
            }).ToList();

            return View(shiftAssignViewModel);
        }
        [HttpPost]
        public IActionResult Entry(ShiftAssignViewModel shiftAssignViewModel) {
            try {
                _shiftAssignService.Create(shiftAssignViewModel);
                TempData["Msg"] = "ShiftAssign record is created successfully.";
            }
            catch (Exception) {
                TempData["Msg"] = "Error occurs when ShiftAssign record is created.";
            }
            return RedirectToAction("List");
        }
        public IActionResult List() {
            return View(_shiftAssignService.GetAll());
        }
        public IActionResult DeletebyId(string id) {
            try {
                _shiftAssignService.Delete(id);
                TempData["Msg"] = "Position record is deleted successfully.";

            }
            catch (Exception ex) {
                TempData["Msg"] = "Error occurs when Position record is deleted.";
            }
            return RedirectToAction("List");
        }
        public IActionResult Edit(string Id) {
            ShiftAssignViewModel shiftAssignViewModel = _shiftAssignService.GetBy(Id);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            shiftAssignViewModel.Employees = _shiftAssignService.GetEmployeeViewModelsList();
            shiftAssignViewModel.Shifts = _shiftService.GetAll().Select(s => new ShiftViewModel {
                Id = s.Id,
                Name = s.Name
            }).ToList();
            return View(shiftAssignViewModel);
        }
        [HttpPost]
        public IActionResult Update(ShiftAssignViewModel shiftAssignViewModel) {
            try {
                _shiftAssignService.Update(shiftAssignViewModel);
                TempData["Msg"] = "Position record is updated SUCCESSFULLY.";
            }
            catch (Exception ex) {
                TempData["Msg"] = "Error occurs when Position record is updated.";
            }
            return RedirectToAction("List");
        }
    }
}