namespace IIG.Web.Data.Models.Practices.LiveClass;
public class MyLiveClassModel
{
    public Guid Id { get; set; }
    public Guid CourseId { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public Guid CoursePriceId { get; set; }
    public string CoursePriceName { get; set; }
    public Guid? LiveClassDetailId { get; set; }
    public int? WatchCount { get; set; }
}
