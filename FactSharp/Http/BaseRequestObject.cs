using Newtonsoft.Json;

namespace FactSharp.Http;

public  abstract class BaseRequestObject
{
    [JsonProperty("api_key")]
    public string ApiKey { get; set; } = string.Empty;
    [JsonProperty("controller")]
    public virtual string Controller { get; set; } = string.Empty;
    [JsonProperty("action")]
    public virtual string Action { get; set; } = string.Empty;
}