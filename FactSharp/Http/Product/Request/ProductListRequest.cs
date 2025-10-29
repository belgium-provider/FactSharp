using Newtonsoft.Json;

namespace FactSharp.Http.Product.Request;

public class ProductListRequest() : BaseListRequestObject("product", "list", "ProductCode")
{
    [JsonProperty("group")]
    public int? Group { get; set; } = null; //id of a group
}