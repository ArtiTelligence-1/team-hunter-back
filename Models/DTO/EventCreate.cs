namespace TeamHunter.Models.DTO;

public class EventCreate{
    public User? Owner { get; set; }
    public string? Title { get; set; }
    public string? Type { get; set; }
    public int ParticipantsLimit { get; set; }
    public AgeInterval? AgeLimitGap { get; set; }
    public DateTime HoldingTime { get; set; } = new DateTime((DateTime.Now + new TimeSpan(3,0,0,0)).Ticks);
    public Location? Location { get; set; }
    public string? Description { get; set; }
    public string? PosterUrl { get; set; }

    public Event toEvent() =>
        new Event(){
            Owner = this.Owner,
            Title = this.Title,
            Type = this.Type,
            ParticipantsLimit = this.ParticipantsLimit,
            AgeLimitGap = this.AgeLimitGap,
            HoldingTime = this.HoldingTime,
            Location = this.Location,
            Description = this.Description,
            PosterUrl = this.PosterUrl
        };
}