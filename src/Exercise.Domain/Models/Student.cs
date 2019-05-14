using MongoDB.Bson.Serialization.Attributes;

namespace Exercise.Domain.Models
{
    public class Student : Entity
    {
        [BsonElement("Salutation")]
        public string Salutation { get; set; }
        [BsonElement("Firstname")]
        public string Firstname { get; set; }
        [BsonElement("Surname")]
        public string Surname { get; set; }
        [BsonElement("Age")]
        public int Age { get; set; }

        //TODO: Figure out if Mongo support immutable types, if so remove this
        public static Student Create(string id, string salutation, int age, string firstname, string surname)
        {
            return new Student { Id = id, Age = age, Surname = surname, Firstname = firstname, Salutation = salutation };
        }
    }
}
