using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Exercise.Domain.Models
{
    [BsonIgnoreExtraElements(Inherited = true)]
    public abstract class Entity : IEntity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public virtual string Id { get; set; }
    }
}
