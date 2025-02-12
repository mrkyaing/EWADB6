using CloudHRMS.Models.DataModels;
using CloudHRMS.Models.ViewModels;
using CloudHRMS.UnitOfWorks;
using CloudHRMS.Utlitity;

namespace CloudHRMS.Services
{
    public class PositionService : IPositionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PositionService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public PositionViewModel Create(PositionViewModel positionViewModel, string loginUserId)
        {
            //data exchange from view model to data model
            try
            {
                var positionEntity = new PositionEntity()
                {
                    Id = Guid.NewGuid().ToString(),
                    Code = positionViewModel.Code,
                    Description = positionViewModel.Description,
                    Level = positionViewModel.Level,
                    CreatedAt = DateTime.Now,
                    CreatedBy = loginUserId,
                    IsActive = true,
                    Ip = NetworkHelper.GetIpAddress()
                };
                _unitOfWork.PositoryRepository.Create(positionEntity);
                _unitOfWork.Commit();
                return positionViewModel;
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        public bool Delete(string id)
        {
            try
            {
                PositionEntity positionEntity = _unitOfWork.PositoryRepository.GetBy(w => w.Id == id && w.IsActive).SingleOrDefault();
                if (positionEntity is not null)
                {
                    positionEntity.IsActive = false;
                    _unitOfWork.PositoryRepository.Update(positionEntity);
                    _unitOfWork.Commit();
                    return true;
                }
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
            }
            return false;
        }

        public IEnumerable<PositionViewModel> GetAll()
        {
            IList<PositionViewModel> positionViewMoel = _unitOfWork.PositoryRepository.GetAll(w => w.IsActive).Select(
                    s => new PositionViewModel()
                    {
                        Id = s.Id,
                        Code = s.Code,
                        Description = s.Description,
                        Level = s.Level
                    }).ToList();
            return positionViewMoel;
        }

        public PositionViewModel GetById(string id)
        {
            PositionViewModel positionViewMoel = _unitOfWork.PositoryRepository.GetBy(w => w.Id == id && w.IsActive).Select(s => new PositionViewModel()
            {
                Id = s.Id,
                Code = s.Code,
                Description = s.Description,
                Level = s.Level
            }).SingleOrDefault();
            return positionViewMoel;
        }

        public PositionViewModel Update(PositionViewModel positionViewModel, string loginUserId)
        {
            try
            {
                PositionEntity positionEntity = _unitOfWork.PositoryRepository.GetBy(w => w.IsActive && w.Id == positionViewModel.Id).SingleOrDefault();
                if (positionEntity is not null)
                {
                    positionEntity.Description = positionViewModel.Description;
                    positionEntity.Level = positionViewModel.Level;
                    positionEntity.UpdatedBy = loginUserId;
                    positionEntity.UpdatedAt = DateTime.Now;
                    positionEntity.Ip = NetworkHelper.GetIpAddress();
                }
                _unitOfWork.PositoryRepository.Update(positionEntity);
                _unitOfWork.Commit();
                return positionViewModel;
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }
    }
}
