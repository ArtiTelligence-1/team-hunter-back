using TeamHunter.Models;
using TeamHunter.Interfaces;

using MongoDB.Driver;
using Microsoft.Extensions.Options;

namespace TeamHunter.Services;

public class SettingsManagerService : ISettingsManagerService
{
    private readonly IMongoCollection<Setting> settingsManager;
    private readonly ISettingsService localData;
    private Dictionary<string, IEnumerable<string>> settingsCache;

    public SettingsManagerService(IDBSessionManagerService manager, IOptions<SettingParams> localData) {
        this.settingsManager = manager.GetCollection<Setting>();
        this.localData = localData.Value;
        this.settingsCache = new Dictionary<string, IEnumerable<string>>();
    }

    public async Task<IEnumerable<string>> GetMultipleAsync(string key) {
        try{
            if (this.settingsCache.ContainsKey(key))
                return this.settingsCache[key];
            var value = (await this.settingsManager.FindAsync(s => s.Key == key)).First().Value;
            if (value.Count() == 0)
                throw new NullReferenceException();

            this.settingsCache[key] = value;
            return value;
        } 
        catch(Exception e)//when (e is NullReferenceException || e is AggregateException)
        {
            Console.WriteLine(e.Data);
            Console.WriteLine(e.Message);
            var localProperty = localData.GetType().GetProperties().First(prop => prop.Name == key);
            if (typeof(IEnumerable<string>).IsAssignableFrom(localProperty.GetValue(localData)!.GetType())){
                await this.settingsManager.InsertOneAsync(new Setting{
                    Key = localProperty.Name,
                    Value = (localProperty.GetValue(localData)! as IEnumerable<string>)!
                });
            }
            else
                await this.settingsManager.InsertOneAsync(new Setting(){
                    Key = localProperty.Name,
                    Value = new List<string> { localProperty.GetValue(localData)!.ToString()! }
                });
            return new List<string>();
            // return await GetMultipleAsync(key);
        }
    }

    public async Task<string> GetSingleAsync(string key) =>
        (await this.GetMultipleAsync(key)).First();
    public async Task SetValueAsync(string key, string value) =>
        await this.SetValueAsync(key, new List<string>() { value });
    public async Task SetValueAsync(string key, IEnumerable<string> values) {
        UpdateDefinition<Setting> update = Builders<Setting>.Update.Set("Value", values);

        this.settingsCache[key] = values;
        await this.settingsManager.UpdateOneAsync(s => s.Key == key, update);
    }
}