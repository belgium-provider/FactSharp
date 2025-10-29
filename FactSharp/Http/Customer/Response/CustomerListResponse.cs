using Newtonsoft.Json;

namespace FactSharp.Http.Customer.Response;

public class CustomerListResponse : BaseListObjectResponse
{
    [JsonProperty("debtors")]
    public List<CustomerListHttpObject> Debtors { get; set; } = [];
}

public class CustomerListHttpObject
{
    public int Identifier { get; set; }
    public required string DebtorCode { get; set; }
    public string CompanyName { get; set; } = string.Empty;
    public string Sex  { get; set; } = string.Empty;
    public string Initials { get; set; } = string.Empty;
    public string SurName  { get; set; } = string.Empty;
    public string EmailAddress { get; set; } = string.Empty;
    public DateTime Modified { get; set; }
}