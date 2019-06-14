using System;
using System.Collections.Generic;
using Exercise.Domain.Companies;

namespace Exercise.Domain
{
    public abstract class InMemoryRepository<TEntity>
    {
        protected readonly List<TEntity> Entities = new List<TEntity>();

        public virtual IReadOnlyList<TEntity> List()
        {
            return Entities.AsReadOnly();
        }

        public abstract TEntity Add(Guid? id = null);
        public abstract Company GetBy(Guid id);
    }
}