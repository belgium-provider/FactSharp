using FactSharp.Http.Invoice.Request;
using FactSharp.Http.Invoice.Response;
using FactSharp.Models;
using FactSharp.Types;

namespace FactSharp.Builder;

public class CreateInvoiceBuilder(string debtorCode)
{
    private readonly CreateInvoiceRequest _request = new()
    {
        Controller = "invoice",
        Action = "add",
        DebtorCode = debtorCode
    };

    public CreateInvoiceBuilder SetDate(DateTime date) { _request.Date = date;  return this; }
    public CreateInvoiceBuilder SetStatus(EInvoiceStatus status) { _request.Status = status; return this; }
    public CreateInvoiceBuilder SetAmountPaid(float? amountPaid) { _request.AmountPaid = amountPaid; return this; }
    public CreateInvoiceBuilder SetDiscount(float? discount) { _request.Discount = discount; return this; }
    public CreateInvoiceBuilder SetIgnoreDiscount(int? ignore) { _request.IgnoreDiscount = ignore; return this; }
    public CreateInvoiceBuilder SetVatCalMethod(string? calcMethod) { _request.VatCalcMethod = calcMethod; return this; }
    public CreateInvoiceBuilder SetCompanyName(string? companyName) { _request.CompanyName = companyName; return this; }
    
    //add more fields.

    //invoice lines management
    public CreateInvoiceBuilder AddInvoiceLine(InvoiceLine line) { _request.InvoiceLines.Add(line);  return this; }
    public CreateInvoiceBuilder AddLines(List<InvoiceLine> lines) { _request.InvoiceLines = lines; return this; }

    public CreateInvoiceRequest Build() => _request;
}