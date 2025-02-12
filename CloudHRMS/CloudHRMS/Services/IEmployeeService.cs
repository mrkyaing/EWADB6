using CloudHRMS.Models.ReportModels;
using CloudHRMS.Models.ViewModels;

namespace CloudHRMS.Services
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeDetailModel> DetailBy(string fromCode, string toCode);
        Task Create(EmployeeViewModel employeeViewModel, string loginUserId);
        Task<IList<EmployeeViewModel>> GetAll(string userId);
        EmployeeViewModel GetById(string id);
        void Update(EmployeeViewModel employeeViewModel, string loginUserId);
        bool Delete(string id);
    }
}
