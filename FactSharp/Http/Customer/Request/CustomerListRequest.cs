namespace FactSharp.Http.Customer.Request;

public class CustomerListRequest() : BaseListRequestObject("debtor", "list", "DebtorCode")
{
    public int Group { get; set; }
    
    //other ?
}