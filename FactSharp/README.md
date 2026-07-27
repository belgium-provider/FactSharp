```
                              ______           __  _____ __
                             / ____/___ ______/ /_/ ___// /_  ____ __________
                            / /_  / __ `/ ___/ __/\__ \/ __ \/ __ `/ ___/ __ \
                           / __/ / /_/ / /__/ /_ ___/ / / / / /_/ / /  / /_/ /
                          /_/    \__,_/\___/\__//____/_/ /_/\__,_/_/  / .___/
                                                                     /_/

                                   ----- WeFact C# SDK -----
```

**FactSharp** is a strongly-typed C# SDK for the [WeFact](https://www.wefact.nl/) invoicing API. It wraps invoicing operations — customers, invoices, invoice lines and products — behind a clean, async client surface, so you don't have to hand-roll HTTP calls, request payloads and response parsing.

Built and maintained by [Belgium-Provider](https://github.com/belgium-provider) to power its own WeFact integrations. Shared as-is — feel free to use it, fork it, or build on top of it for your own WeFact integration.

## Features

| Domain | Client | Covers |
|---|---|---|
| Invoices | `InvoiceClient` | Get by id/code, list, create, send by email, add/delete invoice lines, mark as paid |
| Customers | `CustomerClient` | Get by id/code, list |
| Products | `ProductClient` | Get by id/code, list |

Every client exposes a matching interface (`IInvoiceClient`, `ICustomerClient`, `IProductClient`), implements `IDisposable`, and follows a consistent `VerbNounAsync` naming convention.

## Installation

```
dotnet add package FactSharp
```

## Quickstart

```csharp
using FactSharp.Builder;
using FactSharp.Client;
using FactSharp.Client.Abstract;
using FactSharp.Http.Invoice.Request;
using FactSharp.Http.Invoice.Response;
using FactSharp.Types;

// Your WeFact API key (Instellingen > Automatisering > API in the WeFact backoffice)
using IInvoiceClient invoiceClient = new InvoiceClient(apiKey: "your-wefact-api-key");

InvoiceListRequest request = new InvoiceListRequestBuilder()
    .SetStatus(EInvoiceStatus.Paid)
    .SetLimit(50)
    .Build();

InvoiceListResponse response = await invoiceClient.GetInvoiceListAsync(request);

if (response.Errors is { Count: > 0 })
{
    Console.WriteLine($"Error: {string.Join(", ", response.Errors)}");
    return;
}

foreach (var invoice in response.Invoices)
    Console.WriteLine($"{invoice.InvoiceCode} — {invoice.AmountIncl} {invoice.Currency}");
```

Every domain follows the same pattern: instantiate the client with your API key, build a request (directly, or through a fluent `*RequestBuilder` for list endpoints), await the call, and check `response.Errors` before reading the payload. Every response inherits from `BaseResponseObject`, so error handling is identical across the whole SDK — the client never throws for expected/API failures.

## ASP.NET Core / DI

```csharp
builder.Services.AddWeFactApi(options =>
{
    options.ApiKey = Environment.GetEnvironmentVariable("WE_FACT") ?? string.Empty;
});
```

Inject the resulting `WeFactOptions` wherever you need a client and construct it per call:

```csharp
public class InvoicesController(WeFactOptions options) : ControllerBase
{
    [HttpGet("{id:int}")]
    public async Task<ActionResult<InvoiceResponse>> GetInvoiceByIdAsync(int id)
    {
        using IInvoiceClient client = new InvoiceClient(options.ApiKey);
        return Ok(await client.GetInvoiceByIdAsync(id));
    }
}
```

## Requirements

- .NET 8.0 SDK or later
- A WeFact account with an API key

## Resources

- Full documentation, domain reference and samples: https://github.com/belgium-provider/FactSharp
- Official WeFact API documentation: https://developer.wefact.com/?_locale=nl_NL

## License

MIT
