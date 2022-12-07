namespace TeamHunter.Schemas;

public class EventUpdate{
    public string? Title { get; set; }
    public EventType? Type { get; set; }
    public List<EventTag>? Tags { get; set; }
    public AgeInterval? AgeInterval { get; set; }
    public DateTime? HoldingTime { get; set; }
    public Location? Location { get; set; }
    public string? Description { get; set; }
    public int? ParticipantLimit { get; set; }

    public static EventUpdate fromEvent(Event update) =>
        new EventUpdate(){
            Title = update.Title,
            Type = update.Type,
            Tags = update.Tags,
            AgeInterval = update.AgeInterval,
            HoldingTime = update.HoldingTime,
            Location = update.Location,
            Description = update.Description,
            ParticipantLimit = update.ParticipantLimit
        };

    public Event toEvent() =>
        new Event() {
            Title = this.Title!,
            Type = this.Type!.Value,
            Tags = this.Tags!,
            AgeInterval = this.AgeInterval!,
            HoldingTime = this.HoldingTime!.Value,
            Location = this.Location!,
            Description = this.Description!,
            ParticipantLimit = this.ParticipantLimit!.Value
        };
}