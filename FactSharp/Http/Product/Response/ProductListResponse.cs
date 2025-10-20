using System.Text.Json.Serialization;

namespace FactSharp.Http.Product.Response;

public class ProductListResponse : BaseListObjectResponse
{
    [JsonPropertyName("products")]
    List<ProductListHttpObject> Products { get; set; } = [];
}

public class ProductListHttpObject
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
    public DateTime Modified { get; set; }
}