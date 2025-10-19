namespace FactSharp.Types;

public class PaymentMethod
{
    public const string BankTransfer = "wire";
    public const string Cash = "cash";
    public const string PinPayment = "card";
    public const string DirectDebit = "auth";
    public const string Accounting = "accounting";
    public const string Various = "various";
    public const string Paypal = "paypal";
    public const string Ideal = "ideal";
    public const string QrCode = "qrcode";
    public const string Other = "other";
}