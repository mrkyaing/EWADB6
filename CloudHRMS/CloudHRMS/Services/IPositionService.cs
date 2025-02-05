using CloudHRMS.Models.ViewModels;

namespace CloudHRMS.Services {
    public interface IPositionService {
        void Create(PositionViewModel positionViewModel);
        IEnumerable<PositionViewModel> GetAll();
        PositionViewModel GetById(string id);
        void Update(PositionViewModel positionViewModel);
        bool Delete(string id);
    }
}
