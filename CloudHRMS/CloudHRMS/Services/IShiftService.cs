using CloudHRMS.Models.ViewModels;

namespace CloudHRMS.Services {
    public interface IShiftService {
        void Create(ShiftViewModel entity);
        IEnumerable<ShiftViewModel> GetAll();
        ShiftViewModel GetBy(string Id);
        void Update(ShiftViewModel entity);
        void Delete(string id);
        public IList<AttendancePolicyViewModel> GetAttendancePolicyViewModelsList();
    }
}