using IIG.Core.Common.Enums;

namespace IIG.Web.Data.Models.ShoppingCart.Orders;
public class TransactionPaymentInfoModel
{
    public Guid Id { get; set; }
    public string OrderCode { get; set; }
    public DateTime PayDate { get; set; }
    public EOrderPaymentMethod PaymentMethod { get; set; }
}
