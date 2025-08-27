using System.Linq.Expressions;

namespace DataAccessLayer.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task CommitAsync();
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null, params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity?> GetByIdAsync(int id, params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity> GetSingleByConditionAsync(Expression<Func<TEntity, bool>> predicate = null, params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity> GetSingleWithIncludesAsync(Expression<Func<TEntity, bool>> predicate,Func<IQueryable<TEntity>, IQueryable<TEntity>> includeFunc);
        Task<TEntity> AddAsync(TEntity entity);
        Task AddRangeAsync(List<TEntity> entities);
        Task<bool> Update(TEntity entity);
        Task<bool> UpdateRange(List<TEntity> entities);
        Task<bool> HardRemove(Expression<Func<TEntity, bool>> predicate);
        Task<bool> HardRemoveRange(List<TEntity> entities);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate = null);
        IQueryable<TEntity> GetAllQueryable();
        Task<int> SaveChangesAsync();
    }
}
