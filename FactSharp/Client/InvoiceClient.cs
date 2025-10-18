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
    public async Task<InvoiceResponse> GetInvoiceByCodeAsync(string invoiceCode) => await this.PostAsync<InvoiceResponse>(new InvoiceRequestByCode(){Controller = "invoice", Action = "show", InvoiceCode = invoiceCode});

    /// <summary>
    /// Retrieve a single invoice using it's unique id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<InvoiceResponse> GetInvoiceByIdAsync(int id) => await this.PostAsync<InvoiceResponse>(new InvoiceRequestById() { Controller = "invoice", Action = "show", Identifier = id });

    /// <summary>
    /// Retrieve a list of invoices.
    /// </summary>
    /// <param name="invoiceListRequest"></param>
    /// <returns></returns>
    public Task<InvoiceListResponse> GetInvoiceListAsync(InvoiceListRequest invoiceListRequest) => this.PostAsync<InvoiceListResponse>(invoiceListRequest);
}