using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Exercise.Domain
{
    /// <summary>
    /// MongoDb Connection
    /// </summary>
    /// <example>new UnitOfWork("mongodb://mongodb0.example.com:27017/admin");</example>
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(string connectionString)
            : this(new MongoUrl(connectionString))
        {
        }

        public UnitOfWork(MongoUrl url)
        {
            Client = new MongoClient(url);
            Database = Client.GetDatabase(url.DatabaseName);
        }

        public IMongoClient Client { get; }
        public IMongoDatabase Database { get; }

        public static IUnitOfWork Create(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Default");
            return new UnitOfWork(connectionString);
        }
    }
}
