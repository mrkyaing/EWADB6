using CloudHRMS.DAO;
using CloudHRMS.Models.DataModels;
using CloudHRMS.Models.ViewModels;
using CloudHRMS.Utlitity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace CloudHRMS.Controllers {
    public class EmployeeController : Controller {

        private readonly HRMSDbContext _hRMSDbContext;
        public EmployeeController(HRMSDbContext hRMSDbContext) {
            _hRMSDbContext = hRMSDbContext;
        }
        [Authorize(Roles = "EMPLOYEE,HR")]
        //Reterived : Employee List
        public IActionResult List() {
            //DTO >> data transfer object process in here  (Data Model =>viewModel)
            IList<EmployeeViewModel> employees = (from e in _hRMSDbContext.Employees
                                                  join d in _hRMSDbContext.Departments
                                                  on e.DepartmentId equals d.Id
                                                  join p in _hRMSDbContext.Positions
                                                  on e.PositionId equals p.Id
                                                  where e.IsActive == true && p.IsActive == true && d.IsActive == true
                                                  select new EmployeeViewModel() {
                                                      Id = e.Id,//for delete and update porpose.
                                                      Code = e.Code,
                                                      Name = e.Name,
                                                      Address = e.Address,
                                                      BasicSalary = e.BasicSalary,
                                                      DOE = e.DOE,
                                                      DOB = e.DOB,
                                                      DOR = e.DOR,
                                                      Email = e.Email,
                                                      Phone = e.Phone,
                                                      Gender = e.Gender,
                                                      PositionId = e.PositionId,
                                                      DepartmentId = e.DepartmentId,
                                                      DepartmentInfo = d.Code + "/" + d.Description,
                                                      PositionInfo = p.Code + "/" + p.Description
                                                  }).ToList();
            return View(employees);
        }
        [Authorize(Roles = "HR")]
        //C:Create 
        public IActionResult Entry() {
            EmployeeViewModel employeeViewModel = new EmployeeViewModel();
            employeeViewModel.PositionViewModels = _hRMSDbContext.Positions.Where(w => w.IsActive).Select(s => new PositionViewModel {
                Id = s.Id,
                Code = s.Code
            }).ToList();

            employeeViewModel.DepartmentViewModels = _hRMSDbContext.Departments.Where(w => w.IsActive).Select(s => new DepartmentViewModel {
                Id = s.Id,
                Code = s.Code
            }).ToList();

            return View(employeeViewModel);
        }
        [Authorize(Roles = "HR")]
        [HttpPost]
        public IActionResult Entry(EmployeeViewModel employeeViewModel) {
            try {
                //DTO >> data transfer object process in here .
                var employeeEntity = new EmployeeEntity() {
                    Id = Guid.NewGuid().ToString(),//for primary key auto generation
                    Code = employeeViewModel.Code,
                    Name = employeeViewModel.Name,
                    Email = employeeViewModel.Email,
                    DOE = employeeViewModel.DOE,
                    DOB = employeeViewModel.DOB,
                    DOR = employeeViewModel.DOR,
                    Phone = employeeViewModel.Phone,
                    Address = employeeViewModel.Address,
                    BasicSalary = employeeViewModel.BasicSalary,
                    Gender = employeeViewModel.Gender,
                    PositionId = employeeViewModel.PositionId,
                    DepartmentId = employeeViewModel.DepartmentId,
                    //for audit purpose column
                    CreatedAt = DateTime.Now,
                    CreatedBy = "system",
                    IsActive = true,
                    Ip = NetworkHelper.GetIpAddress()
                };
                _hRMSDbContext.Employees.Add(employeeEntity);//collect the entity to the context
                _hRMSDbContext.SaveChanges();//actually save the data to the database
                TempData["Msg"] = "Employee record is created successfully.";
            }
            catch (Exception e) {
                TempData["Msg"] = "Error occurs when employee record is created.";//show the Status messages in ENGLISH ONLY (we don't support multi-languages)
            }
            return RedirectToAction("List");
        }
        [Authorize(Roles = "HR")]
        //D: Delete
        public IActionResult DeleteById(string id) {//1
            try {
                EmployeeEntity employee = _hRMSDbContext.Employees.Where(w => w.IsActive && w.Id == id).SingleOrDefault();
                if (employee is not null) {
                    employee.IsActive = false;
                    _hRMSDbContext.Employees.Update(employee);
                    _hRMSDbContext.SaveChanges();
                    TempData["Msg"] = "Employee record is deleted successfully.";
                }
            }
            catch (Exception e) {
                TempData["Msg"] = "Error occurs when employee record is deleted.";
            }
            return RedirectToAction("List");
        }

        [Authorize(Roles = "HR")]
        //U:Update
        public IActionResult Edit(string id) {
            EmployeeViewModel employee = _hRMSDbContext.Employees.Where(w => w.IsActive && w.Id == id).Select(s => new EmployeeViewModel() {
                Id = s.Id,//for delete and update porpose.
                Code = s.Code,
                Name = s.Name,
                Address = s.Address,
                BasicSalary = s.BasicSalary,
                DOE = s.DOE,
                DOB = s.DOB,
                DOR = s.DOR,
                Email = s.Email,
                Phone = s.Phone,
                Gender = s.Gender,
                PositionId = s.PositionId,
                DepartmentId = s.DepartmentId
            }).SingleOrDefault();
            employee.PositionViewModels = _hRMSDbContext.Positions.Where(w => w.IsActive).Select(s => new PositionViewModel {
                Id = s.Id,
                Code = s.Code
            }).ToList();
            employee.DepartmentViewModels = _hRMSDbContext.Departments.Where(w => w.IsActive).Select(s => new DepartmentViewModel {
                Id = s.Id,
                Code = s.Code
            }).ToList();
            return View(employee);
        }
        [Authorize(Roles = "HR")]
        [HttpPost]
        public IActionResult Update(EmployeeViewModel employeeViewModel) {
            try {
                EmployeeEntity employee = _hRMSDbContext.Employees.Where(w => w.IsActive && w.Id == employeeViewModel.Id).SingleOrDefault();
                if (employee is not null) {
                    employee.Name = employeeViewModel.Name;
                    employee.Address = employeeViewModel.Address;
                    employee.BasicSalary = employeeViewModel.BasicSalary;
                    employee.DOE = employeeViewModel.DOE;
                    employee.DOB = employeeViewModel.DOB;
                    employee.DOR = employeeViewModel.DOR;
                    employee.Phone = employeeViewModel.Phone;
                    employee.Gender = employeeViewModel.Gender;
                    //for audit purpose when update the recrod by the user.
                    employee.UpdatedAt = DateTime.Now;
                    employee.UpdatedBy = "system";
                    employee.Ip = NetworkHelper.GetIpAddress();
                    employee.DepartmentId = employeeViewModel.DepartmentId;
                    employee.PositionId = employeeViewModel.PositionId;
                    _hRMSDbContext.Employees.Update(employee);
                    _hRMSDbContext.SaveChanges();
                    TempData["Msg"] = "Employee record is updated successfully.";
                }
            }
            catch (Exception e) {
                TempData["Msg"] = "Error occurs when employee record is updated.";
            }
            return RedirectToAction("List");
        }
    }
}
