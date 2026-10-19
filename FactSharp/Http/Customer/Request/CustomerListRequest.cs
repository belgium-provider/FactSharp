using Newtonsoft.Json;

namespace FactSharp.Http.Customer.Request;

public class CustomerListRequest() : BaseListRequestObject("debtor", "list", "DebtorCode")
{
    [JsonProperty("group")]
    public int Group { get; set; }

    /// <summary>
    /// Field(s) to search in (e.g. "EmailAddress", "CompanyName", "DebtorCode", "SurName").
    /// API default when omitted: "DebtorCode|CompanyName|SurName". Must be set together with <see cref="SearchFor"/>.
    /// </summary>
    [JsonProperty("searchat")]
    public string? SearchAt { get; set; } = null;

    /// <summary>
    /// Value to search for. Required by the API whenever <see cref="SearchAt"/> is set.
    /// </summary>
    [JsonProperty("searchfor")]
    public string? SearchFor { get; set; } = null;

    //other ?
}