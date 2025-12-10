namespace IIG.Application.Models.Orders;

public class CartInsertRequest
{
    public Guid CourseId { get; set; }

    public Guid CoursePriceId { get; set; }

    public int Quantity { get; set; } = 1;
}