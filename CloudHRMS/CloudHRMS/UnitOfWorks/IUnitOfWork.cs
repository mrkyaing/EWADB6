using CloudHRMS.Repositories.Domain;

namespace CloudHRMS.UnitOfWorks
{
    public interface IUnitOfWork
    {
        IPositoryRepository PositoryRepository { get; }
        //for commit stages(insert,update,delete)
        void Commit();
        void Rollback();
    }
}
