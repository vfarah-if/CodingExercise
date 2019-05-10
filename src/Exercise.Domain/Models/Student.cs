using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Exercise.Domain.Models
{
    public class Student
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("Salutation")]
        public string Salutation { get; set; }
        [BsonElement("Firstname")]
        public string Firstname { get; set; }
        [BsonElement("Surname")]
        public string Surname { get; set; }
        [BsonElement("Age")]
        public int Age { get; set; }
    }
}
