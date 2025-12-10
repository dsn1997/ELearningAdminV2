namespace IIG.Application.Models.Orders;

public class DeleteShoppingCartRequest
{
    public Guid OrderId { get; set; }

    public Guid CourseId { get; set; }

    public Guid CoursePriceId { get; set; }
}