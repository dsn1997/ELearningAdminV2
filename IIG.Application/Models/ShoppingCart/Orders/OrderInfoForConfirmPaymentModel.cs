using IIG.Core.Common.Enums;

namespace IIG.Application.Models.Orders;
public class OrderInfoForConfirmPaymentModel
{
    public Guid Id { get; set; }
    public string OrderCode { get; set; }
    public decimal TotalPayment { get; set; }
    public EOrderStatus Status { get; set; }
}
