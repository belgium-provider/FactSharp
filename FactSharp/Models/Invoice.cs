namespace FactSharp.Models;

public class Invoice
{
    public int Identifier { get; set; }
    public required string InvoiceCode { get; set; }
    public int Debtor { get; set; }
    public string DebtorCode { get; set; } = string.Empty;
    public int Status { get; set; }
    public string? SubStatus { get; set; }
    public string? PaymentPausedEndDate { get; set; }
    public string? PaymentPausedReason { get; set; }
    public DateTime Date { get; set; }
    public int Term { get; set; }
    public DateTime PayBefore { get; set; }
    public string? PaymentUrl { get; set; }
    public decimal AmountExcl { get; set; }
    public decimal AmountTax { get; set; }
    public decimal AmountIncl { get; set; }
    public decimal AmountPaid { get; set; }
    public decimal AmountOutstanding { get; set; }
    public decimal Discount { get; set; }
    public string VatCalcMethod { get; set; } = string.Empty;
    public string IgnoreDiscount { get; set; } = string.Empty;
    public string? ReferenceNumber { get; set; }
    public string CompanyName { get; set; } = string.Empty;
    public string Initials { get; set; } = string.Empty;
    public string SurName { get; set; } = string.Empty;
    public string Sex { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string EmailAddress { get; set; } = string.Empty;
    public int ExtraClientContactId { get; set; }
    public int InvoiceMethod { get; set; }
    public DateTime? SentDate { get; set; }
    public int Sent { get; set; }
    public int Reminders { get; set; }
    public DateTime? ReminderDate { get; set; }
    public int Summations { get; set; }
    public DateTime? SummationDate { get; set; }
    public string Authorisation { get; set; } = string.Empty;
    public string PaymentMethod { get; set; } = string.Empty;
    public DateTime? PayDate { get; set; }
    public string? TransactionId { get; set; } = string.Empty;
    public string LanguageCode { get; set; } = string.Empty;
    public string Currency { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Comment { get; set; }
    
    //invoice lines
    public List<InvoiceLine> InvoiceLines { get; set; } = [];
    
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
    public string VatShift { get; set; } = string.Empty;
    public List<Models.Translation> Translations { get; set; } = [];
    public List<Models.Attachment> Attachments { get; set; } = [];
}