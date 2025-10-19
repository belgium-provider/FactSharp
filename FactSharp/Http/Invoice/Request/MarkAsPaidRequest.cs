namespace FactSharp.Http.Invoice.Request;

public class MarkAsPaidRequest : BaseRequestObject
{
    public required string InvoiceCode { get; set; }
    public DateTime PayDate { get; set; }
    public required string PaymentMethod { get; set; } = Types.PaymentMethod.Other; //refer to types
}