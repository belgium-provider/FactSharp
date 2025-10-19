using FactSharp.Http.Customer.Request;
using FactSharp.Http.Customer.Response;

namespace FactSharp.Client.Abstract;

public interface ICustomerClient : IBaseClient
{
    Task<CustomerResponse> GetCustomerByIdAsync(int id);
    Task<CustomerResponse> GetCustomerByCodeAsync(string code);
    Task<CustomerListResponse> GetCustomerListAsync(CustomerListRequest request);
}