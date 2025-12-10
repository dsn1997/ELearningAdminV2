namespace IIG.Web.Data.Models.ShoppingCart.Orders;

public class CartInsertRequest
{
    public Guid CourseId { get; set; }

    public Guid CoursePriceId { get; set; }

    public int Quantity { get; set; } = 1;
}