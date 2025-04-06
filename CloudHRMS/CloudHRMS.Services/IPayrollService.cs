using CloudHRMS.Domain.Models.ViewModels;
namespace CloudHRMS.Domain.Services {
    public interface IPayrollService {
        void PayrollProcess(PayrollProcessViewModel ui);
        IEnumerable<PayrollViewModel> GetAll();
        void Delete(DateTime fromDate, DateTime toDate, string departmentId, string employeeId);
    }
}