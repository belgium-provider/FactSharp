using FactSharp.Client.Abstract;
using FactSharp.Options;

namespace FactSharp.Client;

public class InvoiceClient(string apiKey, HttpClient? httpClient = null) : BaseClient(apiKey, httpClient), IInvoiceClient
{
    public string TestReturnString() => "my test string";
}