using FactSharp.Builder;
using FactSharp.Http.Invoice.Request;
using FactSharp.Models;
using FactSharp.Types;

namespace FactSharp.Factory;

public static class CreateInvoiceFactory
{
    /// <summary>
    /// Simpliest invoice creation.
    /// </summary>
    /// <param name="debtorCode"></param>
    /// <param name="status"></param>
    /// <param name="invoiceLines"></param>
    /// <returns></returns>
    public static CreateInvoiceRequest CreateBaseInvoice(string debtorCode, EInvoiceStatus status, List<InvoiceLine>? invoiceLines = null)
    {
        return new CreateInvoiceBuilder(debtorCode)
            .SetStatus(status)
            .AddLines(invoiceLines ?? [])
            .Build();
    }
}