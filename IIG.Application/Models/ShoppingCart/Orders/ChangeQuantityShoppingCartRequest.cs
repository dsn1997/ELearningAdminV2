namespace IIG.Application.Models.Orders;

public class ChangeQuantityShoppingCartRequest
{
    public Guid OrderId { get; set; }

    public Guid CourseId { get; set; }

    public Guid CoursePriceId { get; set; }

    public int Quantity { get; set; }
}