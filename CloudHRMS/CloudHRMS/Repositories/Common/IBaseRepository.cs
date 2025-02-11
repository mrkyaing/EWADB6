using System.Linq.Expressions;

namespace CloudHRMS.Repositories.Common {
    public interface IBaseRepository<T> where T : class {
        //crud process
        void Create(T entity);
        void Create(IList<T> entities);
        //R
        IEnumerable<T> GetAll(Expression<Func<T, bool>> expression);
        IEnumerable<T> GetBy(Expression<Func<T, bool>> expression);
        //U
        void Update(T entity);
        //D
        void Delete(T entity);
        //Actual Delete
        void Delete(T entity, bool isHardDeleted);
    }
}
