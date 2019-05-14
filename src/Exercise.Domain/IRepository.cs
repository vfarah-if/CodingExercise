using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Exercise.Domain.Models;

namespace Exercise.Domain
{
    public interface IRepository<TEntity, TIdType>: IQueryable<TEntity> where TEntity : IEntity<TIdType>
    {
        void Add(IEnumerable<TEntity> entities);
        TEntity Add(TEntity entity);
        Task<long> CountAsync(Expression<Func<TEntity, bool>> filter = null);
        Task<bool> DeleteAsync(TEntity entity);
        Task<bool> DeleteAsync(TIdType id);
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);
        Task<bool> ExistsAsync(TEntity entity);
        Task<bool> ExistsAsync(TIdType id);
        Task<TEntity> GetByAsync(TIdType id);
        Task<PagedResult<TEntity, TIdType>> ListAsync(Expression<Func<TEntity, bool>> filter = null, int page = 1, int pageSize = 100);
        Task<TEntity> UpdateAsync(TEntity entity);
    }

    public interface IRepository<TEntity> : IRepository<TEntity, string>
        where TEntity : IEntity
    {
    }
}