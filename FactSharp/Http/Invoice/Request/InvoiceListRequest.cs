using FactSharp.Types;
using Newtonsoft.Json;

namespace FactSharp.Http.Invoice.Request;

public class InvoiceListRequest() : BaseListRequestObject("invoice", "list", "InvoiceCode")
{
    [JsonProperty("status")]
    public EInvoiceStatus? Status { get; set; } = null; //refer to InvoiceStatus => available status
    
    //other ?
}