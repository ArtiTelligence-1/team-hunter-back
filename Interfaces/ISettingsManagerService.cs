namespace TeamHunter.Interfaces;

public interface ISettingsManagerService
{
    Task<IEnumerable<string>> GetMultipleAsync(string key);
    Task<string> GetSingleAsync(string key);
    Task SetValueAsync(string key, string value);
    Task SetValueAsync(string key, IEnumerable<string> values);
}