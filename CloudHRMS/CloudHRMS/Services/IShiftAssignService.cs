using CloudHRMS.Models.ViewModels;

namespace CloudHRMS.Services {
    public interface IShiftAssignService {
        void Create(ShiftAssignViewModel entity);
        IEnumerable<ShiftAssignViewModel> GetAll();
        ShiftAssignViewModel GetBy(string Id);
        void Update(ShiftAssignViewModel entity);
        void Delete(string id);
        public IList<EmployeeViewModel> GetEmployeeViewModelsList();
    }
}