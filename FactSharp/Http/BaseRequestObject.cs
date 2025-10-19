namespace FactSharp.Http;

public  abstract class BaseRequestObject
{
    public string ApiKey { get; set; } = string.Empty;
    public virtual string Controller { get; set; } = string.Empty;
    public virtual string Action { get; set; } = string.Empty;
}