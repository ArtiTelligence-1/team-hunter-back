namespace TeamHunter.Interfaces;

public interface ISettingsService {
    IEnumerable<string> EventTypes { get; set; }
    uint MessageRepliesLimit { get; set; }
}