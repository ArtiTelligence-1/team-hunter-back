namespace TeamHunter.Models.DTO;

public class ResponseMessage {
    public string status { get; set; } = "no data";
    public string? message { get; set; }

    
    public static ResponseMessage Ok(string message) =>
        ResponseMessage.Message("success", message);
    public static ResponseMessage Ok() =>
        ResponseMessage.Message("success");
    public static ResponseMessage Message(string status, string message = "") =>
        new ResponseMessage{
            status=status,
            message=message
        };
}