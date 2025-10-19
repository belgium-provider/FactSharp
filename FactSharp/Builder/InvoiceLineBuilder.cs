using FactSharp.Models;

namespace FactSharp.Builder;

public class InvoiceLineBuilder(decimal priceExcl, string description, DateTime date)
{
    private readonly InvoiceLine _line = new()
    {
        PriceExcl = priceExcl,
        Description = description,
        Date = date
    };

    public InvoiceLineBuilder SetNumberSuffix(string? suffix) { _line.NumberSuffix = suffix; return this; }
    public InvoiceLineBuilder SetDescription(string desc) { _line.Description = desc; return this; }
    public InvoiceLineBuilder SetDiscountPercentage(decimal discount) { _line.DiscountPercentage = discount; return this; }
    public InvoiceLineBuilder SetDiscountPercentageType(string type) { _line.DiscountPercentageType = type; return this; }
    public InvoiceLineBuilder SetTaxCode(string taxCode) { _line.TaxCode = taxCode; return this; }
    public InvoiceLineBuilder SetTaxPercentage(decimal tax) { _line.TaxPercentage = tax; return this; }
    public InvoiceLineBuilder SetPeriodicId(int id) { _line.PeriodicId = id; return this; }
    public InvoiceLineBuilder SetPeriods(int periods) { _line.Periods = periods; return this; }
    public InvoiceLineBuilder SetPeriodic(string? periodic) { _line.Periodic = periodic; return this; }
    public InvoiceLineBuilder SetStartDate(DateTime? startDate) { _line.StartDate = startDate; return this; }
    public InvoiceLineBuilder SetEndDate(DateTime? endDate) { _line.EndDate = endDate; return this; }
    public InvoiceLineBuilder SetStartPeriod(string? start) { _line.StartPeriod = start; return this; }
    public InvoiceLineBuilder SetEndPeriod(string? end) { _line.EndPeriod = end; return this; }

    public InvoiceLineBuilder SetProductCode(string? productCode) { _line.ProductCode = productCode ?? string.Empty; return this; }

    public InvoiceLine Build() => _line;
}