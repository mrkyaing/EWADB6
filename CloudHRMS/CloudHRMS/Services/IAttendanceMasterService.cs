using CloudHRMS.Models.ViewModels;

namespace CloudHRMS.Services
{
    public interface IAttendanceMasterService
    {
        void DayEndProcess(AttendanceMasterViewModel entity);
        IEnumerable<AttendanceMasterViewModel> GetAll();
        void Delete(DateTime froDate, DateTime toDate, string departmentId, string employeeId);

    }
}