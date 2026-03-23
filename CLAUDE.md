# FactSharp — Claude Context

> C# SDK for the [WeFact API](https://www.wefact.nl/api/) (WeMijnFact).
> Designed for Belgian VoIP integrations. Targets **.NET 8.0**.

---

## Project Structure

```
FactSharp/                      # Core SDK (NuGet-publishable library)
├── Builder/                    # Fluent builders for complex requests
│   └── Abstract/               # BaseListRequestBuilder<TRequest, TBuilder>
├── Client/                     # HTTP client implementations
│   └── Abstract/               # Interfaces (IBaseClient, IInvoiceClient…)
├── Factory/                    # Simplified object creation helpers
├── Http/                       # Request & Response DTOs
│   ├── {Domain}/Request/       # One file per API action
│   └── {Domain}/Response/      # One file per API action
├── Models/                     # Domain models (Invoice, Customer, Product…)
├── Options/                    # WeFactOptions (API key, base URL)
└── Types/                      # Enums & string-constant classes

FactSharp.WebApp/               # ASP.NET Core demo app (not production)
├── Controllers/                # One controller per domain
└── Program.cs
```

---

## Architecture & Key Patterns

### 1. Client Layer

Each domain (`Invoice`, `Customer`, `Product`) has:
- An **interface** in `Client/Abstract/I{Domain}Client.cs`
- A **concrete class** in `Client/{Domain}Client.cs` that extends `BaseClient`

`BaseClient` owns the `HttpClient`, injects the API key, and exposes a single generic `PostAsync<T>` that posts JSON and deserializes the response.

```
IBaseClient
  └── BaseClient (abstract)
        ├── InvoiceClient  → IInvoiceClient
        ├── CustomerClient → ICustomerClient
        └── ProductClient  → IProductClient
```

### 2. HTTP DTOs

Every API call maps to a pair of files under `Http/{Domain}/`:

| File | Purpose |
|---|---|
| `Request/{Action}Request.cs` | Inherits `BaseRequestObject` or `BaseListRequestObject` |
| `Response/{Action}Response.cs` | Inherits `BaseResponseObject` or `BaseListObjectResponse` |

`BaseRequestObject` always carries `api_key`, `controller`, `action` (set by the concrete request).

### 3. Builder Pattern (Fluent API)

Complex requests use builders. All list builders extend `BaseListRequestBuilder<TRequest, TBuilder>` (offset, limit, order, sort, created, modified). Domain-specific list builders add their own filters (`SetStatus`, `SetGroup`…).

Object-creation builders (`CreateInvoiceBuilder`, `InvoiceLineBuilder`) return the fully-populated request/model when `.Build()` is called.

### 4. Factory Pattern

Factories provide quick-start helpers with only the required fields:
- `InvoiceLineFactory.CreateBaseLine(priceExcl, description, date)`
- `CreateInvoiceFactory.CreateBaseInvoice(debtorCode, status, lines?)`

### 5. Dependency Injection

`DependencyInjection.cs` exposes `AddWeFactApi(services, options => …)`.
`WeFactOptions` holds the API key. Clients are registered and resolved via DI.

---

## How to Implement a New Feature

Follow these steps exactly when adding a new domain or a new action to an existing domain.

### Step 1 — Request DTO

Create `Http/{Domain}/Request/{Action}Request.cs`:

```csharp
namespace FactSharp.Http.{Domain}.Request;

public class {Action}Request : BaseRequestObject   // or BaseListRequestObject
{
    // Set controller and action in constructor
    public {Action}Request()
    {
        Controller = "{wefact_controller}";
        Action     = "{wefact_action}";
    }

    // Map each WeFact field with [JsonProperty("snake_case_name")]
    [JsonProperty("field_name")]
    public required string FieldName { get; init; }
}
```

### Step 2 — Response DTO

Create `Http/{Domain}/Response/{Action}Response.cs`:

```csharp
namespace FactSharp.Http.{Domain}.Response;

public class {Action}Response : BaseResponseObject   // or BaseListObjectResponse
{
    [JsonProperty("domain_object")]
    public {DomainModel}? DomainObject { get; init; }
}
```

### Step 3 — Domain Model (if new)

Create `Models/{Domain}.cs` with all fields the API returns. Use `[JsonProperty]` for every non-trivial mapping.

### Step 4 — Interface Method

Add the method signature to `Client/Abstract/I{Domain}Client.cs`:

```csharp
Task<{Action}Response> {Action}Async({ParamType} param, CancellationToken ct = default);
```

### Step 5 — Client Implementation

Implement the method in `Client/{Domain}Client.cs`:

```csharp
public async Task<{Action}Response> {Action}Async({ParamType} param, CancellationToken ct = default)
{
    var request = new {Action}Request { FieldName = param };
    return await PostAsync<{Action}Response>(request, ct);
}
```

Always validate required fields before calling the API (e.g., empty collections, null strings).

### Step 6 — Builder (if complex request)

- **List request** → extend `BaseListRequestBuilder<{Action}Request, {Action}Builder>`, add domain-specific `Set*` methods.
- **Object creation** → create a standalone builder with fluent `Set*` methods and a `Build()` returning the request.

### Step 7 — Factory (if useful shortcut exists)

Add a static method to the relevant factory (or create `{Domain}Factory.cs`) for the most common creation scenario using only required parameters.

### Step 8 — WebApp Controller (demo only)

Add the corresponding endpoint to `FactSharp.WebApp/Controllers/{Domain}Controller.cs` to demonstrate usage.

### Step 9 — DI Registration

If registering a new client, add it in `DependencyInjection.cs`.

---

## Naming Conventions

| Element | Convention | Example |
|---|---|---|
| Namespace | Match folder path exactly | `FactSharp.Http.Invoice.Request` |
| Request class | `{Action}Request` | `MarkAsPaidRequest` |
| Response class | `{Action}Response` | `MarkAsPaidResponse` |
| Interface | `I{Domain}Client` | `IInvoiceClient` |
| Client class | `{Domain}Client` | `InvoiceClient` |
| Builder class | `{Action}Builder` | `CreateInvoiceBuilder` |
| Factory class | `{Domain}Factory` | `InvoiceLineFactory` |
| Enum | `E{Name}` | `EInvoiceStatus` |
| String-constant class | PascalCase | `PaymentMethod`, `Currency` |

---

## Code Style Checklist

- File-scoped namespaces (`namespace Foo.Bar;`)
- Nullable reference types enabled — never use `!` unless unavoidable
- `required` keyword for mandatory DTO properties, `init` for immutability
- `async`/`await` end-to-end; method names end in `Async`
- `CancellationToken ct = default` as the last parameter on every async method
- `[JsonProperty("snake_case")]` on every property that maps to a WeFact field
- Builders return `this` for chaining; final method is `Build()`
- No magic strings — use `Types/` constants

---

## Skills

| Skill | File | When to use |
|---|---|---|
| Git conventions | `.claude/skills/git-conventions.md` | Every commit or branch name |
| C# conventions | `.claude/skills/csharp-conventions.md` | Any C# code question or review |

## Commands

Slash commands available in this project (type them directly in the chat):

| Command | Description |
|---|---|
| `/create-commit` | Stage all changes (`git add -A`), generate and create a conventional commit following git-conventions skill. Never pushes. Optionally pass a scope: `/create-commit api` |

---

## Dependencies

| Package | Version | Purpose |
|---|---|---|
| `Newtonsoft.Json` | 13.0.4 | JSON serialization |
| `Microsoft.Extensions.DependencyInjection.Abstractions` | 9.0.9 | DI support |
| `DotNetEnv` | 3.1.1 | `.env` loading (WebApp only) |
| `Swashbuckle.AspNetCore` | 6.6.2 | Swagger (WebApp only) |

---

## Environment

The API key is stored in `.env` (WebApp) and loaded via `DotNetEnv`:
```
WE_FACT=your_api_key_here
```
Never commit a real API key. The `.env.example` file serves as the template.
