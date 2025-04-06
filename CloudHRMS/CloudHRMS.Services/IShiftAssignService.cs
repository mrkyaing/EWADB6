using CloudHRMS.Domain.Models.ViewModels;

namespace CloudHRMS.Domain.Services {
    public interface IShiftAssignService {
        void Create(ShiftAssignViewModel entity);
        IEnumerable<ShiftAssignViewModel> GetAll();
        ShiftAssignViewModel GetBy(string Id);
        void Update(ShiftAssignViewModel entity);
        void Delete(string id);
        public IList<EmployeeViewModel> GetEmployeeViewModelsList();
    }
}