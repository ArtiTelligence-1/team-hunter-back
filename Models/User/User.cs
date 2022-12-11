namespace TeamHunter.Models;

public class User : UserShortInfo
{
    public long TelegramId { get; set; }
    public string Bio { get; set; } = String.Empty;
    public string? AboutMe { get; set;}
    public string? Sex { get; set; }
    public DateTime BirthDate { get; set; }
    public DateTime JoinDate { get; set; } = DateTime.Now;
    public List<Event> ActiveEvents { get; set; } = new List<Event>();
    public List<SessionInfo> ActiveSessions { get; set; } = new List<SessionInfo>();
}