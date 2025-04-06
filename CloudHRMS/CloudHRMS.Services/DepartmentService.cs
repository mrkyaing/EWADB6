using CloudHRMS.Domain.Models.DataModels;
using CloudHRMS.Domain.Models.ViewModels;
using CloudHRMS.Domain.Utlitity;
using CloudHRMS.UnitOfWorks;
namespace CloudHRMS.Services {
    public class DepartmentService : IDepartmentService {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentService(IUnitOfWork unitOfWork) {
            this._unitOfWork = unitOfWork;
        }

        public void Create(DepartmentViewModel departmentViewModel) {
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
                _unitOfWork.DepartmentRepository.Create(departmentEntity);
                _unitOfWork.Commit();
            }
            catch (Exception e) {
                _unitOfWork.Rollback();
            }
        }

        public bool Delete(string id) {
            try {
                DepartmentEntity department = _unitOfWork.DepartmentRepository.GetBy(w => w.IsActive && w.Id == id).SingleOrDefault();
                if (department is not null) {
                    department.IsActive = false;
                    _unitOfWork.DepartmentRepository.Update(department);
                    _unitOfWork.Commit();
                    return true;
                }
            }
            catch (Exception e) {
                _unitOfWork.Rollback();
            }
            return false;
        }

        public IEnumerable<DepartmentViewModel> GetAll() {
            return _unitOfWork.DepartmentRepository.GetAll(w => w.IsActive).Select(
                     s => new DepartmentViewModel() {
                         // in view model required daclared
                         Id = s.Id,
                         Code = s.Code,
                         Description = s.Description,
                         ExtensionPhone = s.ExtensionPhone,
                     }).AsEnumerable();
        }

        public DepartmentViewModel GetById(string id) {
            return _unitOfWork.DepartmentRepository.GetBy(w => w.IsActive && w.Id == id).Select(
                 s => new DepartmentViewModel() {
                     // in view model required daclared
                     Id = s.Id,
                     Code = s.Code,
                     Description = s.Description,
                     ExtensionPhone = s.ExtensionPhone,
                 }).SingleOrDefault();
        }

        public void Update(DepartmentViewModel departmentViewModel) {
            try {
                DepartmentEntity department = _unitOfWork.DepartmentRepository.GetBy(w => w.IsActive && w.Id == departmentViewModel.Id).SingleOrDefault(); // take from data server
                if (department is not null) {
                    // change data  UI ( Viewmodel) to datamodel (DB/ Server)
                    department.Description = departmentViewModel.Description;
                    department.ExtensionPhone = departmentViewModel.ExtensionPhone;

                    department.UpdatedAt = DateTime.Now;// id is generated in background in DB from excel sheet 
                    department.UpdatedBy = "system";
                    department.Ip = NetworkHelper.GetIpAddress();
                    _unitOfWork.DepartmentRepository.Create(department);
                    _unitOfWork.Commit();
                }
            }
            catch (Exception e) {
                _unitOfWork.Rollback();
            }
        }
    }
}
