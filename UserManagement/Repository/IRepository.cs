using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace UserManagement.Repository
{
    public interface IRepository<TEntity, in TKey> where TEntity : class where TKey : struct
    {
        Task<TEntity> GetAsync(TKey id, bool includeDetails = false);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, params string[] includes);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, bool includeDetails = false);
        Task<TEntity> GetAsNoTrackingAsync(Expression<Func<TEntity, bool>> predicate, params string[] includes);

        Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate = null, params string[] includes);
        Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate = null, bool includeDetails = false);

        Task<TEntity> AddAsync(TEntity entity, bool autoSave = false);
        Task<TEntity> UpdateAsync(TEntity entity, bool autoSave = false);
        Task DeleteAsync(TEntity entity, bool autoSave = false);
        Task DeleteAsync(TKey id, bool autoSave = false);
        IQueryable<TEntity> AsQueryable();

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);

    }

}