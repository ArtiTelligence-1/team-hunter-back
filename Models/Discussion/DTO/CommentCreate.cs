namespace TeamHunter.Models.DTO;

public class CommentCreate {
    public string? text { get; set; }
    public DateTime? replyTo { get; set; }

    public bool Validate() =>
        this.text is not null &&
        this.replyTo is not null;
}