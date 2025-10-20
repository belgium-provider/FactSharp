using FactSharp.Builder;
using FactSharp.Client;
using FactSharp.Client.Abstract;
using FactSharp.Http.Product.Request;
using FactSharp.Http.Product.Response;
using FactSharp.Options;
using Microsoft.AspNetCore.Mvc;

namespace FactSharp.WebApp.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ProductsController(WeFactOptions options) : ControllerBase
{
    private readonly WeFactOptions _options = options;
    
    /// <summary>
    /// Retrieve a product using it's code ref
    /// </summary>
    /// <param name="productCode"></param>
    /// <returns></returns>
    [HttpGet("ref/{productCode}")]
    public async Task<ActionResult<ProductResponse>> GetProductByReferenceAsync(string productCode)
    {
        using IProductClient client = new ProductClient(_options.ApiKey);
        ProductResponse product = await client.GetProductByCodeAsync(productCode);
        return Ok(product);
    }
    
    /// <summary>
    /// Retrieve a single product using it's id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductResponse>> GetProductByIdAsync(int id)
    {
        using IProductClient client = new ProductClient(_options.ApiKey);
        ProductResponse product = await client.GetProductByIdAsync(id);
        return Ok(product);
    }
    
    /// <summary>
    /// Retrieve a list of products
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<ProductListResponse>> GetProductListAsync()
    {
        ProductListRequest request = new ProductListRequestBuilder()
            .SetLimit(50)
            .Build();
        
        using IProductClient client = new ProductClient(_options.ApiKey);
        ProductListResponse response = await client.GetProductListAsync(request);
        return Ok(response);
    }
}