using FactSharp.Builder.Abstract;
using FactSharp.Http.Customer.Request;

namespace FactSharp.Builder;

public class CustomerListRequestBuilder : BaseListRequestBuilder<CustomerListRequest, CustomerListRequestBuilder>
{
    public CustomerListRequestBuilder()
    {
        Request.Controller = "debtor";
        Request.Action = "list";
        Request.Sort = "DebtorCode";
    }

    public CustomerListRequestBuilder SetGroup(int group)
    {
        Request.Group = group;
        return this;
    }

    public override CustomerListRequest Build() => Request;
}