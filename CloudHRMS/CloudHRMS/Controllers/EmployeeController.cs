using CloudHRMS.Models.ViewModels;
using CloudHRMS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
namespace CloudHRMS.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IPositionService _positionService;
        private readonly IDepartmentService _departmentService;
        public EmployeeController(IEmployeeService employeeService, IPositionService positionService, IDepartmentService departmentService)
        {
            _employeeService = employeeService;
            this._positionService = positionService;
            this._departmentService = departmentService;
        }

        [Authorize(Roles = "EMPLOYEE,HR")]
        //Reterived : Employee List
        public async Task<IActionResult> List()
        {
            var loginUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);// will give the user's userId
            return View(await _employeeService.GetAll(loginUserId));
        }
        [Authorize(Roles = "HR")]
        //C:Create 
        public IActionResult Entry()
        {
            EmployeeViewModel employeeViewModel = new EmployeeViewModel();
            employeeViewModel.PositionViewModels = _positionService.GetAll().Select(s => new PositionViewModel
            {
                Id = s.Id,
                Code = s.Code
            }).ToList();

            employeeViewModel.DepartmentViewModels = _departmentService.GetAll().Select(s => new DepartmentViewModel
            {
                Id = s.Id,
                Code = s.Code
            }).ToList();

            return View(employeeViewModel);
        }
        [Authorize(Roles = "HR")]
        [HttpPost]
        public async Task<IActionResult> Entry(EmployeeViewModel employeeViewModel)
        {
            try
            {
                var loginUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);// will give the user's userId
                await _employeeService.Create(employeeViewModel, loginUserId);
                TempData["Msg"] = "Employee record is created successfully.";
            }
            catch (Exception e)
            {
                TempData["Msg"] = "Error occurs when employee record is created.";//show the Status messages in ENGLISH ONLY (we don't support multi-languages)
            }
            return RedirectToAction("List");
        }
        [Authorize(Roles = "HR")]
        //D: Delete
        public IActionResult DeleteById(string id)
        {//1
            try
            {
                _employeeService.Delete(id);
                TempData["Msg"] = "Employee record is deleted successfully.";
            }
            catch (Exception e)
            {
                TempData["Msg"] = "Error occurs when employee record is deleted.";
            }
            return RedirectToAction("List");
        }

        [Authorize(Roles = "HR")]
        //U:Update
        public IActionResult Edit(string id)
        {
            var employee = _employeeService.GetById(id);
            employee.PositionViewModels = _positionService.GetAll().Select(s => new PositionViewModel
            {
                Id = s.Id,
                Code = s.Code
            }).ToList();
            employee.DepartmentViewModels = _departmentService.GetAll().Select(s => new DepartmentViewModel
            {
                Id = s.Id,
                Code = s.Code
            }).ToList();

            return View(employee);
        }
        [Authorize(Roles = "HR")]
        [HttpPost]
        public IActionResult Update(EmployeeViewModel employeeViewModel)
        {
            try
            {
                var loginUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);// will give the user's userId
                _employeeService.Update(employeeViewModel, loginUserId);
                TempData["Msg"] = "Employee record is updated successfully.";
            }
            catch (Exception e)
            {
                TempData["Msg"] = "Error occurs when employee record is updated.";
            }
            return RedirectToAction("List");
        }
    }
}
