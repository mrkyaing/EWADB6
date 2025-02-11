using CloudHRMS.Models.ViewModels;

namespace CloudHRMS.Services {
    public interface IAttendanceMasterService {
        void DayEndProcess(AttendanceMasterViewModel entity);
        IEnumerable<AttendanceMasterViewModel> GetAll();
        void Delete(DateTime attendanceDate, string departmentId, string employeeId);

    }
}