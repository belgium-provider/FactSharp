using FactSharp.Client.Abstract;
using FactSharp.Options;

namespace FactSharp.Client;

public class InvoiceClient(WeFactOptions options, HttpClient? httpClient = null) : BaseClient(options, httpClient), IInvoiceClient
{
    public void Test() => Console.WriteLine("Testing invoice");
}