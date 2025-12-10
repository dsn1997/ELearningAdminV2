namespace IIG.Web.Data.Models.ShoppingCart.OrderDetails;

public class OrderDetailModel
{
    public Guid OrderId { get; set; }

    public Guid CourseId { get; set; }

    public Guid CoursePriceId { get; set; }

    public int Quantity { get; set; }

    public bool IsDraft { get; set; }
}