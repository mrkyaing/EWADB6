using CloudHRMS.Models.ViewModels;

namespace CloudHRMS.Services {
    public interface IPayrollService {
        void PayrollProcess(PayrollProcessViewModel ui);
        IEnumerable<PayrollViewModel> GetAll();
        void Delete(DateTime attendanceDate, string departmentId, string employeeId);

    }
}