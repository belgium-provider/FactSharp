namespace FactSharp.Models;

public class Translation
{
    public required string Status { get; set; }
    public string Country { get; set; } = string.Empty;
    public string InvoiceMethod { get; set; } = string.Empty;
    public string PaymentMethod { get; set; } = string.Empty;
    public string LanguageLabel { get; set; } = string.Empty;
}