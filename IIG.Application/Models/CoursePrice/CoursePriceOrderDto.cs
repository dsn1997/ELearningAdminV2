namespace IIG.Web.Data.Models.CoursePrice;

public class CoursePriceOrderDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int NumberOfMonth { get; set; }
    public decimal? SalePrice { get; set; }

    public DateTime? SaleValidFrom { get; set; }
    public DateTime? SaleValidTo { get; set; }
}