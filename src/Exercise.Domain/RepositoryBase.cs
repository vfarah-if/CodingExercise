﻿using Exercise.Domain.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
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
            Collection = unitOfWork.Database.GetCollection<TEntity>(GetCollectionName());
        }

        public IMongoCollection<TEntity> Collection { get; }

        /// <remarks>
        /// The advantage of a synchronous add is the InsertOne generates an Id without needing
        /// a do a separate call
        /// </remarks>
        public TEntity Add(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            Collection.InsertOne(entity);
            return entity;
        }

        public void Add(IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            Collection.InsertMany(entities);
        }

        public async Task<long> CountAsync()
        {
            return await Collection.CountDocumentsAsync(FilterDefinition<TEntity>.Empty).ConfigureAwait(false);
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            return await DeleteAsync(entity.Id).ConfigureAwait(false);
        }

        public async Task<bool> DeleteAsync(TIdType id)
        {
            var filter = Builders<TEntity>.Filter.Eq(s => s.Id, id);
            Collection.DeleteOne(filter);
            var result = await Collection.DeleteOneAsync<TEntity>(x => x.Id.Equals(id)).ConfigureAwait(false);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }

        public async Task<bool> ExistsAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            return await ExistsAsync(entity.Id).ConfigureAwait(false);
        }

        public async Task<bool> ExistsAsync(TIdType id)
        {
            var result = await Collection.CountDocumentsAsync(x => x.Id.Equals(id)).ConfigureAwait(false);
            return result > 0;
        }

        public async Task<IReadOnlyCollection<TEntity>> ListAsync(int page = 1, int pageSize = 100)
        {
            if (page < 0)
            {
                page = 1;
            }

            double totalDocuments = await CountAsync();
            var maxPageCount = Math.Ceiling(totalDocuments / pageSize);
            if (maxPageCount > 0 && page > maxPageCount)
            {
                page = (int)maxPageCount;
            }
            return await Collection.AsQueryable()
                .Skip(page - 1)
                .Take(pageSize)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        private static string GetCollectionName()
        {
            var collectionName = typeof(TEntity).Name;

            if (string.IsNullOrEmpty(collectionName))
            {
                throw new ArgumentException("Collection name cannot be empty for this entity");
            }
            return collectionName;
        }
    }
}