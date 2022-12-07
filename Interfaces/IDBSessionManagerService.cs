using MongoDB.Driver;

namespace TeamHunter.Interfaces;

public interface IDBSessionManagerService
{
    public IMongoCollection<T> GetCollection<T>();
}