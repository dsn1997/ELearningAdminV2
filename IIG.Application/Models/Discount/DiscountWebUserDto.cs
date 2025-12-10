namespace IIG.Web.Data.Models.Discount;

public class DiscountWebUserDto
{
    public Guid WebUserId { get; set; }
    public string DiscountCode { get; set; }
    public int? NoUse { get; set; }
    public DateTime DateUse { get; set; }
}