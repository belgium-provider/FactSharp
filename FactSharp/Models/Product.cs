namespace FactSharp.Models;

public class Product
{
    public int Identifier { get; set; }
    public required string ProductCode { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string ProductKeyPhrase { get; set; } = string.Empty;
    public string ProductDescription { get; set; } = string.Empty;
    public string NumberSuffix { get; set; } = string.Empty;
    public string Barcode { get; set; } = string.Empty;
    public decimal PriceExcl { get; set; }
    public string PricePeriod { get; set; } = string.Empty;
    public string TaxCode { get; set; } = string.Empty;
    public decimal TaxPercentage { get; set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }

    public List<ProductGroup> Groups { get; set; } = new();
    public Dictionary<string, string> Translations { get; set; } = new();
}

public class ProductGroup
{
    public int Id { get; set; }
    public string GroupName { get; set; } = string.Empty;
}
