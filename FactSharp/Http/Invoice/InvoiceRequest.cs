using System.ComponentModel.DataAnnotations;

namespace FactSharp.Http.Invoice;

public class InvoiceRequest : BaseRequestObject
{
    [MinLength(6)]
    public required string InvoiceCode { get; set; }
}