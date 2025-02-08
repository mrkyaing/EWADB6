using CloudHRMS.DAO;
using CloudHRMS.Repositories.Domain;

namespace CloudHRMS.UnitOfWorks {
    public class UnitOfWork : IUnitOfWork {
        private readonly HRMSDbContext _dbContext;
        public UnitOfWork(HRMSDbContext dbContext) => this._dbContext = dbContext;
        //declare all type of repository to do Unit Of Work in here 
        private IPositoryRepository _positoryRepository;
        public IPositoryRepository PositoryRepository {
            get {
                return _positoryRepository = _positoryRepository ?? new PositionRepository(_dbContext);
            }
        }

        private IEmployeeRepository _employeeRepository;
        public IEmployeeRepository EmployeeRepository {
            get {
                return _employeeRepository = _employeeRepository ?? new EmployeeRepository(_dbContext);
            }
        }

        private IDepartmentRepository departmentRepository;
        public IDepartmentRepository DepartmentRepository {
            get {
                return departmentRepository = departmentRepository ?? new DepartmentRepository(_dbContext);
            }
        }
        //------------------------------
        public void Commit() {
            _dbContext.SaveChanges();
        }

        public void Rollback() {
            _dbContext.Dispose();
        }
    }
}
