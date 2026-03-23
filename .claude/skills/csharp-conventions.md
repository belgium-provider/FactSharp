# Skill: C# Conventions

> All C# code in this project follows official Microsoft guidelines.
> Reference: [Microsoft C# Coding Conventions](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)
> Reference: [.NET Design Guidelines](https://learn.microsoft.com/en-us/dotnet/standard/design-guidelines/)

---

## Naming

| Element | Style | Example |
|---|---|---|
| Namespace | PascalCase, mirrors folder path | `FactSharp.Http.Invoice.Request` |
| Class / Record / Struct | PascalCase | `InvoiceClient`, `WeFactOptions` |
| Interface | `I` prefix + PascalCase | `IInvoiceClient` |
| Enum type | `E` prefix + PascalCase (project convention) | `EInvoiceStatus` |
| Enum member | PascalCase | `EInvoiceStatus.Paid` |
| Method | PascalCase; async methods end in `Async` | `GetInvoiceByCodeAsync` |
| Property | PascalCase | `DebtorCode`, `AmountIncl` |
| Field (private) | `_camelCase` | `_httpClient` |
| Parameter / local var | camelCase | `invoiceCode`, `ct` |
| Constant | PascalCase | `PaymentMethod.BankTransfer` |
| Generic type param | `T` or descriptive `T`-prefix | `TRequest`, `TBuilder` |

**Never** use Hungarian notation (`strName`, `intCount`).
**Never** use underscores in type, method, or property names (except private fields).

---

## File & Namespace Layout

- One type per file; filename matches type name exactly.
- Use **file-scoped namespaces** (C# 10+):
  ```csharp
  namespace FactSharp.Http.Invoice.Request;   // no braces
  ```
- Namespace must reflect the folder path from the project root.
- `using` directives go at the top, outside the namespace declaration.
- Order: system namespaces first, then third-party, then project namespaces; each group alphabetically sorted.

---

## Types & Modifiers

### Classes

```csharp
// Prefer sealed unless designed for inheritance
public sealed class WeFactOptions
{
    public required string ApiKey { get; init; }
}
```

### Records (for pure data)

Prefer `record` or `record class` for immutable DTOs when no custom behavior is needed.

### Interfaces

Define one interface per abstraction. Keep interfaces small (ISP).

```csharp
public interface IInvoiceClient : IBaseClient
{
    Task<InvoiceResponse> GetInvoiceByCodeAsync(string invoiceCode, CancellationToken ct = default);
}
```

### Enums

```csharp
public enum EInvoiceStatus
{
    Concept    = 0,
    Sent       = 2,
    PartlyPaid = 3,
    Paid       = 4,
    Credit     = 8,
    Expired    = 9,
}
```

Use explicit integer values when they map to external API values.

---

## Properties & Fields

```csharp
// Immutable DTO property — use required + init
public required string InvoiceCode { get; init; }

// Mutable property
public string? CompanyName { get; set; }

// Private backing field
private readonly HttpClient _httpClient;
```

- Prefer `{ get; init; }` for request/response objects (immutable after construction).
- Use `required` instead of constructor-enforced nullability where applicable.
- Keep fields `private readonly` unless a specific reason exists.
- Never expose public fields — always use properties.

---

## Nullable Reference Types

Nullable reference types are **enabled** project-wide (`<Nullable>enable</Nullable>`).

```csharp
// Non-nullable: caller must provide a value
public required string DebtorCode { get; init; }

// Nullable: field is optional in the API
public string? CompanyName { get; init; }

// Suppress null warnings only when you are certain
var value = dict["key"]!;
```

- Do not use `!` (null-forgiving) unless genuinely certain; prefer null checks or pattern matching.
- Always annotate return types: `Task<string?>`, `List<Invoice>?`.

---

## Async / Await

```csharp
// Always propagate CancellationToken as the last parameter with a default
public async Task<InvoiceResponse> GetInvoiceByCodeAsync(
    string invoiceCode,
    CancellationToken ct = default)
{
    var request = new InvoiceRequest { InvoiceCode = invoiceCode };
    return await PostAsync<InvoiceResponse>(request, ct);
}
```

- Every async method name ends in `Async`.
- Always `await` — never `.Result` or `.Wait()` (deadlock risk).
- Pass `CancellationToken` end-to-end; never ignore it.
- Return `Task` (not `void`) for fire-and-forget only when unavoidable; prefer `Task<T>`.
- Use `ConfigureAwait(false)` in library code (SDK layer), not in ASP.NET Core controllers.

References:
- [Async/Await Best Practices](https://learn.microsoft.com/en-us/dotnet/csharp/asynchronous-programming/async-in-depth)
- [CancellationToken](https://learn.microsoft.com/en-us/dotnet/standard/threading/cancellation-in-managed-threads)

---

## JSON Serialization (Newtonsoft.Json)

All WeFact API fields use `snake_case`. Map them explicitly:

```csharp
using Newtonsoft.Json;

public class CreateInvoiceRequest : BaseRequestObject
{
    [JsonProperty("debtor_code")]
    public required string DebtorCode { get; init; }

    [JsonProperty("invoice_lines")]
    public required IReadOnlyList<InvoiceLine> InvoiceLines { get; init; }

    // Omit null fields from serialization
    [JsonProperty("company_name", NullValueHandling = NullValueHandling.Ignore)]
    public string? CompanyName { get; init; }
}
```

- Always use `[JsonProperty]` on every mapped field — never rely on convention.
- Use `NullValueHandling.Ignore` for optional fields to avoid sending null to the API.
- Use `MissingMemberHandling.Ignore` in deserialization settings (already set in `BaseClient`).

---

## Builder Pattern

```csharp
public sealed class InvoiceLineBuilder
{
    private readonly InvoiceLine _line;

    public InvoiceLineBuilder(decimal priceExcl, string description, string date)
    {
        _line = new InvoiceLine
        {
            PriceExcl   = priceExcl,
            Description = description,
            Date        = date,
        };
    }

    public InvoiceLineBuilder SetDiscount(decimal percent)
    {
        _line.Discount = percent;
        return this;   // always return this for fluent chaining
    }

    public InvoiceLine Build() => _line;
}
```

Rules:
- Constructors receive **required** parameters only.
- Each `Set*` method returns `this` (or `TBuilder` for generic builders).
- `Build()` is the terminal method; it returns the constructed object.
- Builders live in `Builder/` or `Builder/Abstract/`.

---

## Factory Pattern

```csharp
public static class InvoiceLineFactory
{
    public static InvoiceLine CreateBaseLine(decimal priceExcl, string description, string date)
        => new InvoiceLineBuilder(priceExcl, description, date).Build();

    public static InvoiceLine CreateProductLine(
        decimal priceExcl, string description, string date, string productCode)
        => new InvoiceLineBuilder(priceExcl, description, date)
            .SetProductCode(productCode)
            .Build();
}
```

Factories are `static` classes with `static` methods. They wrap builders for common scenarios.

---

## Error Handling

```csharp
// In client methods — validate before calling the API
if (request.InvoiceLines.Count == 0)
    return BaseResponseObject.CreateErrorObject<CreateInvoiceResponse>("InvoiceLines cannot be empty.");

// Check response status, not exceptions
var response = await PostAsync<InvoiceResponse>(request, ct);
if (response.Status == "error")
{
    // Errors are in response.Errors (List<string>)
}
```

- Do not swallow exceptions silently.
- Use `BaseResponseObject.CreateErrorObject<T>()` for validation failures before hitting the API.
- Never throw from builders or factories — return null or use the result pattern.

---

## Collections

```csharp
// Prefer IReadOnlyList<T> for output / DTO collections
public IReadOnlyList<InvoiceLine> InvoiceLines { get; init; } = [];

// Prefer List<T> for mutable internal collections
private readonly List<InvoiceLine> _lines = [];

// Use LINQ for transformations, not loops
var paid = invoices.Where(i => i.Status == EInvoiceStatus.Paid).ToList();
```

Reference: [Collections (Microsoft)](https://learn.microsoft.com/en-us/dotnet/standard/collections/)

---

## Formatting

- **Indentation**: 4 spaces (no tabs).
- **Braces**: Allman style (opening brace on its own line) for types and methods; single-line allowed for simple lambdas and expression-bodied members.
- **Line length**: aim for ≤ 120 characters.
- **Expression-bodied members** for trivial one-liners:
  ```csharp
  public string FullName => $"{Initials} {Surname}";
  ```
- **var**: use when the type is obvious from the right-hand side; use explicit type otherwise.
- **Object initializers** preferred over property-assignment blocks.
- **Pattern matching** preferred over `is` + cast chains.

---

## What Claude Should Do With This Skill

When **writing new C# code**:
1. Apply naming from the table above without exception.
2. Use file-scoped namespaces matching the folder path.
3. Add `CancellationToken ct = default` to every async method.
4. Annotate nullable reference types accurately.
5. Use `[JsonProperty("snake_case")]` for every WeFact-mapped field.
6. Return `this` from every builder `Set*` method.

When **reviewing existing code**:
1. Flag any naming that deviates from the conventions above.
2. Check async methods for missing `CancellationToken` propagation.
3. Check for missing nullable annotations or unsafe `!` suppressions.
4. Check `[JsonProperty]` completeness on DTOs.
5. Suggest expression-bodied members where appropriate.

When **unsure about a C# rule**, link to the relevant official Microsoft documentation page listed at the top of this file.
