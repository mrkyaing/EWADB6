﻿using CloudHRMS.Domain.DAO;
using CloudHRMS.Domain.Models.DataModels;
using CloudHRMS.Repositories.Common;

namespace CloudHRMS.Repositories.Domain {
    public class PositionRepository : BaseRepository<PositionEntity>, IPositoryRepository {
        private readonly HRMSDbContext _context;
        public PositionRepository(HRMSDbContext dbContext) : base(dbContext) {
            _context = dbContext;
        }
    }
}
