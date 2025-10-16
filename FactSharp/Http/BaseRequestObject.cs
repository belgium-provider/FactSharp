namespace FactSharp.Http;

public  abstract class BaseRequestObject
{
    public string ApiKey { get; set; } = string.Empty;
    public required string Controller { get; set; }
    public required string Action { get; set; }
}