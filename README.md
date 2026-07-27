```
                              ______           __  _____ __
                             / ____/___ ______/ /_/ ___// /_  ____ __________
                            / /_  / __ `/ ___/ __/\__ \/ __ \/ __ `/ ___/ __ \
                           / __/ / /_/ / /__/ /_ ___/ / / / / /_/ / /  / /_/ /
                          /_/    \__,_/\___/\__//____/_/ /_/\__,_/_/  / .___/
                                                                     /_/

                                   ----- WeFact C# SDK -----
```

![NuGet Version](https://img.shields.io/nuget/v/FactSharp?label=NuGet)
![NuGet Downloads](https://img.shields.io/nuget/dt/FactSharp)
![.NET 8](https://img.shields.io/badge/.NET-8.0-512BD4)
![MIT License](https://img.shields.io/badge/license-MIT-green)

**FactSharp** is a strongly-typed C# SDK for the [WeFact](https://www.wefact.nl/) invoicing API. It wraps invoicing operations — customers, invoices, invoice lines and products — behind a clean, async client surface, so you don't have to hand-roll HTTP calls, request payloads and response parsing.

Built and maintained by [Belgium-Provider](https://github.com/belgium-provider) to power its own WeFact integrations.

> [!IMPORTANT]
> This project started as an internal base library for Belgium-Provider's own tooling. It's shared as-is — feel free to use it, fork it, or build on top of it for your own WeFact integration.

## Features

| Domain | Client | Covers |
|---|---|---|
| Invoices | `InvoiceClient` | Get by id/code, list, create, send by email, add/delete invoice lines, mark as paid |
| Customers | `CustomerClient` | Get by id/code, list |
| Products | `ProductClient` | Get by id/code, list |

Every client targets .NET 8.0, exposes a matching interface (`IInvoiceClient`, `ICustomerClient`, `IProductClient`) for testing/abstraction, implements `IDisposable`, and follows a consistent `VerbNounAsync` naming convention across the whole API surface.

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
    .SetOffset(1)
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

Every domain follows the same pattern: instantiate the client with your API key, build a request (directly or through a fluent `*RequestBuilder` for list endpoints), await the call, and check `response.Errors` before reading the payload.

## Project structure

```
FactSharp/
  Client/       InvoiceClient, CustomerClient, ProductClient — all extend BaseClient
    Abstract/   IInvoiceClient, ICustomerClient, IProductClient, IBaseClient
  Builder/      Fluent builders for requests and invoice lines
    Abstract/   Generic BaseListRequestBuilder<TRequest, TBuilder>
  Factory/      Convenience factories (CreateInvoiceFactory, InvoiceLineFactory)
  Http/         Request / Response DTOs grouped by domain (Invoice, Customer, Product)
  Models/       Plain domain models (Invoice, InvoiceLine, Customer, Product, ...)
  Options/      WeFactOptions (API key holder)
  Types/        Enum/constant helpers (EInvoiceStatus, PaymentMethod, Currency, VatCalcMethod, Order)
  DependencyInjection.cs   AddWeFactApi() extension for Microsoft.Extensions.DependencyInjection
```

All calls funnel through `BaseClient.PostAsync<T>`, which serializes the request to JSON, POSTs it to the WeFact API endpoint, and deserializes the result into a response inheriting `BaseResponseObject`. Network failures, non-success HTTP responses and API-level errors are all normalized into that same response shape — the SDK does not throw for expected/API failures.

## Domain reference

### InvoiceClient — invoices & invoice lines

```csharp
using IInvoiceClient client = new InvoiceClient(apiKey);
```

| Method | WeFact controller / action |
|---|---|
| `GetInvoiceByCodeAsync` / `GetInvoiceByIdAsync` | `invoice` / `show` |
| `GetInvoiceListAsync` | `invoice` / `list` |
| `CreateInvoiceAsync` | `invoice` / `add` |
| `SendInvoiceByRefAsync` / `SendInvoiceByIdAsync` | `invoice` / `sendbyemail` |
| `MarkAsPaidAsync` | `invoice` / `markaspaid` |
| `AddInvoiceLineAsync` | `invoiceline` / `add` |
| `DeleteInvoiceLineAsync` | `invoiceline` / `delete` |

### CustomerClient — customers (debtors)

```csharp
using ICustomerClient client = new CustomerClient(apiKey);
```

| Method | WeFact controller / action |
|---|---|
| `GetCustomerByIdAsync` / `GetCustomerByCodeAsync` | `debtor` / `show` |
| `GetCustomerListAsync` | `debtor` / `list` |

### ProductClient — products

```csharp
using IProductClient client = new ProductClient(apiKey);
```

| Method | WeFact controller / action |
|---|---|
| `GetProductByIdAsync` / `GetProductByCodeAsync` | `product` / `show` |
| `GetProductListAsync` | `product` / `list` |

## Builders & factories

List endpoints (`GetInvoiceListAsync`, `GetCustomerListAsync`, `GetProductListAsync`) take a request built through a fluent `*RequestBuilder`, all sharing the same base options via `BaseListRequestBuilder<TRequest, TBuilder>`:

```csharp
CustomerListRequest request = new CustomerListRequestBuilder()
    .SetGroup(3)
    .SetLimit(50)
    .SetOffset(1)
    .SetOrder(Order.Desc)
    .SetSort("Modified")
    .Build();
```

Creating an invoice combines `CreateInvoiceBuilder` for the invoice itself and `InvoiceLineBuilder` for each line:

```csharp
using FactSharp.Models;

InvoiceLine line = new InvoiceLineBuilder(priceExcl: 99.00m, description: "Hosting — July", date: DateTime.Today)
    .SetProductCode("HOSTING-01")
    .SetTaxPercentage(21)
    .Build();

CreateInvoiceRequest invoiceRequest = new CreateInvoiceBuilder(debtorCode: "DEB0001")
    .SetStatus(EInvoiceStatus.Concept)
    .AddInvoiceLine(line)
    .Build();

CreateInvoiceResponse response = await invoiceClient.CreateInvoiceAsync(invoiceRequest);
```

`CreateInvoiceFactory` and `InvoiceLineFactory` shortcut the most common cases, skipping the builder calls entirely:

```csharp
using FactSharp.Factory;

List<InvoiceLine> lines =
[
    InvoiceLineFactory.CreateBaseLine(10.00m, "Mollie payment fees", DateTime.Today),
    InvoiceLineFactory.CreateProductLine(10.00m, "Hosting fees", DateTime.Today, "HOSTING-01")
];

CreateInvoiceRequest request = CreateInvoiceFactory.CreateBaseInvoice("DEB0001", EInvoiceStatus.Paid, lines);
```

## Types reference

`FactSharp.Types` groups the WeFact enumerations/constants used across requests and responses:

| Type | Values |
|---|---|
| `EInvoiceStatus` | `Concept`, `Sent`, `PartlyPaid`, `Paid`, `Credit`, `Expired` |
| `PaymentMethod` | `BankTransfer`, `Cash`, `PinPayment`, `DirectDebit`, `Accounting`, `Various`, `Paypal`, `Ideal`, `QrCode`, `Other` |
| `Currency` | `Eur`, `Usd` |
| `VatCalcMethod` | `Excl`, `Incl` |
| `Order` | `Asc`, `Desc` |

## Dependency injection (ASP.NET Core / generic host)

`AddWeFactApi` registers a validated `WeFactOptions` singleton holding your API key:

```csharp
builder.Services.AddWeFactApi(options =>
{
    options.ApiKey = Environment.GetEnvironmentVariable("WE_FACT") ?? string.Empty;
});
```

It throws at startup if no API key is provided. Clients are lightweight and disposable, so inject `WeFactOptions` where you need it and construct the client per call:

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

See `FactSharp.WebApp` in this repository for a full sample covering invoices, customers and products.

## Error handling

FactSharp doesn't throw on API-level or transport errors — it normalizes them. Every response inherits from `BaseResponseObject`:

```csharp
public abstract class BaseResponseObject
{
    public string Status { get; set; }
    public string Action { get; set; }
    public string Controller { get; set; }
    public DateTime Date { get; set; }
    public List<string>? Errors { get; set; }
}
```

`Errors` is `null`/empty on success. Always check it before reading the rest of the response:

```csharp
var response = await client.CreateInvoiceAsync(request);
if (response.Errors is { Count: > 0 })
{
    // handle/log response.Errors / response.Status
    return;
}
```

## Requirements

- .NET 8.0 SDK or later
- A WeFact account with an API key

## Resources

- Official WeFact API documentation: https://developer.wefact.com/?_locale=nl_NL
- Source & issues: https://github.com/belgium-provider/FactSharp

## License

MIT
