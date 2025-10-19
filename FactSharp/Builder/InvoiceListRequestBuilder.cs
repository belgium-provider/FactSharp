using FactSharp.Builder.Abstract;
using FactSharp.Http.Invoice.Request;

namespace FactSharp.Builder;

public class InvoiceListRequestBuilder : BaseListRequestBuilder<InvoiceListRequest, InvoiceListRequestBuilder>
{
    public InvoiceListRequestBuilder()
    {
        Request.Controller = "invoice";
        Request.Action = "list";
        Request.Sort = "InvoiceCode";
    }

    public InvoiceListRequestBuilder SetStatus(string? status)
    {
        Request.Status = status;
        return this;
    }
    
    public override InvoiceListRequest Build() => Request;
}