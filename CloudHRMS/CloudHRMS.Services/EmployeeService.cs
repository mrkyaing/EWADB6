using CloudHRMS.Domain.Models.DataModels;
using CloudHRMS.Domain.Models.ViewModels;
using CloudHRMS.Domain.Utlitity;
using CloudHRMS.Models.ReportModels;
using CloudHRMS.UnitOfWorks;
namespace CloudHRMS.Services {
    public class EmployeeService : IEmployeeService {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;
        public EmployeeService(IUnitOfWork unitOfWork, IUserService userService) {
            this._unitOfWork = unitOfWork;
            this._userService = userService;
        }

        public async Task Create(EmployeeViewModel employeeViewModel, string loginUserId) {
            try {
                var userId = await _userService.CreateUserAsync(employeeViewModel.Email, "CloudHRMS@123", employeeViewModel.Email);
                if (userId is not null) {
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
                        CreatedBy = loginUserId,
                        IsActive = true,
                        Ip = NetworkHelper.GetIpAddress(),
                        UserId = userId
                    };
                    _unitOfWork.EmployeeRepository.Create(employeeEntity);
                    _unitOfWork.Commit();
                }
            }
            catch (Exception e) {
                _unitOfWork.Rollback();
            }
        }

        public bool Delete(string id) {
            try {
                EmployeeEntity employee = _unitOfWork.EmployeeRepository.GetBy(w => w.IsActive && w.Id == id).SingleOrDefault();
                if (employee is not null) {
                    employee.IsActive = false;
                    _unitOfWork.EmployeeRepository.Update(employee);
                    _unitOfWork.Commit();
                    return true;
                }
            }
            catch (Exception e) {
                _unitOfWork.Rollback();
            }
            return false;
        }

        public IEnumerable<EmployeeDetailModel> DetailBy(string fromCode, string toCode) {
            return (from e in _unitOfWork.EmployeeRepository.GetAll(w => w.IsActive)
                    join p in _unitOfWork.PositoryRepository.GetAll(w => w.IsActive)
                    on e.PositionId equals p.Id
                    where e.Code.CompareTo(fromCode) >= 0 && e.Code.CompareTo(toCode) <= 0
                    select new EmployeeDetailModel {
                        Code = e.Code,
                        Name = e.Name,
                        Address = e.Address,
                        BasicSalary = e.BasicSalary,
                        DOE = e.DOE.ToString("yyyy-MM-dd"),
                        DOB = e.DOB.ToString("yyyy-MM-dd"),
                        DOR = e.DOR?.ToString("yyyy-MM-dd"),
                        Email = e.Email,
                        Phone = e.Phone,
                        Gender = e.Gender,
                        PositionInfo = p.Code + "/" + p.Description
                    });
        }

        public async Task<IList<EmployeeViewModel>> GetAll(string loginUserId) {
            IList<string> roles = new List<string>();
            if (!string.IsNullOrEmpty(loginUserId)) {
                roles = await _userService.GetRolesByUserIdAync(loginUserId);
            }
            //DTO >> data transfer object process in here  (Data Model =>viewModel)
            IList<EmployeeViewModel> employees = (from e in _unitOfWork.EmployeeRepository.GetAll(w => w.IsActive)
                                                  join d in _unitOfWork.DepartmentRepository.GetAll(w => w.IsActive)
                                                  on e.DepartmentId equals d.Id
                                                  join p in _unitOfWork.PositoryRepository.GetAll(w => w.IsActive)
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
                                                      PositionInfo = p.Code + "/" + p.Description,
                                                      UserId = e.UserId
                                                  }).ToList();
            if (roles.Contains("EMPLOYEE")) {
                employees = employees.Where(w => w.UserId == loginUserId).ToList();
            }
            return employees;
        }

        public EmployeeViewModel GetById(string id) {
            return _unitOfWork.EmployeeRepository.GetBy(w => w.IsActive && w.Id == id).Select(s => new EmployeeViewModel() {
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
        }

        public void Update(EmployeeViewModel employeeViewModel, string loginUserId) {
            try {
                EmployeeEntity employee = _unitOfWork.EmployeeRepository.GetBy(w => w.IsActive && w.Id == employeeViewModel.Id).SingleOrDefault();
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
                    employee.UpdatedBy = loginUserId;
                    employee.Ip = NetworkHelper.GetIpAddress();
                    employee.DepartmentId = employeeViewModel.DepartmentId;
                    employee.PositionId = employeeViewModel.PositionId;
                    _unitOfWork.EmployeeRepository.Update(employee);
                    _unitOfWork.Commit();
                }
            }
            catch (Exception e) {
                _unitOfWork.Rollback();
            }
        }
    }
}
