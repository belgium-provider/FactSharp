using Newtonsoft.Json;

namespace FactSharp.Http.Invoice.Request;

public class DeleteInvoiceLineRequest : BaseRequestObject
{
    //default
    [JsonProperty("controller")]
    public override required string Controller { get; set; } = "invoiceline";
    [JsonProperty("action")]
    public override required string Action { get; set; } = "delete";
    
    public int Identifier { get; set; }
    public required string InvoiceCode { get; set; }
    public List<int> InvoiceLineIds { get; set; } = [];
}