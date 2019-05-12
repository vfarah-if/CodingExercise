using MongoDB.Driver;

namespace Exercise.Domain
{
    public interface IUnitOfWork
    {
        IMongoClient Client { get; }
        IMongoDatabase Database { get; }
    }
}