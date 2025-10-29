using Newtonsoft.Json;

namespace FactSharp.Http;

public class BaseListObjectResponse : BaseResponseObject
{
    [JsonProperty("totalresults")]
    public int TotalResults { get; set; } = 1;
    [JsonProperty("currentresults")]
    public int CurrentResults { get; set; } = 1;
    [JsonProperty("offset")]
    public int Offset { get; set; } = 0;
}