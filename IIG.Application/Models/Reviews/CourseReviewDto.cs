namespace IIG.Application.Models;

public class CourseReviewDto
{
    public int NumberOfRating { get; set; }
    public decimal AverageRating { get; set; }
    public IEnumerable<ReviewDto> Reviews { get; set; }
}

public class CourseReviewSumaryDto
{
    public Guid CourseId { get; set; }
    public int TotalReview { get; set; }
    public int TotalCourseTest { get; set; }
    public decimal TotalRate { get; set; }
    public bool? IsFreeLearningUnit1Lesson1 { get; set; }
    public float AvgRate
    {
        get
        {
            if (TotalReview == 0) return 0;

            return (float)(TotalRate / TotalReview);
        }
    }
}
public class ReviewDto
{
    public Guid Id { get; set; }
    public Guid? WebUserId { get; set; }
    public string FullName { get; set; }
    public string AvatarUrl { get; set; }
    public decimal Rate { get; set; }
    public string Content { get; set; }
}
