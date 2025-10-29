using Newtonsoft.Json;

namespace FactSharp.Http.Customer.Request;

public class CustomerListRequest() : BaseListRequestObject("debtor", "list", "DebtorCode")
{
    [JsonProperty("group")]
    public int Group { get; set; }
    
    //other ?
}