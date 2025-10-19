namespace FactSharp.Types;

public enum EInvoiceStatus : int
{
    Concept = 0,
    Sent = 2,
    PartlyPaid = 3,
    Paid = 4,
    Credit = 8,
    Expired = 9
}