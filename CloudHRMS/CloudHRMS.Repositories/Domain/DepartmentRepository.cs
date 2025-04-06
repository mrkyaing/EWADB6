using CloudHRMS.Domain.DAO;
using CloudHRMS.Domain.Models.DataModels;
using CloudHRMS.Repositories.Common;

namespace CloudHRMS.Repositories.Domain {
    public class DepartmentRepository : BaseRepository<DepartmentEntity>, IDepartmentRepository {
        private readonly HRMSDbContext _context;
        public DepartmentRepository(HRMSDbContext dbContext) : base(dbContext) {
            _context = dbContext;
        }
    }
}
