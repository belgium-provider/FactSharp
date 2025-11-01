using FactSharp.Http.Invoice.Request;
using FactSharp.Http.Invoice.Response;

namespace FactSharp.Client.Abstract;

public interface IInvoiceClient : IBaseClient
{
    Task<InvoiceResponse> GetInvoiceByCodeAsync(string invoiceCode);
    Task<InvoiceResponse> GetInvoiceByIdAsync(int id);
    Task<InvoiceResponse> SendInvoiceByRefAsync(string invoiceCode);
    Task<InvoiceResponse> SendInvoiceByIdAsync(int id);
    Task<InvoiceListResponse> GetInvoiceListAsync(InvoiceListRequest invoiceListRequest);
    Task<CreateInvoiceResponse> CreateInvoiceAsync(CreateInvoiceRequest createInvoiceRequest);
    Task<InvoiceResponse> AddInvoiceLineAsync(AddInvoiceLineRequest request);
    Task<InvoiceResponse> DeleteInvoiceLineAsync(DeleteInvoiceLineRequest request);
    Task<MarkAsPaidResponse> MarkAsPaidAsync(string invoiceCode, string paymentMethod, DateTime? paidDate = null);
}