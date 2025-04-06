using CloudHRMS.Domain.Models.ViewModels;
namespace CloudHRMS.Services {
    public interface IAttendancePolicyService {
        void Cerate(AttendancePolicyViewModel attendancePolicyViewModel);
        IEnumerable<AttendancePolicyViewModel> GetAll();
        AttendancePolicyViewModel GetById(string id);
        void Update(AttendancePolicyViewModel attendancePolicyViewModel);
        bool Delete(string id);
    }
}