using System.ComponentModel.DataAnnotations;

namespace FactSharp.Http.Product.Request;

public class ProductRequest : BaseRequestObject
{
    
}

public class ProductByIdRequest : ProductRequest
{
    public int Identifier { get; set; }
}

public class ProductByCodeRequest : ProductRequest
{
    [MinLength(3)]
    public required string ProductCode { get; set; }
}