using Newtonsoft.Json;

namespace FactSharp.Http.Product.Response;

public class ProductResponse : BaseResponseObject
{
    [JsonProperty("product")]
    public required Models.Product Product { get; set; }
}