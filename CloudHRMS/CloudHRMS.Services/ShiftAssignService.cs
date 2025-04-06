using CloudHRMS.Domain.Models.DataModels;
using CloudHRMS.Domain.Models.ViewModels;
using CloudHRMS.Domain.Services;
using CloudHRMS.Domain.Utlitity;
using CloudHRMS.UnitOfWorks;
namespace CloudHRMS.Services {
    public class ShiftAssignService : IShiftAssignService {
        private readonly IUnitOfWork _unitOfWork;

        public ShiftAssignService(IUnitOfWork unitOfWork) {
            this._unitOfWork = unitOfWork;
        }
        public void Create(ShiftAssignViewModel entity) {
            try {
                ShiftAssignEntity shiftAssign = new ShiftAssignEntity {
                    Id = Guid.NewGuid().ToString(),
                    EmployeeId = entity.EmployeeId,
                    ShiftId = entity.ShiftId,
                    FromDate = entity.FromDate,
                    ToDate = entity.ToDate,
                    CreatedBy = "system",
                    CreatedAt = DateTime.Now,
                    Ip = NetworkHelper.GetIpAddress(),
                    IsActive = true
                };
                _unitOfWork.ShiftAssignRepository.Create(shiftAssign);
                _unitOfWork.Commit();
            }
            catch (Exception ex) {
                _unitOfWork.Rollback();
            }
        }
        public void Delete(string id) {
            try {
                ShiftAssignEntity shiftAssignEntity = _unitOfWork.ShiftAssignRepository.GetBy(W => W.IsActive && W.Id == id).SingleOrDefault();
                if (shiftAssignEntity != null) {
                    shiftAssignEntity.IsActive = false;

                    shiftAssignEntity.Ip = NetworkHelper.GetIpAddress();
                    _unitOfWork.ShiftAssignRepository.Update(shiftAssignEntity);
                    _unitOfWork.Commit();
                }
            }
            catch {
                _unitOfWork.Rollback();
            }

        }
        public IEnumerable<ShiftAssignViewModel> GetAll() {
            IList<ShiftAssignViewModel> shiftAssigns = (from S in _unitOfWork.ShiftAssignRepository.GetAll(w => w.IsActive)
                                                        join a in _unitOfWork.EmployeeRepository.GetAll(w => w.IsActive)
                                                        on S.EmployeeId equals a.Id
                                                        join sh in _unitOfWork.ShiftRepository.GetAll(w => w.IsActive)
                                                        on S.ShiftId equals sh.Id
                                                        select new ShiftAssignViewModel {
                                                            Id = S.Id,
                                                            EmployeeId = S.EmployeeId,
                                                            ShiftId = S.ShiftId,
                                                            FromDate = S.FromDate,
                                                            ToDate = S.ToDate,
                                                            EmployeeName = a.Name,
                                                            ShiftName = sh.Name
                                                        }).ToList();
            return shiftAssigns;
        }
        public ShiftAssignViewModel GetBy(string Id) {
            ShiftAssignViewModel shiftAssignViewModel = (from S in _unitOfWork.ShiftAssignRepository.GetAll(w => w.IsActive)
                                                         join a in _unitOfWork.EmployeeRepository.GetAll(w => w.IsActive)
                                                         on S.EmployeeId equals a.Id
                                                         join sh in _unitOfWork.ShiftRepository.GetAll(w => w.IsActive)
                                                         on S.ShiftId equals sh.Id
                                                         select new ShiftAssignViewModel {
                                                             Id = S.Id,
                                                             EmployeeId = S.EmployeeId,
                                                             ShiftId = S.ShiftId,
                                                             FromDate = S.FromDate,
                                                             ToDate = S.ToDate,
                                                             EmployeeName = a.Name,
                                                             ShiftName = sh.Name
                                                         }).SingleOrDefault();
            return shiftAssignViewModel;
        }

        public IList<EmployeeViewModel> GetEmployeeViewModelsList() {
            return _unitOfWork.EmployeeRepository.GetAll(w => w.IsActive).Select(e => new EmployeeViewModel {
                Id = e.Id,
                Name = e.Name
            }).ToList();
        }

        public void Update(ShiftAssignViewModel entity) {
            try {
                ShiftAssignEntity shiftAssignEntity = _unitOfWork.ShiftAssignRepository.GetBy(w => w.IsActive && w.Id == entity.Id).SingleOrDefault();
                if (shiftAssignEntity != null) {
                    shiftAssignEntity.EmployeeId = entity.EmployeeId;
                    shiftAssignEntity.ShiftId = entity.ShiftId;
                    shiftAssignEntity.FromDate = entity.FromDate;
                    shiftAssignEntity.ToDate = entity.ToDate;
                    shiftAssignEntity.UpdatedBy = "system";
                    shiftAssignEntity.UpdatedAt = DateTime.Now;
                    shiftAssignEntity.IsActive = true;
                    shiftAssignEntity.Ip = NetworkHelper.GetIpAddress();
                    _unitOfWork.ShiftAssignRepository.Update(shiftAssignEntity);
                    _unitOfWork.Commit();
                }
            }
            catch (Exception ex) {
                _unitOfWork.Rollback();
            }
        }

    }
}