using FactSharp.Models;
using FactSharp.Types;

namespace FactSharp.Http.Invoice.Request;

public class CreateInvoiceRequest : BaseRequestObject
{
    internal CreateInvoiceRequest() {}
    
    //default
    public override required string Controller { get; set; } = "invoice";
    public override required string Action { get; set; } = "add";
    
    //required
    public required string DebtorCode { get; set; }
    public EInvoiceStatus Status { get; set; } = EInvoiceStatus.Concept;
    public List<InvoiceLine> InvoiceLines { get; set; } = [];
    public DateTime Date { get; set; } = DateTime.Now;

    //other
    public float? AmountPaid { get; set; } = null;
    public float? Discount { get; set; } = null;
    public int? IgnoreDiscount { get; set; } = null;
    public string? VatCalcMethod { get; set; } = null; //refer to type.
    public string? CompanyName { get; set; } = null;

    //other. => set later.
}