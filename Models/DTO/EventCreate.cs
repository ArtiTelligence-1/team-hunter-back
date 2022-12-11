namespace TeamHunter.Models.DTO;

public class EventCreate{
    public User? Owner { get; set; }
    public string? Title { get; set; }
    public string? Type { get; set; }
    public int ParticipantsLimit { get; set; }
    public AgeInterval? AgeLimitGap { get; set; }
    public DateTime? HoldingTime { get; set; }
    public Location? Location { get; set; }
    public string? Description { get; set; }
    public string? PosterUrl { get; set; }

    bool Validate() => 
        this.Owner is not null &&
        this.Title is not null &&
        this.AgeLimitGap is not null &&
        this.HoldingTime is not null &&
        this.Location is not null &&
        this.Description is not null &&
        this.PosterUrl is not null;

    public Event toEvent() =>
        this.Validate() ?
            throw new ArgumentNullException()
        : 
            new Event(){
                Owner = this.Owner,
                Title = this.Title,
                Type = this.Type,
                ParticipantsLimit = this.ParticipantsLimit,
                AgeLimitGap = this.AgeLimitGap,
                HoldingTime = this.HoldingTime!.Value,
                Location = this.Location,
                Description = this.Description,
                PosterUrl = this.PosterUrl
            };
}