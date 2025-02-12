using CloudHRMS.Models.ViewModels;

namespace CloudHRMS.Services
{
    public interface IPayrollService
    {
        void PayrollProcess(PayrollProcessViewModel ui);
        IEnumerable<PayrollViewModel> GetAll();
        void Delete(DateTime fromDate, DateTime toDate, string departmentId, string employeeId);

    }
}