using MongoDB.Bson.Serialization.Attributes;

namespace Exercise.Domain.Models
{
   
    public interface IEntity<TIdType>
    {
        [BsonId]
        TIdType Id { get; set; }
    }

    public interface IEntity : IEntity<string>
    {
    }
}