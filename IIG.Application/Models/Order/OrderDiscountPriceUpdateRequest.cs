namespace IIG.Web.Data.Models.Order;

public class OrderDiscountPriceUpdateRequest
{
    public decimal TotalDiscount { get; set; }
    public decimal TotalPayment { get; set; }
}