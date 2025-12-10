namespace IIG.Web.Data.Models.ShoppingCart.OrderCourse;
public class CourseListInCartDto
{
    public Guid OrderId { get; set; }
    public Guid CourseId { get; set; }
    public Guid CoursePriceId { get; set; }
    public int? Quantity { get; set; }
    public bool IsDraft { get; set; }
    public decimal? Price { get; set; }
    public decimal? SalePrice { get; set; }
    public string CourseName { get; set; }
    public string CourseNameType { get; set; }
}