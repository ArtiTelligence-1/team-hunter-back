namespace TeamHunter.Models.DTO;

public class UserCreate {
    public long TelegramId { get; set; }
    public string? FirstName { get; set; }
    public string LastName { get; set; } = String.Empty;
    public string? PhotoUrl { get; set; }
    public string Bio { get; set; } = String.Empty;
    public string? AboutMe { get; set;}
    public string? Sex { get; set; }
    public DateTime BirthDate { get; set; }
}