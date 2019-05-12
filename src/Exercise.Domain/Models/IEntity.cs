using MongoDB.Bson.Serialization.Attributes;

namespace Exercise.Domain.Models
{
   
    public interface IEntity<TKey>
    {
        [BsonId]
        TKey Id { get; set; }
    }

    public interface IEntity : IEntity<string>
    {
    }
}