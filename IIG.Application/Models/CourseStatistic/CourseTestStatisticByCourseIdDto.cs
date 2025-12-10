namespace IIG.Application.Models;
public class CourseTestStatisticByCourseIdDto
{
    public Guid CourseTestId { get; set; }
    public string CourseTestTitle { get; set; }
    public List<SectionStatisticDetailModel> Sections { get; set; } = new();
}

public class SectionStatisticDetailModel
{
    public Guid SectionId { get; set; }
    public string SectionName { get; set; }
    public int ExactScore { get; set; }
    public string RankingScore { get; set; }
    public int MinScore { get; set; }
    public int MaxScore { get; set; }
}
