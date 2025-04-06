using CloudHRMS.Domain.Models.ViewModels;

namespace CloudHRMS.Services {
    public interface IDepartmentService {
        void Create(DepartmentViewModel departmentViewModel);
        IEnumerable<DepartmentViewModel> GetAll();
        DepartmentViewModel GetById(string id);
        void Update(DepartmentViewModel departmentViewModel);
        bool Delete(string id);
    }
}
