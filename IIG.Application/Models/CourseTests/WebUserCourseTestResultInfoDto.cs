namespace IIG.Application.Models;

public class WebUserCourseTestResultInfoDto
{
    public Guid CourseTestId { get; set; }

    public string CourseTestTitle { get; set; }

    public Guid WebUserId { get; set; }

    public int TotalQuestion { get; set; }

    public int TotalCorrectAnswer { get; set; }

    public string RankingScore { get; set; }

    public DateTime? SubmittedDate { get; set; }

    public string ComponentsDetails { get; set; }
}