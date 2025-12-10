namespace IIG.Application.Models;

public class DiscountApplyRequest
{
    public Guid OrderId { get; set; }
    public string DiscountCode { get; set; }
}