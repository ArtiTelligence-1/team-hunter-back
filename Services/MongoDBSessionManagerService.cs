using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TeamHunter.Interfaces;
using TeamHunter.Models;

namespace TeamHunter.Services;

public class MongoDBSessionManagerService : IDBSessionManagerService {
    private IMongoClient databaseClient;
    private ICredentialsService databaseConfig;
    public MongoDBSessionManagerService(ICredentialsService databaseConfig){
        this.databaseConfig = databaseConfig;
        databaseClient = new MongoClient(this.databaseConfig.DatabaseConnectionString);
    }

    public IMongoCollection<T> GetCollection<T>() =>
        databaseClient.GetDatabase(this.databaseConfig.DatabaseName).GetCollection<T>(typeof(T).Name);
        
}