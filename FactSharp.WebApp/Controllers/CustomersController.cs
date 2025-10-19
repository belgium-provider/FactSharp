using FactSharp.Builder;
using FactSharp.Client;
using FactSharp.Client.Abstract;
using FactSharp.Http.Customer.Request;
using FactSharp.Http.Customer.Response;
using FactSharp.Options;
using Microsoft.AspNetCore.Mvc;

namespace FactSharp.WebApp.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CustomersController(WeFactOptions options) : ControllerBase
{
    private readonly WeFactOptions _options = options;
    
    /// <summary>
    /// Retrieve a customer using it's id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<CustomerResponse>> GestCustomerById(int id)
    {
        using ICustomerClient client = new CustomerClient(_options.ApiKey);
        CustomerResponse wefactResponse = await client.GetCustomerByIdAsync(id);
        return Ok(wefactResponse);
    }
    
    /// <summary>
    /// Retrieve a single client based on it's debtor code
    /// </summary>
    /// <param name="debtorCode"></param>
    /// <returns></returns>
    [HttpGet("ref/{debtorCode}")]
    public async Task<ActionResult<CustomerResponse>> GetCustomerByReferenceAsync(string debtorCode)
    {
        using ICustomerClient client = new CustomerClient(_options.ApiKey);
        CustomerResponse wefactResponse = await client.GetCustomerByCodeAsync(debtorCode);
        return Ok(wefactResponse);
    }
    
    
    /// <summary>
    /// Getting a list of customers
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<CustomerListResponse>> GetCustomersAsync()
    {
        CustomerListRequest request = new CustomerListRequestBuilder()
            .SetLimit(50)
            .Build();

        using ICustomerClient client = new CustomerClient(_options.ApiKey);
        CustomerListResponse wefactResponse = await client.GetCustomerListAsync(request);
        return Ok(wefactResponse);
    }
}