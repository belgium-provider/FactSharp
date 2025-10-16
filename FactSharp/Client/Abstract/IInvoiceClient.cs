using FactSharp.Http.Invoice;

namespace FactSharp.Client.Abstract;

public interface IInvoiceClient : IBaseClient
{
    Task<InvoiceResponse> GetInvoiceByCodeAsync(string invoiceCode);
}