using DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccessLayer.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly FlowerShopDBContext _dbContext;

        public Repository(FlowerShopDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            try
            {
                var result = await _dbContext.Set<TEntity>().AddAsync(entity);
                return result.Entity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task AddRangeAsync(List<TEntity> entities)
        {
            try
            {
                await _dbContext.Set<TEntity>().AddRangeAsync(entities);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            // Add OrderBy for entities that have DisplayIndex property
            if (typeof(TEntity).GetProperty("DisplayIndex") != null)
            {
                var parameter = Expression.Parameter(typeof(TEntity), "x");
                var property = Expression.Property(parameter, "DisplayIndex");
                var lambda = Expression.Lambda<Func<TEntity, int>>(property, parameter);
                query = query.OrderBy(lambda);
            }

            return await query.ToListAsync();
        }

        public IQueryable<TEntity> GetAllQueryable()
        {
            return _dbContext.Set<TEntity>().AsQueryable();
        }


        public async Task<TEntity?> GetByIdAsync(int id, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            // Tìm thuộc tính chứa "Id" (ví dụ: Id, ProductId, UserId, ...)
            var keyProperty = typeof(TEntity)
                .GetProperties()
                .FirstOrDefault(p => p.Name.EndsWith("Id", StringComparison.OrdinalIgnoreCase));

            if (keyProperty == null)
            {
                throw new InvalidOperationException($"Entity {typeof(TEntity).Name} không có khóa chính hợp lệ.");
            }

            // Tạo biểu thức động: x => x.{KeyProperty} == id
            var parameter = Expression.Parameter(typeof(TEntity), "x");
            var propertyAccess = Expression.Property(parameter, keyProperty);
            var constant = Expression.Constant(id);
            var equalExpression = Expression.Equal(propertyAccess, constant);
            var lambda = Expression.Lambda<Func<TEntity, bool>>(equalExpression, parameter);

            return await query.FirstOrDefaultAsync(lambda);
        }

        public async Task<TEntity> GetSingleByConditionAsync(Expression<Func<TEntity, bool>> predicate = null, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return await query.FirstOrDefaultAsync(predicate);
        }

        public async Task<bool> HardRemove(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                var entities = await _dbContext.Set<TEntity>().Where(predicate).ToListAsync();
                if (entities.Any())
                {
                    _dbContext.Set<TEntity>().RemoveRange(entities);
                    return true;
                }
                return false; // Không có gì để xóa
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while performing hard remove: {ex.Message}");
            }
        }

        public async Task<bool> HardRemoveRange(List<TEntity> entities)
        {
            try
            {
                if (entities.Any())
                {
                    _dbContext.Set<TEntity>().RemoveRange(entities);
                    return true;
                }
                return false; // Không có gì để xóa
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while performing hard remove range: {ex.Message}");
            }
        }

        public async Task<bool> Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            return true;
        }

        public async Task<bool> UpdateRange(List<TEntity> entities)
        {
            _dbContext.Set<TEntity>().UpdateRange(entities);
            return true;
        }

        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<TEntity> GetSingleWithIncludesAsync(
            Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> includeFunc)
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();

            if (includeFunc != null)
                query = includeFunc(query);

            return await query.FirstOrDefaultAsync(predicate);
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate != null)
                return await _dbContext.Set<TEntity>().CountAsync(predicate);

            return await _dbContext.Set<TEntity>().CountAsync();
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate != null)
                return await _dbContext.Set<TEntity>().AnyAsync(predicate);

            return await _dbContext.Set<TEntity>().AnyAsync();
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
