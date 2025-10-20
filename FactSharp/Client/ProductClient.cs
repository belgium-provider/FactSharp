using FactSharp.Client.Abstract;
using FactSharp.Http.Product.Request;
using FactSharp.Http.Product.Response;

namespace FactSharp.Client;

public class ProductClient(string apiKey, HttpClient? httpClient = null) : BaseClient(apiKey, httpClient), IProductClient
{
    /// <summary>
    /// Retrieve a single product using it's unique id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<ProductResponse> GetProductByIdAsync(int id) => await this.PostAsync<ProductResponse>(new ProductByIdRequest(){Controller = "product", Action = "show", Identifier = id});

    /// <summary>
    /// Retrieve a single product using it's code ref
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    public async Task<ProductResponse> GetProductByCodeAsync(string code) => await this.PostAsync<ProductResponse>(new ProductByCodeRequest(){Controller = "product", Action = "show", ProductCode = code});

    /// <summary>
    /// Retrieve all products
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<ProductListResponse> GetProductListAsync(ProductListRequest request) => await this.PostAsync<ProductListResponse>(request);
}