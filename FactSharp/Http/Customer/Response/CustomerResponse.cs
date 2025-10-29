using Newtonsoft.Json;

namespace FactSharp.Http.Customer.Response;

public class CustomerResponse : BaseResponseObject
{
    [JsonProperty("debtor")]
    public required Models.Customer Debtor { get; set; }
}