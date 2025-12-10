namespace IIG.Application.Models;
public class CourseListInOrderDto
{
    public Guid OrderId { get; set; }
    public Guid CourseId { get; set; }
    public Guid CoursePriceId { get; set; }
    public Guid ExamToolCategoryId { get; set; }
    public int Quantity { get; set; }
    public decimal? TotalPrice { get; set; }
    public decimal? TotalSalePrice { get; set; }
    public string Name { get; set; }
    public string CoursePriceName { get; set; }
    public int ProductType { get; set; }
    public int NumSent { get; set; }
    public bool IsMocktestType { get; set; }
    public Guid? ClassTypeId { get; set; }
    public string CourseCode { get; set; }
}