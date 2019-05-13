using Exercise.Domain.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Exercise.Domain
{
    public class RepositoryBase<TEntity, TIdType> where TEntity : IEntity<TIdType>
    {
        public RepositoryBase(IConfiguration configuration)
            : this(UnitOfWork.Create(configuration))
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }
        }

        public RepositoryBase(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException(nameof(unitOfWork));
            }
            var collectionName = typeof(TEntity).Name;
            Collection = unitOfWork.Database.GetCollection<TEntity>(collectionName);
        }

        protected IMongoCollection<TEntity> Collection { get; }

        /// <remarks>
        /// The advantage of a synchronous add is the InsertOne generates an Id without needing
        /// a do a separate call
        /// </remarks>
        public virtual TEntity Add(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            Collection.InsertOne(entity);
            return entity;
        }

        public virtual void Add(IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            Collection.InsertMany(entities);
        }

        public virtual async Task<long> CountAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            var filterDefinition =
                filter == null ? FilterDefinition<TEntity>.Empty : (FilterDefinition<TEntity>)filter;
            return await Collection.CountDocumentsAsync(filterDefinition).ConfigureAwait(false);
        }

        public virtual async Task<bool> DeleteAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            return await DeleteAsync(entity.Id).ConfigureAwait(false);
        }

        public virtual async Task<bool> DeleteAsync(TIdType id)
        {
            var filter = Builders<TEntity>.Filter.Eq(s => s.Id, id);
            Collection.DeleteOne(filter);
            var result = await Collection.DeleteOneAsync(x => x.Id.Equals(id)).ConfigureAwait(false);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }

        public virtual async Task<bool> ExistsAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            return await ExistsAsync(entity.Id).ConfigureAwait(false);
        }

        public virtual async Task<bool> ExistsAsync(TIdType id)
        {
            var result = await Collection.CountDocumentsAsync(x => x.Id.Equals(id))
                .ConfigureAwait(false);
            return result > 0;
        }

        public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            return await Collection
                .AsQueryable()
                .AnyAsync(predicate)
                .ConfigureAwait(false);
        }

        public virtual async Task<TEntity> GetByAsync(TIdType id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var filter = Builders<TEntity>.Filter.Eq(s => s.Id, id);
            var cursor = await Collection.FindAsync(filter);
            return cursor.MoveNext() ? cursor.Current.SingleOrDefault() : default(TEntity);
        }

        public virtual async Task<PagedResult<TEntity, TIdType>> ListAsync(
            Expression<Func<TEntity, bool>> filter = null, 
            int page = 1,
            int pageSize = 100)
        {
            if (page < 0)
            {
                page = 1;
            }

            double totalDocuments = await CountAsync(filter);
            var maxPageCount = Math.Ceiling(totalDocuments / pageSize);
            if (maxPageCount > 0 && page > maxPageCount)
            {
                page = (int)maxPageCount;
            }

            List<TEntity> result = null;
            if (filter == null)
            {
                 result = await Collection.AsQueryable()
                    .Skip(page - 1)
                    .Take(pageSize)
                    .ToListAsync()
                    .ConfigureAwait(false);

            }
            else
            {
                result = await Collection.AsQueryable()
                    .Where(filter)
                    .Skip(page - 1)
                    .Take(pageSize)
                    .ToListAsync()
                    .ConfigureAwait(false);
            }
            return new PagedResult<TEntity, TIdType>(result.AsReadOnly(), page, result.Count, maxPageCount, totalDocuments);
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var filter = Builders<TEntity>.Filter.Eq(x => x.Id, entity.Id);
            await Collection.ReplaceOneAsync(filter, entity);
            return entity;
        }
    }
}