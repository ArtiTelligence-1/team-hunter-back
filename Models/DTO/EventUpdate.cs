namespace TeamHunter.Models.DTO;

public class EventUpdate{
    public int? ParticipantsLimit { get; set; }
    public AgeInterval? AgeLimitGap { get; set; }
    public DateTime HoldingTime { get; set; } = new DateTime((DateTime.Now + new TimeSpan(3,0,0,0)).Ticks);
    public Location? Location { get; set; }
    public string? Description { get; set; }
    public string? PosterUrl { get; set; }

    public static EventUpdate fromEvent(Event update) =>
        new EventUpdate(){
            ParticipantsLimit = update.ParticipantsLimit,
            AgeLimitGap = update.AgeLimitGap,
            HoldingTime = update.HoldingTime,
            Location = update.Location,
            Description = update.Description,
            PosterUrl = update.PosterUrl
        };
}