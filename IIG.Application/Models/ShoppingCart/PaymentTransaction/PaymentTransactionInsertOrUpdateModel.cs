namespace IIG.Application.Models.PaymentTransaction;
public class PaymentTransactionInsertOrUpdateModel
{
    public Guid Id { get; set; }
    public string OrderCode { get; set; }
    public string TransactionCode { get; set; }
    public string TraceId { get; set; }
    public string BankCode { get; set; }
    public string PaymentContent { get; set; }
    public string Status { get; set; }
    public DateTime? PayDate { get; set; }
    public string CardType { get; set; }
}
