using System;
using System.Collections.Generic;
using System.Linq;

namespace Exercise.Domain
{
    public abstract class InMemoryRepository<TEntity> where TEntity: GuidEntity
    {
        protected readonly List<TEntity> Entities = new List<TEntity>();


        public virtual TEntity Add(TEntity item)
        {
            if (!Exists(item))
            {
                Entities.Add(item);
            }

            return item;
        }

        public virtual bool Exists(TEntity item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            return GetBy(item.Id) != null;
        }

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