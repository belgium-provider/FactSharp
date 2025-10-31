using System.Text.Json.Serialization;

namespace FactSharp.Models;

public class CustomFields
{
    [JsonPropertyName("gocardlessid")]
    public string? GoCardLessId { get; set; } = null;

    [JsonPropertyName("gocardlessmandaat")]
    public string? GoCardLessMandaat { get; set; } = null;
}