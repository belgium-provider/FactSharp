namespace FactSharp.Http.Invoice.Response;

public class InvoiceListResponse : BaseListObjectResponse
{
    public List<InvoiceListHttpObject> Invoices { get; set; } = [];
}

public class InvoiceListHttpObject
{
    public int Identifier { get; set; }
    public required string InvoiceCode { get; set; }
    public int Debtor { get; set; }
    public string DebtorCode { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public string Initials { get; set; } = string.Empty;
    public string SurName { get; set; } = string.Empty;
    public decimal AmountExcl { get; set; }
    public decimal AmountTax { get; set; }
    public decimal AmountIncl { get; set; }
    public decimal AmountPaid { get; set; }
    public decimal AmountOutstanding { get; set; }
    public string Currency { get; set; } = Types.Currency.Eur;
    public DateTime Date { get; set; } = DateTime.Today;
    public int Term { get; set; }
    
    //other
}