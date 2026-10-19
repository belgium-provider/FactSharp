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

    /// <summary>
    /// Sets the search filter. WeFact requires "searchfor" whenever "searchat" is provided,
    /// so both must be set together.
    /// </summary>
    /// <param name="searchAt">Field(s) to search in, e.g. "EmailAddress", "CompanyName", "DebtorCode", "SurName".</param>
    /// <param name="searchFor">Value to search for.</param>
    public CustomerListRequestBuilder SetSearch(string searchAt, string searchFor)
    {
        if (string.IsNullOrWhiteSpace(searchAt))
            throw new ArgumentException("searchAt is required.", nameof(searchAt));
        if (string.IsNullOrWhiteSpace(searchFor))
            throw new ArgumentException("searchFor is required when searchAt is set.", nameof(searchFor));

        Request.SearchAt = searchAt;
        Request.SearchFor = searchFor;
        return this;
    }

    public override CustomerListRequest Build() => Request;
}