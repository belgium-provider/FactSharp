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
        InvoiceResponse invoice = await client.GetInvoiceByCodeAsync(invoiceCode);
        return Ok(invoice);
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
        InvoiceResponse invoice = await client.GetInvoiceByIdAsync(id);
        return Ok(invoice);
    }
    
    /// <summary>
    /// Retrieve a list of invoices.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<InvoiceListResponse>> GetInvoicesAsync([FromQuery] EInvoiceStatus? status = null, [FromQuery] int? page = null)
    {
        //Crafting object
        InvoiceListRequest requestObject = new InvoiceListRequestBuilder()
            .SetStatus(status ?? EInvoiceStatus.Paid) //paid
            .SetLimit(50)
            .SetOffset(page ?? 1)
            .Build();
        
        using IInvoiceClient client = new InvoiceClient(_options.ApiKey);
        InvoiceListResponse invoiceList = await client.GetInvoiceListAsync(requestObject);
        return Ok(invoiceList);
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

        CreateInvoiceRequest invoiceRequest = CreateInvoiceFactory.CreateBaseInvoice("DEBTOR_CODE", EInvoiceStatus.Paid, lines);
        using IInvoiceClient client = new InvoiceClient(_options.ApiKey);
        CreateInvoiceResponse response = await client.CreateInvoiceAsync(invoiceRequest);
        return Ok(response);
    }
    
    /// <summary>
    /// Marking an invoice as paid
    /// </summary>
    /// <returns></returns>
    [HttpPost("pay/{invoiceCode}")]
    public async Task<ActionResult<MarkAsPaidResponse>> MarkInvoiceAsPaidAsync(string invoiceCode)
    {
        using IInvoiceClient client = new InvoiceClient(_options.ApiKey);
        MarkAsPaidResponse response = await client.MarkAsPaidAsync(invoiceCode, Types.PaymentMethod.Other, DateTime.Now);
        return Ok(response);
    }
    
    /// <summary>
    /// Sending a single invoice by email
    /// </summary>
    /// <param name="invoiceCode"></param>
    /// <returns></returns>
    [HttpPost("send/{invoiceCode}")]
    public async Task<ActionResult<InvoiceResponse>> SendInvoiceByEmailAsync(string invoiceCode)
    {
        using IInvoiceClient client = new InvoiceClient(_options.ApiKey);
        InvoiceResponse response = await client.SendInvoiceByRefAsync(invoiceCode);
        return Ok(response);
    }
}