using FactSharp.Builder;
using FactSharp.Models;

namespace FactSharp.Factory;

public static class InvoiceLineFactory
{
    /// <summary>
    /// Create a base new invoice line with product ref
    /// </summary>
    /// <param name="description"></param>
    /// <param name="priceExcl"></param>
    /// <param name="date"></param>
    /// <param name="productCode"></param>
    /// <returns></returns>
    public static InvoiceLine CreateProductLine(decimal priceExcl, string description, DateTime date, string productCode)
    {
        return new InvoiceLineBuilder(priceExcl, description, date)
            .SetProductCode(productCode)
            .Build();
    }
    
    /// <summary>
    /// Creating a base invoice line object
    /// </summary>
    /// <param name="priceExcl"></param>
    /// <param name="description"></param>
    /// <param name="date"></param>
    /// <returns></returns>
    public static InvoiceLine CreateBaseLine(decimal priceExcl, string description, DateTime date) => new InvoiceLineBuilder(priceExcl, description, date).Build();
}