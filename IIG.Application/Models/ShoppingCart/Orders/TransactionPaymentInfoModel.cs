using IIG.Core.Common.Enums;

namespace IIG.Application.Models.Orders;
public class TransactionPaymentInfoModel
{
    public Guid Id { get; set; }
    public string OrderCode { get; set; }
    public DateTime PayDate { get; set; }
    public EOrderPaymentMethod PaymentMethod { get; set; }
}
