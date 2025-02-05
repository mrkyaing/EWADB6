using CloudHRMS.DAO;
using CloudHRMS.Models.DataModels;
using CloudHRMS.Models.ViewModels;
using CloudHRMS.Utlitity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudHRMS.Controllers {

    public class DepartmentController : Controller {
        private readonly HRMSDbContext _hRMSDbContext;

        public DepartmentController(HRMSDbContext hRMSDbContext) {
            _hRMSDbContext = hRMSDbContext;
        }
        [Authorize(Roles = "EMPLOYEE,HR")]
        public IActionResult List() {
            IList<DepartmentViewModel> department = _hRMSDbContext.Departments.Where(w => w.IsActive == true).Select(
                s => new DepartmentViewModel() {
                    // in view model required daclared
                    Id = s.Id,
                    Code = s.Code,
                    Description = s.Description,
                    ExtensionPhone = s.ExtensionPhone,
                }).ToList();

            return View(department);
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
                var departmentEntity = new DepartmentEntity() {
                    Id = Guid.NewGuid().ToString(),
                    Code = departmentViewModel.Code,
                    Description = departmentViewModel.Description,
                    ExtensionPhone = departmentViewModel.ExtensionPhone,

                    CreatedAt = DateTime.Now,
                    CreatedBy = "system",
                    IsActive = true,
                    Ip = NetworkHelper.GetIpAddress()
                };
                _hRMSDbContext.Departments.Add(departmentEntity);
                _hRMSDbContext.SaveChanges();
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
                DepartmentEntity department = _hRMSDbContext.Departments.Where(w => w.IsActive && w.Id == id).SingleOrDefault();
                if (department is not null) {
                    department.IsActive = false;
                    _hRMSDbContext.Departments.Update(department);
                    _hRMSDbContext.SaveChanges();
                    TempData["Msg"] = "Department record is delected successfully";
                }
            }
            catch (Exception e) {
                TempData["Msg"] = "Error occur when employee record is delected.";

            }
            return RedirectToAction("list"); // after deleting to show list view table
        }// end delete

        [Authorize(Roles = "HR")]
        public IActionResult Edit(string id)// Edit or Update(httpget)
        {
            DepartmentViewModel department = _hRMSDbContext.Departments.Where(w => w.IsActive && w.Id == id).Select(
                s => new DepartmentViewModel() {
                    // in view model required daclared
                    Id = s.Id,
                    Code = s.Code,
                    Description = s.Description,
                    ExtensionPhone = s.ExtensionPhone,
                }).SingleOrDefault();

            return View(department);
        }

        [Authorize(Roles = "HR")]
        [HttpPost]
        public IActionResult Update(DepartmentViewModel departmentViewModel) {
            try {
                DepartmentEntity department = _hRMSDbContext.Departments.Where(w => w.IsActive && w.Id == departmentViewModel.Id).SingleOrDefault(); // take from data server
                if (department is not null) {
                    // change data  UI ( Viewmodel) to datamodel (DB/ Server)
                    department.Description = departmentViewModel.Description;
                    department.ExtensionPhone = departmentViewModel.ExtensionPhone;

                    department.UpdatedAt = DateTime.Now;// id is generated in background in DB from excel sheet 
                    department.UpdatedBy = "system";
                    department.Ip = NetworkHelper.GetIpAddress();

                    _hRMSDbContext.Departments.Update(department);
                    _hRMSDbContext.SaveChanges();
                    TempData["Msg"] = "Department record is updated successfully.";

                }
            }
            catch (Exception e) {
                TempData["Msg"] = "Error occur when Department record is updated.";

            }
            return RedirectToAction("List");
        }
    }
}