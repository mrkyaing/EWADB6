using CloudHRMS.Models.DataModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CloudHRMS.DAO {
    public class HRMSDbContext : IdentityDbContext<IdentityUser, IdentityRole, string> {
        public HRMSDbContext(DbContextOptions<HRMSDbContext> db) : base(db) {
        }
        //Has-a Relationship for all of Entities as DBSet
        public DbSet<EmployeeEntity> Employees { get; set; }
        public DbSet<DepartmentEntity> Departments { get; set; }
        public DbSet<PositionEntity> Positions { get; set; }
        public DbSet<ShiftEntity> Shifts { get; set; }
        public DbSet<ShiftAssignEntity> ShiftAssigns { get; set; }
        public DbSet<AttendancePolicyEntity> AttendancePolicies { get; set; }
        public DbSet<DailyAttendanceEntity> DailyAttendances { get; set; }
        public DbSet<AttendanceMasterEntity> AttendanceMasters { get; set; }
        public DbSet<PayrollEntity> Payrolls { get; set; }
    }
}
