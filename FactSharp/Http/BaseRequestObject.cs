namespace FactSharp.Http;

public  abstract class BaseRequestObject
{
    public string ApiKey { get; set; } = string.Empty;
    public virtual required string Controller { get; set; }
    public virtual required string Action { get; set; }
}