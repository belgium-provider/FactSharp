using System.ComponentModel.DataAnnotations;

namespace FactSharp.Http.Invoice;

public class InvoiceRequest : BaseRequestObject
{
}

public class InvoiceRequestByCode : InvoiceRequest
{
    [MinLength(6)]
    public required string InvoiceCode { get; set; }
}

public class InvoiceRequestById : InvoiceRequest
{
    [Required]
    public int Identifier { get; set; }
}