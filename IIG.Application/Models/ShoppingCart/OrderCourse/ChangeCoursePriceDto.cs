namespace IIG.Web.Data.Models.ShoppingCart.OrderCourse;
public class ChangeCoursePriceDto
{
    public Guid OrderId { get; set; }
    public Guid CourseId { get; set; }
    public Guid CoursePriceIdOld { get; set; }
    public Guid CoursePriceIdNew { get; set; }
}