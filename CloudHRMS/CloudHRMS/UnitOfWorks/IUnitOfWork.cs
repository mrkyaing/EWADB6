using CloudHRMS.Repositories.Domain;

namespace CloudHRMS.UnitOfWorks {
    public interface IUnitOfWork {
        IDepartmentRepository DepartmentRepository { get; }
        IPositoryRepository PositoryRepository { get; }
        IEmployeeRepository EmployeeRepository { get; }
        //for commit stages(insert,update,delete)
        void Commit();
        void Rollback();
    }
}
