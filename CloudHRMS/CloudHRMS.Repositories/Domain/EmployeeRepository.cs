using CloudHRMS.Domain.DAO;
using CloudHRMS.Domain.Models.DataModels;
using CloudHRMS.Repositories.Common;

namespace CloudHRMS.Repositories.Domain {
    public class EmployeeRepository : BaseRepository<EmployeeEntity>, IEmployeeRepository {
        private readonly HRMSDbContext _context;
        public EmployeeRepository(HRMSDbContext dbContext) : base(dbContext) {
            _context = dbContext;
        }
    }
}
