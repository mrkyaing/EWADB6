using CloudHRMS.Models.ViewModels;

namespace CloudHRMS.Services {
    public interface IPositionService {
        PositionViewModel Create(PositionViewModel positionViewModel);
        IEnumerable<PositionViewModel> GetAll();
        PositionViewModel GetById(string id);
        PositionViewModel Update(PositionViewModel positionViewModel);
        bool Delete(string id);
    }
}
