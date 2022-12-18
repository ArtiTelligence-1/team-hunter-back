namespace TeamHunter.Models.DTO;

public class EventCreate {
    // public User? Owner { get; set; }
    public string? title { get; set; }
    public string? type { get; set; }
    public int participantsLimit { get; set; }
    public AgeInterval? ageLimitGap { get; set; }
    public DateTime? holdingTime { get; set; }
    public Location? location { get; set; }
    public string? description { get; set; }
    public string? posterUrl { get; set; }

    public bool Validate() =>
        this.title is not null &&
        this.ageLimitGap is not null &&
        this.holdingTime is not null &&
        this.location is not null &&
        this.description is not null &&
        this.posterUrl is not null;

    public Event toEvent() =>
        !this.Validate() ?
            throw new ArgumentNullException()
        : 
            new Event(){
                Title = this.title,
                Type = this.type,
                ParticipantsLimit = this.participantsLimit,
                AgeLimitGap = this.ageLimitGap,
                HoldingTime = this.holdingTime!.Value,
                Location = this.location,
                Description = this.description,
                PosterUrl = this.posterUrl
            };
}