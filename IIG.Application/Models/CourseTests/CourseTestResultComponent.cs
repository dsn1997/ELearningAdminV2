namespace IIG.Application.Models;
public class CourseTestResultComponent
{
    public Guid SectionId { get; set; }
    public string SectionName { get; set; }
    public int ExactScore { get; set; }
    public string RankingScore { get; set; }
    public int MinScore { get; set; }
    public int MaxScore { get; set; }
}
