namespace IIG.Web.Data.Models.Discount;

public class DiscountApplyRequest
{
    public Guid OrderId { get; set; }
    public string DiscountCode { get; set; }
}