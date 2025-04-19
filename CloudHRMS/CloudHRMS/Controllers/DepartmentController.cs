using CloudHRMS.Domain.Models.ViewModels;
using CloudHRMS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudHRMS.Controllers {

    public class DepartmentController : Controller {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService) {
            _departmentService = departmentService;
        }
        [Authorize(Roles = "EMPLOYEE,HR")]
        public IActionResult List() {
            return View(_departmentService.GetAll().ToList());
        }// end list

        [Authorize(Roles = "HR")]
        [HttpGet]
        public IActionResult Entry() {
            return View();
        }
        [Authorize(Roles = "HR")]
        [HttpPost]
        public IActionResult Entry(DepartmentViewModel departmentViewModel) {
            try {
                _departmentService.Create(departmentViewModel);
                TempData["Msg"] = "Department record is created successfully.";
            }
            catch (Exception e) {
                TempData["Msg"] = "Error Occur when department record is created.";
            }
            return RedirectToAction("List");
        }// end entry

        [Authorize(Roles = "HR")]
        public IActionResult DeleteById(string id) {
            try {
                _departmentService.Delete(id);
                TempData["Msg"] = "Department record is delected successfully";
            }
            catch (Exception e) {
                TempData["Msg"] = "Error occur when employee record is delected.";

            }
            return RedirectToAction("list"); // after deleting to show list view table
        }// end delete

        [Authorize(Roles = "HR")]
        public IActionResult Edit(string id)// Edit or Update(httpget)
        {

            return View(_departmentService.GetById(id));
        }

        [Authorize(Roles = "HR")]
        [HttpPost]
        public IActionResult Update(DepartmentViewModel departmentViewModel) {
            try {
                _departmentService.Update(departmentViewModel);
                TempData["Msg"] = "Department record is updated successfully.";
            }
            catch (Exception e) {
                TempData["Msg"] = "Error occur when Department record is updated.";
            }
            return RedirectToAction("List");
        }
    }
}