using FactSharp.Builder;
using FactSharp.Client;
using FactSharp.Client.Abstract;
using FactSharp.Factory;
using FactSharp.Models;
using FactSharp.Options;
using FactSharp.Types;
using FactSharp.Http.Invoice.Request;
using FactSharp.Http.Invoice.Response;
using Microsoft.AspNetCore.Mvc;

namespace FactSharp.WebApp.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class InvoicesController(WeFactOptions options) : ControllerBase
{
    private readonly WeFactOptions _options = options;
    
    /// <summary>
    /// retrieve an invoice using it's ref.
    /// </summary>
    /// <param name="invoiceCode"></param>
    /// <returns></returns>
    [HttpGet("ref/{invoiceCode}")]
    public async Task<ActionResult<InvoiceResponse>> GetInvoiceByRefAsync(string invoiceCode)
    {
        using IInvoiceClient client = new InvoiceClient(_options.ApiKey);
        InvoiceResponse wefactResponse = await client.GetInvoiceByCodeAsync(invoiceCode);
        return Ok(wefactResponse);
    }
    
    /// <summary>
    /// retrieving an invoice using it's id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<InvoiceResponse>> GetInvoiceByIdAsync(int id)
    {
        using IInvoiceClient client = new InvoiceClient(_options.ApiKey);
        InvoiceResponse wefactResponse = await client.GetInvoiceByIdAsync(id);
        return Ok(wefactResponse);
    }
    
    /// <summary>
    /// Retrieve a list of invoices.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<InvoiceListResponse>> GetInvoicesAsync()
    {
        //Crafting object
        InvoiceListRequest requestObject = new InvoiceListRequestBuilder()
            .SetStatus(Types.InvoiceStatus.Paid) //paid
            .SetLimit(50)
            .Build();
        
        using IInvoiceClient client = new InvoiceClient(_options.ApiKey);
        InvoiceListResponse wefactResponse = await client.GetInvoiceListAsync(requestObject);
        return Ok(wefactResponse);
    }
    
    /// <summary>
    /// Sample showing how to use the CreateInvoice method
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<CreateInvoiceResponse>> CreateInvoiceAsync()
    {
        List<InvoiceLine> lines =
        [
            InvoiceLineFactory.CreateBaseLine(10.00m, "Mollie payment fees", DateTime.Now),
            InvoiceLineFactory.CreateProductLine(10.00m, "Mollie payment fees", DateTime.Now, "PRODUCT_CODE")

        ];

        CreateInvoiceRequest invoiceRequest = new CreateInvoiceBuilder(debtorCode:"YOUR_DEBTOR_CODE")
            .SetStatus(EInvoiceStatus.Paid) //paid invoice
            .AddLines(lines)
            .Build();

        using IInvoiceClient client = new InvoiceClient(_options.ApiKey);
        CreateInvoiceResponse wefactResponse = await client.CreateInvoiceAsync(invoiceRequest);
        return Ok(wefactResponse);
    }
    
    /// <summary>
    /// Marking an invoice as paid
    /// </summary>
    /// <returns></returns>
    [HttpPost("pay")]
    public async Task<ActionResult<MarkAsPaidResponse>> MarkInvoiceAsPaidAsync()
    {
        using IInvoiceClient client = new InvoiceClient(_options.ApiKey);
        MarkAsPaidResponse wefactResponse = await client.MarkAsPaidAsync("INVOICE_CODE", Types.PaymentMethod.Other, DateTime.Now);
        return Ok(wefactResponse);
    }
}