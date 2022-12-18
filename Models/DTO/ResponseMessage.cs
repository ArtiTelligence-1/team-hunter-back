namespace TeamHunter.Models.DTO;

public class ResponseMessage {
    public string status { get; set; }
    public string? message { get; set; }

    public static ResponseMessage Ok() =>
        ResponseMessage.Message("success");
    public static ResponseMessage Message(string status) =>
        new ResponseMessage{
            status=status
        };
}