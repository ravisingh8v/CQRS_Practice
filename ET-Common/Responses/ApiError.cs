namespace ET_Common.Responses;

public class ApiError
{
    public int StatusCode { get; set; }
    public string Message { get; set; } = string.Empty;
    public List<string>? Details { get; set; }
    public DateTime? Timestamp { get; set; } = DateTime.UtcNow;
}