using Bacen.Domain.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Bacen.Domain.Utils;

public class UnitOfWork : IUnitOfWork
{
    public IMongoDatabase Database { get; private set; }
    
    public UnitOfWork(IOptions<BacenDatabaseSettings> bacenDatabaseSettings)
    {
        var mongoClient = new MongoClient(bacenDatabaseSettings.Value.ConnectionString);
        Database = mongoClient.GetDatabase(bacenDatabaseSettings.Value.DatabaseName);
    }

    public IMongoCollection<T> GetCollection<T>(string collectionName) where T : Entity
    {
        return Database.GetCollection<T>(collectionName);
    }
}