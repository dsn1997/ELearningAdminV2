namespace IIG.Web.Data.Models.ShoppingCart.Orders;

public class DeleteShoppingCartRequest
{
    public Guid OrderId { get; set; }

    public Guid CourseId { get; set; }

    public Guid CoursePriceId { get; set; }
}