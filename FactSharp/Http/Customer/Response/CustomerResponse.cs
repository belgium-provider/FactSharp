namespace FactSharp.Http.Customer.Response;

public class CustomerResponse : BaseResponseObject
{
    public required Models.Customer Debtor { get; set; }
}