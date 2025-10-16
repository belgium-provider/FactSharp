using FactSharp.Client.Abstract;
using FactSharp.Http.Invoice;
using FactSharp.Options;

namespace FactSharp.Client;

public class InvoiceClient(string apiKey, HttpClient? httpClient = null) : BaseClient(apiKey, httpClient), IInvoiceClient
{
    /// <summary>
    /// Retrieve a single invoice using it's unique code
    /// </summary>
    /// <param name="invoiceCode"></param>
    /// <returns></returns>
    public async Task<InvoiceResponse> GetInvoiceByCodeAsync(string invoiceCode) => await this.PostAsync<InvoiceResponse>(new InvoiceRequest(){Controller = "invoice", Action = "show", InvoiceCode = invoiceCode});
}