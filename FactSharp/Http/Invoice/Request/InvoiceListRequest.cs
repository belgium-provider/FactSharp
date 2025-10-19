namespace FactSharp.Http.Invoice.Request;

public class InvoiceListRequest : BaseRequestObject
{
    internal InvoiceListRequest() { } //forcing usage of Builder for creating objects.
    
    //default
    public override required string Controller { get; set; } = "invoice";
    public override required string Action { get; set; } = "list";
    public int Offset { get; set; } = 0;
    public int Limit { get; set; } = 1000; //standard 
    public string Order { get; set; } = Types.Order.Asc; //refer to Order model.
    public string Sort { get; set; } = "InvoiceCode";

    //other
    public string? Status { get; set; } = null; //refer to InvoiceStatus => available status
    public DateTime? Created { get; set;  } = null;
    public DateTime? Modified { get; set; } = null;
    
    //other ?
}