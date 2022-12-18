namespace TeamHunter.Models.DTO;

public class ResponseMessage {
    public string status { get; set; }
    public string? message { get; set; }

    public static ResponseMessage Ok(){
        return new ResponseMessage{
            status="success"
        };
    }
    public static ResponseMessage Ok(string status){

        return new ResponseMessage();
    }
}