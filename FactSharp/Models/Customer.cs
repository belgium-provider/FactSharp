using System.Text.Json.Serialization;

namespace FactSharp.Models;

public class Customer
{
    public int Identifier { get; set; }
    public required string DebtorCode  { get; set; }
    public string CompanyName { get; set; } = string.Empty;
    public string CompanyNumber { get; set; } = string.Empty;
    public string TaxNumber { get; set; } = string.Empty;
    public string Sex { get; set; } = string.Empty;
    public string Initials { get; set; } = string.Empty;
    public string SurName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string EmailAddress { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string MobileNumber { get; set; } = string.Empty;
    public string FaxNumber { get; set; } = string.Empty;
    public string Comment { get; set; } = string.Empty;
    public int InvoiceMethod { get; set; } = 0;
    public string DirectDebitApplyTo { get; set; } = string.Empty;
    public string InvoiceAuthorisation { get; set; } = string.Empty;
    public DateTime? MandateDate { get; set; } = null;
    public string MandateID { get; set; } = string.Empty;
    public string AccountNumber { get; set; } = string.Empty;
    public string AccountIban { get; set; } = string.Empty;
    public string AccountBIC { get; set; } = string.Empty;
    public string AccountName { get; set; } = string.Empty;
    public string AccountBank { get; set; } = string.Empty;
    public string AccountCity { get; set; } = string.Empty;
    public string Mailing { get; set; } = string.Empty;
    public int InvoiceTerm { get; set; } = -1;
    public int PeriodicInvoiceDays { get; set; } = -1;
    public int PaymentMail { get; set; } = -1;
    public string LanguageCode { get; set; } = string.Empty;
    public string Currency { get; set; } = string.Empty;
    public string CustomTaxCode { get; set; } = string.Empty;
    public string ReminderEmailAddress { get; set; } = string.Empty;
    public DateTime? Created { get; set; } = null;
    public DateTime? Modified { get; set; } = null;
    public int DefaultBillingContactId { get; set; } = 0;
    public int DefaultQuoteContactId { get; set; } = 0;
    public List<object> ExtraClientContacts { get; set; } = new List<object>();
    public string InvoiceDataForPriceQuote { get; set; } = string.Empty;
    public string InvoiceCompanyName { get; set; } = string.Empty;
    public string InvoiceSex { get; set; } = string.Empty;
    public string InvoiceInitials { get; set; } = string.Empty;
    public string InvoiceSurName { get; set; } = string.Empty;
    public string InvoiceAddress { get; set; } = string.Empty;
    public string InvoiceZipCode { get; set; } = string.Empty;
    public string InvoiceCity { get; set; } = string.Empty;
    public string InvoiceCountry { get; set; } = string.Empty;
    public string InvoiceEmailAddress { get; set; } = string.Empty;
    //public List<Translation> Translations { get; set; } = [];
    //public List<string> Groups { get; set; } = [];
    
    [JsonPropertyName("CustomFields")]
    public CustomFields? CustomFields { get; set; } = null;
}