using CloudHRMS.Models.DataModels;
using CloudHRMS.Models.ViewModels;
using CloudHRMS.UnitOfWorks;
using CloudHRMS.Utlitity;

namespace CloudHRMS.Services {
    public class DailyAttendanceService : IDailyAttendanceService {
        private readonly IUnitOfWork _unitOfWork;
        public DailyAttendanceService(IUnitOfWork unitOfWork) {
            this._unitOfWork = unitOfWork;
        }
        public IList<DepartmentViewModel> GetDepartments() {
            return _unitOfWork.DepartmentRepository.GetAll(w => w.IsActive).Select(x => new DepartmentViewModel {
                Id = x.Id,
                Code = x.Code,
                Description = x.Description
            }).ToList();
        }
        public IList<EmployeeViewModel> GetEmployees() {
            return _unitOfWork.EmployeeRepository.GetAll(w => w.IsActive).Select(x => new EmployeeViewModel {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name
            }).ToList();
        }
        public IList<EmployeeViewModel> GetEmployeeList(string departmentId) {
            return _unitOfWork.EmployeeRepository.GetAll(w => w.IsActive && w.DepartmentId == departmentId).Select(x => new EmployeeViewModel {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name
            }).ToList();
        }
        public void Create(DailyAttendanceViewModel entity) {
            try {
                DailyAttendanceEntity dailyAttendance = new DailyAttendanceEntity {
                    Id = Guid.NewGuid().ToString(),
                    EmployeeId = entity.EmployeeId,
                    DepartmentId = entity.DepartmentId,
                    InTime = entity.InTime,
                    OutTime = entity.OutTime,
                    AttendanceDate = entity.AttendanceDate,
                    CreatedBy = "system",
                    CreatedAt = DateTime.Now,
                    Ip = NetworkHelper.GetIpAddress(),
                    IsActive = true
                };
                _unitOfWork.DailyAttendanceRepository.Create(dailyAttendance);
                _unitOfWork.Commit();
            }
            catch (Exception ex) {
                _unitOfWork.Rollback();
            }
        }

        public void Delete(string id) {
            try {
                DailyAttendanceEntity dailyAttendance = _unitOfWork.DailyAttendanceRepository.GetBy(w => w.Id == id && w.IsActive).FirstOrDefault();
                if (dailyAttendance != null) {
                    dailyAttendance.IsActive = false;
                    dailyAttendance.Ip = NetworkHelper.GetIpAddress();
                    _unitOfWork.DailyAttendanceRepository.Update(dailyAttendance);
                    _unitOfWork.Commit();
                }
            }
            catch (Exception ex) {
                _unitOfWork.Rollback();
            }
        }

        public IEnumerable<DailyAttendanceViewModel> GetAll() {
            IList<DailyAttendanceViewModel> dailyAttendanceViewModels = (from da in _unitOfWork.DailyAttendanceRepository.GetAll(w => w.IsActive)
                                                                         join d in _unitOfWork.DepartmentRepository.GetAll(w => w.IsActive)
                                                                        on da.DepartmentId equals d.Id
                                                                         join e in _unitOfWork.EmployeeRepository.GetAll(w => w.IsActive)
                                                                         on da.EmployeeId equals e.Id
                                                                         select new DailyAttendanceViewModel {
                                                                             Id = da.Id,
                                                                             EmployeeId = da.EmployeeId,
                                                                             DepartmentId = da.DepartmentId,
                                                                             InTime = da.InTime,
                                                                             OutTime = da.OutTime,
                                                                             AttendanceDate = da.AttendanceDate,
                                                                             DepartmentName = d.Description,
                                                                             EmployeeName = e.Name
                                                                         }).ToList();
            return dailyAttendanceViewModels;
        }

        public DailyAttendanceViewModel GetBy(string Id) {
            DailyAttendanceViewModel dailyAttendanceViewModel = _unitOfWork.DailyAttendanceRepository.GetBy(w => w.Id == Id && w.IsActive).Select(x => new DailyAttendanceViewModel {
                Id = x.Id,
                EmployeeId = x.EmployeeId,
                DepartmentId = x.DepartmentId,
                InTime = x.InTime,
                OutTime = x.OutTime,
                AttendanceDate = x.AttendanceDate
            }).FirstOrDefault();
            return dailyAttendanceViewModel;
        }

        public void Update(DailyAttendanceViewModel entity) {
            try {
                DailyAttendanceEntity dailyAttendance = _unitOfWork.DailyAttendanceRepository.GetBy(w => w.Id == entity.Id && w.IsActive).FirstOrDefault();
                if (dailyAttendance != null) {
                    dailyAttendance.EmployeeId = entity.EmployeeId;
                    dailyAttendance.DepartmentId = entity.DepartmentId;
                    dailyAttendance.InTime = entity.InTime;
                    dailyAttendance.OutTime = entity.OutTime;
                    dailyAttendance.AttendanceDate = entity.AttendanceDate;
                    dailyAttendance.UpdatedBy = "system";
                    dailyAttendance.UpdatedAt = DateTime.Now;
                    dailyAttendance.Ip = NetworkHelper.GetIpAddress();
                    _unitOfWork.DailyAttendanceRepository.Update(dailyAttendance);
                    _unitOfWork.Commit();
                }
            }
            catch (Exception ex) {
                _unitOfWork.Rollback();
            }
        }
    }
}
