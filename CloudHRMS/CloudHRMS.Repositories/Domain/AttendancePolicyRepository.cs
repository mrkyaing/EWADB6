﻿using CloudHRMS.Domain.DAO;
using CloudHRMS.Domain.Models.DataModels;
using CloudHRMS.Repositories.Common;

namespace CloudHRMS.Repositories.Domain {
    public class AttendancePolicyRepository : BaseRepository<AttendancePolicyEntity>, IAttendancePolicyRepository {
        private readonly HRMSDbContext _dbContext;
        public AttendancePolicyRepository(HRMSDbContext dbContext) : base(dbContext) {
        }
    }

}