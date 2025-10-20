using FactSharp.Http.Product.Request;
using FactSharp.Http.Product.Response;

namespace FactSharp.Client.Abstract;

public interface IProductClient : IBaseClient
{
    Task<ProductResponse> GetProductByIdAsync(int id);
    Task<ProductResponse> GetProductByCodeAsync(string code);
    Task<ProductListResponse> GetProductListAsync(ProductListRequest request);
}