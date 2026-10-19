using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FactSharp.Http.Invoice.Response;

public class MarkAsPaidResponse : BaseResponseObject
{
    // WeFact returns "success" as a string or as a JSON array, so keep it as a token
    [JsonProperty("success")]
    public JToken? Success { get; set; }
}
