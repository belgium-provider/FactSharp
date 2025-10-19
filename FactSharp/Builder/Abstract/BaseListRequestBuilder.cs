using FactSharp.Http;

namespace FactSharp.Builder.Abstract;

public class BaseListRequestBuilder<TRequest, TBuilder> where TRequest : BaseListRequestObject, new() where TBuilder : BaseListRequestBuilder<TRequest, TBuilder>
{
    protected readonly TRequest Request = new();
    
    public TBuilder SetOffset(int offset)
    {
        Request.Offset = offset;
        return (TBuilder)this;
    }

    public TBuilder SetLimit(int limit)
    {
        Request.Limit = limit;
        return (TBuilder)this;
    }

    public TBuilder SetOrder(string order)
    {
        Request.Order = order;
        return (TBuilder)this;
    }

    public TBuilder SetSort(string sort)
    {
        Request.Sort = sort;
        return (TBuilder)this;
    }

    public TBuilder SetCreated(DateTime? created)
    {
        Request.Created = created;
        return (TBuilder)this;
    }

    public TBuilder SetModified(DateTime? modified)
    {
        Request.Modified = modified;
        return (TBuilder)this;
    }

    public virtual TRequest Build() => Request;
}