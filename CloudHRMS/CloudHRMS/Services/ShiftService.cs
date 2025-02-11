using CloudHRMS.Models.DataModels;
using CloudHRMS.Models.ViewModels;
using CloudHRMS.UnitOfWorks;
using CloudHRMS.Utlitity;


namespace CloudHRMS.Services {
    public class ShiftService : IShiftService {
        private readonly IUnitOfWork _unitOfWork;

        public ShiftService(IUnitOfWork unitOfWork) {
            this._unitOfWork = unitOfWork;
        }
        public IList<AttendancePolicyViewModel> GetAttendancePolicyViewModelsList() {
            IList<AttendancePolicyViewModel> attendancePolicyViewModels = _unitOfWork.AttendancePolicyRepository.GetAll(w => w.IsActive).Select
                (A => new AttendancePolicyViewModel {
                    Id = A.Id,
                    Name = A.Name,
                }).ToList();
            return attendancePolicyViewModels;
        }
        public void Create(ShiftViewModel entity) {

            try {
                ShiftEntity shift = new ShiftEntity {
                    Id = Guid.NewGuid().ToString(),
                    Name = entity.Name,
                    InTime = entity.InTime,
                    OutTime = entity.OutTime,
                    LateAfter = entity.LateAfter,
                    EarlyOutBefore = entity.EarlyOutBefore,
                    AttendancePolicyId = entity.AttendancePolicyId,
                    CreatedBy = "system",
                    CreatedAt = DateTime.Now,
                    Ip = NetworkHelper.GetIpAddress(),
                    IsActive = true
                };
                _unitOfWork.ShiftRepository.Create(shift);
                _unitOfWork.Commit();
            }
            catch (Exception ex) {
                _unitOfWork.Rollback();
            }
        }

        public void Delete(string id) {
            try {
                ShiftEntity shiftEntity = _unitOfWork.ShiftRepository.GetBy(w => w.IsActive && w.Id == id).SingleOrDefault();
                if (shiftEntity != null) {
                    shiftEntity.IsActive = false;
                    _unitOfWork.ShiftRepository.Delete(shiftEntity);
                    _unitOfWork.Commit();
                }
            }
            catch (Exception) {
                _unitOfWork.Rollback();
            }
        }

        public IEnumerable<ShiftViewModel> GetAll() {
            IList<ShiftViewModel> shiftViewModels = (from S in _unitOfWork.ShiftRepository.GetAll(w => w.IsActive)
                                                     join a in _unitOfWork.AttendancePolicyRepository.GetAll(w => w.IsActive)
                                                     on S.AttendancePolicyId equals a.Id
                                                     select new ShiftViewModel {
                                                         Id = S.Id,
                                                         Name = S.Name,
                                                         InTime = S.InTime,
                                                         OutTime = S.OutTime,
                                                         LateAfter = S.LateAfter,
                                                         EarlyOutBefore = S.EarlyOutBefore,
                                                         AttendancePolicyId = S.AttendancePolicyId,
                                                         AttendancePolicyInfo = a.Name
                                                     }).ToList();
            return shiftViewModels;
        }

        public ShiftViewModel GetBy(string Id) {
            ShiftViewModel shiftViewModel = (from S in _unitOfWork.ShiftRepository.GetAll(w => w.IsActive)
                                             join a in _unitOfWork.AttendancePolicyRepository.GetAll(w => w.IsActive)
                                             on S.AttendancePolicyId equals a.Id
                                             select new ShiftViewModel {
                                                 Id = S.Id,
                                                 Name = S.Name,
                                                 InTime = S.InTime,
                                                 OutTime = S.OutTime,
                                                 LateAfter = S.LateAfter,
                                                 EarlyOutBefore = S.EarlyOutBefore,
                                                 AttendancePolicyId = S.AttendancePolicyId,
                                                 AttendancePolicyInfo = a.Name
                                             }).SingleOrDefault();
            return shiftViewModel;
        }

        public void Update(ShiftViewModel entity) {
            try {
                ShiftEntity shiftEntity = _unitOfWork.ShiftRepository.GetBy(w => w.IsActive && w.Id == entity.Id).SingleOrDefault();
                if (shiftEntity != null) {
                    shiftEntity.Name = entity.Name;
                    shiftEntity.InTime = entity.InTime;
                    shiftEntity.OutTime = entity.OutTime;
                    shiftEntity.LateAfter = entity.LateAfter;
                    shiftEntity.EarlyOutBefore = entity.EarlyOutBefore;
                    shiftEntity.AttendancePolicyId = entity.AttendancePolicyId;
                    shiftEntity.UpdatedBy = "system";
                    shiftEntity.UpdatedAt = DateTime.Now;
                    shiftEntity.Ip = NetworkHelper.GetIpAddress();
                    _unitOfWork.ShiftRepository.Update(shiftEntity);
                    _unitOfWork.Commit();
                }
            }
            catch (Exception ex) {
                _unitOfWork.Rollback();
            }
        }
    }
}