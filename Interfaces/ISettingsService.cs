namespace TeamHunter.Interfaces;

public interface ISettingsService
{
    Task<List<string>> GetMultiple(string key);
    Task<string> GetSingle(string key);
    Task SetValue(string key, string value);
    Task SetValue(string key, List<string> values);
}