using FactSharp.Http.Invoice.Request;
using FactSharp.Http.Invoice.Response;

namespace FactSharp.Builder;

public class InvoiceListRequestBuilder
{
    private readonly InvoiceListRequest _request = new()
    {
        Controller = "invoice",
        Action = "list",
    };
    
    public InvoiceListRequestBuilder SetOffset(int offset) {_request.Offset = offset; return this;}
    public InvoiceListRequestBuilder SetLimit(int limit) {_request.Limit = limit; return this;}
    public InvoiceListRequestBuilder SetOrder(string order) { _request.Order = order; return this; }
    public InvoiceListRequestBuilder SetSort(string sort) { _request.Sort = sort; return this; }
    public InvoiceListRequestBuilder SetStats(string? status) { _request.Status = status; return this; }
    public InvoiceListRequestBuilder SetCreated(DateTime? created) { _request.Created = created; return this; }
    public InvoiceListRequestBuilder SetModified(DateTime? modified) { _request.Modified = modified; return this; }
    
    public InvoiceListRequest Build() =>  _request;
}