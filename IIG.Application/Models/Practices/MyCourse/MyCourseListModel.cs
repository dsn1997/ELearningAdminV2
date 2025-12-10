namespace IIG.Web.Data.Models.Practices.MyCourse;

public class MyCourseListModel
{
    public Guid CourseId { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public bool ExpiredStatus { get; set; }
    public int NumberDaysLeft { get; set; }
    public decimal CourseCompletion { get; set; }
    public DateTime? LearningCourseExpiredUTC { get; set; }
    public Guid? CoursePriceId { get; set; }
    public string NameNonAscii { get; set; }

    public Guid? CourseScoringId { get; set; }
    public int? WatchCount { get; set; }
    public int? Type { get; set; }
    public bool? IsMocktestType { get; set; }
    // implement other fields later
    
     public int? Status { get; set; }
     public DateTime? PreserveStart { get; set; }
     public DateTime? PreserveEnd { get; set; }

}