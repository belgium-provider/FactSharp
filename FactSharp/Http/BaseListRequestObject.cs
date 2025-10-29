using Newtonsoft.Json;

namespace FactSharp.Http;

public abstract class BaseListRequestObject(string controller, string action, string sort) : BaseRequestObject
{
    [JsonProperty("controller")]
    public override string Controller { get; set; } = controller;
    [JsonProperty("action")]
    public override string Action { get; set; } = action;

    [JsonProperty("offset")]
    public int Offset { get; set; } = 1;
    [JsonProperty("limit")]
    public int Limit { get; set; } = 1000; //standard 
    [JsonProperty("order")]
    public string Order { get; set; } = Types.Order.Asc; //refer to Order model.
    [JsonProperty("sort")]
    public string Sort { get; set; } = sort;
    
    [JsonProperty("created")]
    public DateTime? Created { get; set;  } = null;
    [JsonProperty("updated")]
    public DateTime? Modified { get; set; } = null;
}