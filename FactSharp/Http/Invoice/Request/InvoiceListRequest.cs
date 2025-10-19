namespace FactSharp.Http.Invoice.Request;

public class InvoiceListRequest() : BaseListRequestObject("invoice", "list", "InvoiceCode")
{
    public string? Status { get; set; } = null; //refer to InvoiceStatus => available status
    
    //other ?
}