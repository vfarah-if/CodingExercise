using System;
using System.Collections.Generic;
using System.Linq;

namespace Exercise.Domain
{
    public abstract class InMemoryRepository<TEntity> where TEntity: GuidEntity
    {
        protected readonly List<TEntity> Entities = new List<TEntity>();

        public abstract TEntity Add(Guid? id = null);

        public virtual IReadOnlyList<TEntity> List()
        {
            return Entities.AsReadOnly();
        }

        public virtual TEntity GetBy(Guid id)
        {
            return Entities.SingleOrDefault(x => x.Id == id);
        }
    }
}