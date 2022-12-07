using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TeamHunter.Interfaces;
using TeamHunter.Models;

namespace TeamHunter.Services;

public class MongoDBSessionManagerService : IDBSessionManagerService {
    private IMongoClient databaseClient;
    private IDatabaseConfigService databaseConfig;
    public MongoDBSessionManagerService(IDatabaseConfigService databaseConfig){
        this.databaseConfig = databaseConfig;
        databaseClient = new MongoClient(this.databaseConfig.ConnectionString);
    }

    public IMongoCollection<T> GetCollection<T>() =>
        databaseClient.GetDatabase(this.databaseConfig.DatabaseName).GetCollection<T>(typeof(T).Name);
        
}