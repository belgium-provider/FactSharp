using FactSharp.Client.Abstract;
using FactSharp.Http.Customer.Request;
using FactSharp.Http.Customer.Response;

namespace FactSharp.Client;

public class CustomerClient(string apiKey, HttpClient? httpClient = null) : BaseClient(apiKey, httpClient), ICustomerClient
{
    /// <summary>
    /// Retrieve a customer using it's unique id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<CustomerResponse> GetCustomerByIdAsync(int id) => await this.PostAsync<CustomerResponse>(new CustomerRequestById(){Identifier = id, Controller = "debtor", Action = "show"});
    
    /// <summary>
    /// Retrieve a customer using it's debtor code
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    public async Task<CustomerResponse> GetCustomerByCodeAsync(string code) => await this.PostAsync<CustomerResponse>(new CustomerRequestByCode(){DebtorCode = code,  Controller = "debtor", Action = "show"});

    /// <summary>
    /// Get list of customers
    /// </summary>
    /// <param name="customerListRequest"></param>
    /// <returns></returns>
    public async Task<CustomerListResponse> GetCustomerListAsync(CustomerListRequest customerListRequest) => await this.PostAsync<CustomerListResponse>(customerListRequest);
}