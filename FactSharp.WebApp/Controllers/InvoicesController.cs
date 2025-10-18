using FactSharp.Client;
using FactSharp.Client.Abstract;
using FactSharp.Http.Invoice;
using FactSharp.Options;
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
}