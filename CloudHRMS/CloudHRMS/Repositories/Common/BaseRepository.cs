using CloudHRMS.DAO;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CloudHRMS.Repositories.Common {
    public class BaseRepository<T> : IBaseRepository<T> where T : class {
        private readonly HRMSDbContext _context;
        private readonly DbSet<T> _dbSet;

        public BaseRepository(HRMSDbContext dbContext) {
            _context = dbContext;
            _dbSet = _context.Set<T>();
        }

        public void Create(T entity) {
            _context.Add<T>(entity);
        }

        public void Create(IList<T> entities) {
            foreach (var entity in entities) {
                _context.Add<T>(entity);
            }
        }

        public void Delete(T entity) {
            //called to update function
            Update(entity);
        }

        public void Delete(T entity, bool isHardDeleted) {
            _context.Remove<T>(entity);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> expression) {
            return _dbSet.AsNoTracking().Where(expression).AsEnumerable();
        }

        public IEnumerable<T> GetBy(Expression<Func<T, bool>> expression) {
            return _dbSet.AsNoTracking().Where(expression).AsEnumerable();
        }

        public void Update(T entity) {
            _context.Update<T>(entity);
        }
    }
}
