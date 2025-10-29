using Newtonsoft.Json;

namespace FactSharp.Http.Invoice.Response;

public class CreateInvoiceResponse : BaseResponseObject
{
    [JsonProperty("invoice")]
    public required Models.Invoice Invoice { get; set; }
}