﻿
using CloudHRMS.Domain.DAO;
using CloudHRMS.Domain.Models.DataModels;
using CloudHRMS.Repositories.Common;

namespace CloudHRMS.Repositories.Domain {
    public class AttendanceMasterRepository : BaseRepository<AttendanceMasterEntity>, IAttendanceMasterRepository {
        private readonly HRMSDbContext _hRMSDBContext;
        public AttendanceMasterRepository(HRMSDbContext hRMSDBContext) : base(hRMSDBContext) {
        }
    }
}