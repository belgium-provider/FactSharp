using FactSharp.Client;
using FactSharp.Client.Abstract;
using FactSharp.Options;
using Microsoft.AspNetCore.Mvc;

namespace FactSharp.WebApp.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class TestsController(WeFactOptions options) : ControllerBase
{
    private readonly WeFactOptions _options = options;
    
    [HttpGet]
    public ActionResult TestController()
    {
        using IInvoiceClient invoiceClient = new InvoiceClient(_options.ApiKey);
        return Ok(invoiceClient.TestReturnString());
    }
}