using CloudHRMS.Models.DataModels;
using CloudHRMS.Models.ViewModels;
using CloudHRMS.UnitOfWorks;
using CloudHRMS.Utlitity;

namespace CloudHRMS.Services {
    public class AttendancePolicyService : IAttendancePolicyService {
        private readonly IUnitOfWork _unitOfWork;
        public AttendancePolicyService(IUnitOfWork unitOfWork) {
            this._unitOfWork = unitOfWork;
        }
        public void Cerate(AttendancePolicyViewModel attendancePolicyViewModel) {
            try {
                var attendancePolicyEntity = new AttendancePolicyEntity() {
                    Id = Guid.NewGuid().ToString(),
                    Name = attendancePolicyViewModel.Name,
                    NumberOfLateTime = attendancePolicyViewModel.NumberOfLateTime,
                    NumberOfEarlyOutTime = attendancePolicyViewModel.NumberOfEarlyOutTime,
                    DeductionInAmount = attendancePolicyViewModel.DeductionInAmount,
                    DeductionInDay = attendancePolicyViewModel.DeductionInDay,
                    CreatedAt = DateTime.Now,
                    CreatedBy = "system",
                    IsActive = true,
                    Ip = NetworkHelper.GetIpAddress()
                };
                _unitOfWork.AttendancePolicyRepository.Create(attendancePolicyEntity);
                _unitOfWork.Commit();
            }
            catch (Exception ex) {
                _unitOfWork.Rollback();
            }
        }

        public bool Delete(string id) {
            try {
                AttendancePolicyEntity attendancePolicyEntity = _unitOfWork.AttendancePolicyRepository.GetBy(w => w.IsActive && w.Id == id).SingleOrDefault();
                if (attendancePolicyEntity is not null) {
                    attendancePolicyEntity.IsActive = false;
                    _unitOfWork.AttendancePolicyRepository.Update(attendancePolicyEntity);
                    _unitOfWork.Commit();
                    return true;
                }
            }
            catch (Exception ex) {
                _unitOfWork.Rollback();
            }
            return false;
        }

        public IEnumerable<AttendancePolicyViewModel> GetAll() {
            IList<AttendancePolicyViewModel> attendancePolicyViewModels = _unitOfWork.AttendancePolicyRepository.GetAll(w => w.IsActive).Select
                (s => new AttendancePolicyViewModel() {
                    Id = s.Id,
                    Name = s.Name,
                    NumberOfEarlyOutTime = s.NumberOfEarlyOutTime,
                    NumberOfLateTime = s.NumberOfLateTime,
                    DeductionInAmount = s.DeductionInAmount,
                    DeductionInDay = s.DeductionInDay
                }).ToList();
            return attendancePolicyViewModels;
        }

        public AttendancePolicyViewModel GetById(string id) {
            AttendancePolicyViewModel attendancePolicyViewModel = _unitOfWork.AttendancePolicyRepository.GetBy(w => w.IsActive && w.Id == id).Select
                (s => new AttendancePolicyViewModel() {
                    Id = s.Id,
                    Name = s.Name,
                    NumberOfEarlyOutTime = s.NumberOfEarlyOutTime,
                    NumberOfLateTime = s.NumberOfLateTime,
                    DeductionInAmount = s.DeductionInAmount,
                    DeductionInDay = s.DeductionInDay
                }).SingleOrDefault();
            return attendancePolicyViewModel;
        }

        public void Update(AttendancePolicyViewModel attendancePolicyViewModel) {
            try {
                AttendancePolicyEntity attendancePolicyEntity = _unitOfWork.AttendancePolicyRepository.GetBy(w => w.IsActive && w.Id == attendancePolicyViewModel.Id).SingleOrDefault();
                if (attendancePolicyEntity is not null) {
                    attendancePolicyEntity.Name = attendancePolicyViewModel.Name;
                    attendancePolicyEntity.NumberOfEarlyOutTime = attendancePolicyViewModel.NumberOfEarlyOutTime;
                    attendancePolicyEntity.NumberOfLateTime = attendancePolicyViewModel.NumberOfLateTime;
                    attendancePolicyEntity.DeductionInAmount = attendancePolicyViewModel.DeductionInAmount;
                    attendancePolicyEntity.DeductionInDay = attendancePolicyViewModel.DeductionInDay;
                    attendancePolicyEntity.UpdatedBy = "system";
                    attendancePolicyEntity.UpdatedAt = DateTime.Now;
                    attendancePolicyEntity.Ip = NetworkHelper.GetIpAddress();
                    _unitOfWork.AttendancePolicyRepository.Update(attendancePolicyEntity);
                    _unitOfWork.Commit();
                }
            }
            catch (Exception ex) {
                _unitOfWork.Rollback();
            }
        }
    }

}