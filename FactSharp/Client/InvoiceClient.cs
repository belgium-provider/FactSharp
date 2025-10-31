using FactSharp.Client.Abstract;
using FactSharp.Http;
using FactSharp.Http.Invoice.Request;
using FactSharp.Http.Invoice.Response;

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
    /// Sending an invoice by email using it's unique code
    /// </summary>
    /// <param name="invoiceCode"></param>
    /// <returns></returns>
    public async Task<InvoiceResponse> SendInvoiceByRefAsync(string invoiceCode) => await this.PostAsync<InvoiceResponse>(new InvoiceRequestByCode(){Controller = "invoice", Action = "sendbyemail", InvoiceCode = invoiceCode});

    /// <summary>
    /// Sending an invoice by email using it's unique id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<InvoiceResponse> SendInvoiceByIdAsync(int id) => await this.PostAsync<InvoiceResponse>(new InvoiceRequestById() { Controller = "invoice", Action = "sendbyemail", Identifier = id });

    /// <summary>
    /// Retrieve a list of invoices.
    /// </summary>
    /// <param name="invoiceListRequest"></param>
    /// <returns></returns>
    public async Task<InvoiceListResponse> GetInvoiceListAsync(InvoiceListRequest invoiceListRequest) =>  await this.PostAsync<InvoiceListResponse>(invoiceListRequest);

    /// <summary>
    /// Creating a new invoice
    /// </summary>
    /// <param name="createInvoiceRequest"></param>
    /// <returns></returns>
    public async Task<CreateInvoiceResponse> CreateInvoiceAsync(CreateInvoiceRequest createInvoiceRequest)
    {
        if(createInvoiceRequest.InvoiceLines.Count == 0)
            return BaseResponseObject.CreateErrorObject<CreateInvoiceResponse>("invoice", "invoice list", "invoice list empty");
                
        return await this.PostAsync<CreateInvoiceResponse>(createInvoiceRequest);
    }

    /// <summary>
    /// marking an invoice as paid
    /// </summary>
    /// <param name="invoiceCode"></param>
    /// <param name="paymentMethod"></param>
    /// <param name="paidDate"></param>
    /// <returns></returns>
    public async Task<MarkAsPaidResponse> MarkAsPaidAsync(string invoiceCode, string paymentMethod, DateTime? paidDate = null) => await this.PostAsync<MarkAsPaidResponse>(new MarkAsPaidRequest(){Controller = "invoice", Action = "markaspaid", PaymentMethod = paymentMethod, InvoiceCode = invoiceCode, PayDate = paidDate ?? DateTime.Now});
}