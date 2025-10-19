using System.ComponentModel.DataAnnotations;

namespace FactSharp.Http.Customer.Request;

public class CustomerRequest : BaseRequestObject
{
    
}

public class CustomerRequestById : CustomerRequest
{
    public int Identifier { get; set; }
}

public class CustomerRequestByCode : CustomerRequest
{
    [MinLength(6)]
    public required string DebtorCode { get; set; }
}