namespace TeamHunter.Models.DTO;

public class EventDetails : EventShortInfo {
    public Location? Location { get; set; }
    public string? Description { get; set; }
    /// <summary>Up To 10 messages in event chat</summary>
    public List<Comment> Discussion { get; set; } = new List<Comment>();
}