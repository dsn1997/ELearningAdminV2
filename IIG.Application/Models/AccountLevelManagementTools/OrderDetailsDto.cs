public class OrderDetailsDto
{
    public Guid OrderId { get; set; }
    public Guid CoursePriceId { get; set; }
    public int Quantity { get; set; }
    public Guid CourseId { get; set; }
    public Guid? ExamToolCategoryId { get; set; }
    public string FullTextSearchOrder { get; set; }
}