using IIG.Core.Common.Enums;

namespace IIG.Web.Data.Models.Order;

public class OrderInfoDto
{
    public Guid Id { get; set; }
    public string OrderCode { get; set; }
    public decimal PriceShipping { get; set; }
    public int ReceiptCode { get; set; }
    public decimal TotalPayment { get; set; }
    public decimal TotalPrice { get; set; }
    public string ApplyDiscountCode { get; set; }
    public EDiscountType? ApplyDiscountMethod { get; set; }
    public decimal? ApplyDiscountPercent { get; set; }
    public decimal? ApplyDiscountPrice { get; set; }
    public DateTime ApplyDiscountExpiredTime { get; set; }
}