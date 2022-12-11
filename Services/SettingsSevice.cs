using TeamHunter.Interfaces;

namespace TeamHunter.Services;

public class SettingsService : ISettingsService {
    private readonly ISettingsManagerService settingsManager;

    public SettingsService(ISettingsManagerService settingsManager) {
        this.settingsManager = settingsManager;
    }

    public IEnumerable<string> EventTypes { 
        get => this.settingsManager.GetMultipleAsync("EventTypes").Result;
        set => this.settingsManager.SetValueAsync("EventTypes", value).Wait(); 
    }
    public uint MessageRepliesLimit { 
        get => Convert.ToUInt32(this.settingsManager.GetSingleAsync("MessageRepliesLimit").Result);
        set => this.settingsManager.SetValueAsync("MessageRepliesLimit", value.ToString()).Wait(); 
    }
}