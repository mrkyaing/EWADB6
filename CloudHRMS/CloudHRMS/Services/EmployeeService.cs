using CloudHRMS.Models.ReportModels;
using CloudHRMS.UnitOfWorks;

namespace CloudHRMS.Services {
    public class EmployeeService : IEmployeeService {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(IUnitOfWork unitOfWork) {
            this._unitOfWork = unitOfWork;
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
    }
}
