using CloudHRMS.Domain.Models.ViewModels;

namespace CloudHRMS.Services {
    public interface IPositionService {
        PositionViewModel Create(PositionViewModel positionViewModel, string loginUserId);
        IEnumerable<PositionViewModel> GetAll();
        PositionViewModel GetById(string id);
        PositionViewModel Update(PositionViewModel positionViewModel, string loginUserId);
        bool Delete(string id);
    }
}
