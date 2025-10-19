namespace FactSharp.Http.Invoice.Response;

public class InvoiceResponse : BaseResponseObject
{
    public required Models.Invoice Invoice { get; set; }
}