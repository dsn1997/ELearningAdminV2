namespace IIG.Web.Data.Models.ShoppingCart.Orders;

public class UpdateDraftShoppingCartRequest
{
    public Guid OrderId { get; set; }

    public Guid CourseId { get; set; }

    public Guid CoursePriceId { get; set; }

    public bool IsDraft { get; set; }
}