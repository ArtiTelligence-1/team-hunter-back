namespace TeamHunter.Models.DTO;

public class ResponseData<T>: ResponseMessage{
    public T? data { get; set; }

    public static ResponseData<T> Ok(string status, T data) =>
        new ResponseData<T> {
            status = status,
            data = data
        };
}