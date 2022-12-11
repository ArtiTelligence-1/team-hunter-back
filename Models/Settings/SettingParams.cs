using TeamHunter.Interfaces;

namespace TeamHunter.Models;

public class SettingParams: ISettingsService {
    public IEnumerable<string> EventTypes { get; set; } = new List<string>();
    public uint MessageRepliesLimit { get; set; }
}