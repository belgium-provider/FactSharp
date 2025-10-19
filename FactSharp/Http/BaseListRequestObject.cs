namespace FactSharp.Http;

public abstract class BaseListRequestObject(string controller, string action, string sort) : BaseRequestObject
{
    public override string Controller { get; set; } = controller;
    public override string Action { get; set; } = action;


    public int Offset { get; set; } = 0;
    public int Limit { get; set; } = 1000; //standard 
    public string Order { get; set; } = Types.Order.Asc; //refer to Order model.
    public string Sort { get; set; } = sort;

    public DateTime? Created { get; set;  } = null;
    public DateTime? Modified { get; set; } = null;
}