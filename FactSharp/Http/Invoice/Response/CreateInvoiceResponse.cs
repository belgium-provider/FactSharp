namespace FactSharp.Http.Invoice.Response;

public class CreateInvoiceResponse : BaseResponseObject
{
    public required Models.Invoice Invoice { get; set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
    public string VatShift { get; set; } = string.Empty;
    public List<Models.InvoiceTranslation> Translations { get; set; } = [];
    public List<Models.Attachment> Attachments { get; set; } = [];
}