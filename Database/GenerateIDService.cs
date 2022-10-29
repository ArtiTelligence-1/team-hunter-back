using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TeamHunterBackend.Schemas;

namespace TeamHunterBackend.DB
{
    public class GenerateIDService : IGenerateIDService
    {
        private readonly IMongoCollection<Counter> _counters;
        public GenerateIDService(IOptions<DBSettings> options)
        {
            var mongoClient = new MongoClient(options.Value.ConnectionString);
            _counters = mongoClient.GetDatabase(options.Value.DatabaseName).GetCollection<Counter>(options.Value.CollectionName[6]);
        }
        public int GenerateID(string counterName)
        {
            var obj = _counters.Find(m => m.CounterName == counterName).FirstOrDefault();
            obj.Count += 1;
            _counters.ReplaceOne(m => m.CounterName == counterName, obj);
            return obj.Count;
        }
    }
}