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
    }
}
