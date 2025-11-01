using FactSharp.Models;
using Newtonsoft.Json;

namespace FactSharp.Http.Invoice.Request;

public class AddInvoiceLineRequest : BaseRequestObject
{
    //default
    [JsonProperty("controller")]
    public override required string Controller { get; set; } = "invoiceline";
    [JsonProperty("action")]
    public override required string Action { get; set; } = "add";
    
    public int Identifier { get; set; }
    public required string InvoiceCode { get; set; }
    public List<InvoiceLine> InvoiceLines = [];
}