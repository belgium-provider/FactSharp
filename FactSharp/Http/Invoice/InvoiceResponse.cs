namespace FactSharp.Http.Invoice;

public class InvoiceResponse : BaseResponseObject
{
    public required Models.Invoice Invoice { get; set; }
}