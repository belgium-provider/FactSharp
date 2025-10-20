namespace FactSharp.Http.Product.Response;

public class ProductResponse : BaseResponseObject
{
    public required Models.Product Product { get; set; }
}