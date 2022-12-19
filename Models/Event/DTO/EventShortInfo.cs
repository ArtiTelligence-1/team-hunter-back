namespace TeamHunter.Models.DTO;

public class EventShortInfo {
    public string? Id { get; set; }
    public string? PosterUrl { get; set; }
    public string? Title { get; set; }
    public string? Type { get; set; }
    public int ParticipantsLimit { get; set; }
    public int ParticipantsAmount { get; set; } = 0;
    public AgeInterval? AgeLimitGap { get; set; }
    public DateTime HoldingTime { get; set; } = new DateTime((DateTime.Now + new TimeSpan(3,0,0,0)).Ticks);
}