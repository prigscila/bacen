using Bacen.Domain.Entities;
using MongoDB.Driver;

namespace Bacen.Domain.Utils;

public interface IUnitOfWork
{  
    IMongoCollection<T> GetCollection<T>(string collectionName) where T : Entity;
}