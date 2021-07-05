using System;

namespace Exercise.Domain
{
    public class GuidEntity
    {
        public GuidEntity(Guid? id = null)
        {
            Id = id ?? Guid.NewGuid();
        }

        public Guid Id { get; }
    }
}