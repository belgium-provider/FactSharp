namespace FactSharp.Models;

public class InvoiceLine
{
    /// <summary>
    /// Private fields immutable
    /// </summary>
    public required decimal PriceExcl { get; set; }
    public required string Description { get; set; }
    public required DateTime Date { get; set; }
    
    /// <summary>
    /// Optional fields
    /// </summary>
    public int Identifier { get; set; }
    public int Number { get; set; }
    public string? NumberSuffix { get; set; }
    public string ProductCode { get; set; } = string.Empty;
    public decimal DiscountPercentage { get; set; }
    public string DiscountPercentageType { get; set; } = string.Empty;
    public string TaxCode { get; set; } = string.Empty;
    public decimal TaxPercentage { get; set; }
    public int PeriodicId { get; set; }
    public int Periods { get; set; }
    public string? Periodic { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? StartPeriod { get; set; }
    public string? EndPeriod { get; set; }
}