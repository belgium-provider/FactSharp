using FactSharp.Builder.Abstract;
using FactSharp.Http.Product.Request;

namespace FactSharp.Builder;

public class ProductListRequestBuilder : BaseListRequestBuilder<ProductListRequest, ProductListRequestBuilder>
{
    public ProductListRequestBuilder()
    {
        Request.Controller = "product";
        Request.Action = "list";
        Request.Sort = "ProductCode";
    }

    public ProductListRequestBuilder WithGroup(int group)
    {
        Request.Group = group;
        return this;
    }
    
    public override ProductListRequest Build() => Request;
}