namespace IIG.Web.Data.Models.ShoppingCart.Orders;

public class MultipleUpdateDraftShoppingCartRequest
{
    public List<DraftShoppingCartRequest> ListDraftShoppingCartRequest { get; set; } = new();
    public Guid OrderId { get; set; }
    public bool IsDraft { get; set; }
}

public class DraftShoppingCartRequest
{
    public Guid CourseId { get; set; }

    public Guid CoursePriceId { get; set; }
}