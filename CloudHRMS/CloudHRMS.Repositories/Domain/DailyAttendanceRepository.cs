using CloudHRMS.Domain.DAO;
using CloudHRMS.Domain.Models.DataModels;
using CloudHRMS.Repositories.Common;

namespace CloudHRMS.Repositories.Domain {
    public class DailyAttendanceRepository : BaseRepository<DailyAttendanceEntity>, IDailyAttendanceRepository {
        private readonly HRMSDbContext _hRMSDBContext;
        public DailyAttendanceRepository(HRMSDbContext hRMSDBContext) : base(hRMSDBContext) {
        }
    }

}