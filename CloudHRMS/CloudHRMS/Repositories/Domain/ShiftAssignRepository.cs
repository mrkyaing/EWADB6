using CloudHRMS.DAO;
using CloudHRMS.Models.DataModels;
using CloudHRMS.Repositories.Common;

namespace CloudHRMS.Repositories.Domain {
    public class ShiftAssignRepository : BaseRepository<ShiftAssignEntity>, IShiftAssignRepository {
        private readonly HRMSDbContext _hRMSDBContext;
        public ShiftAssignRepository(HRMSDbContext hRMSDBContext) : base(hRMSDBContext) {
        }
    }

}