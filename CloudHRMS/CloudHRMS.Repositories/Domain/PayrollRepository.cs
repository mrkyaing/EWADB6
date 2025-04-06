using CloudHRMS.Domain.DAO;
using CloudHRMS.Domain.Models.DataModels;
using CloudHRMS.Repositories.Common;

namespace CloudHRMS.Repositories.Domain {
    public class PayrollRepository : BaseRepository<PayrollEntity>, IPayrollRepository {
        private readonly HRMSDbContext _hRMSDBContext;
        public PayrollRepository(HRMSDbContext hRMSDBContext) : base(hRMSDBContext) {
        }
    }
}