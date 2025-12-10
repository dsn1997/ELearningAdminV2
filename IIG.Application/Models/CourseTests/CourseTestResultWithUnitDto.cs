namespace IIG.Web.Data.Models.CourseTests;

public class CourseTestResultWithUnitDto
{
    public Guid CourseTestId { get; set; }

    public string CourseTestTitle { get; set; }

    public Guid? UnitId { get; set; }

    public string RankingScore { get; set; }

    public int TotalQuestion { get; set; }
  
    public int TotalCorrectAnswer { get; set; }
}