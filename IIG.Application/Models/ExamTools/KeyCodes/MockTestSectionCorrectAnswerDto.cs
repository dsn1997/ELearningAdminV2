namespace IIG.Web.Data.Models.ExamTools.KeyCodes;

public class MockTestSectionCorrectAnswerDto
{
    public Guid SectionId { get; set; }
    
    public string SectionName { get; set; }
    
    public Guid? RankingScoreId { get; set; }
    
    public int NumberOfCorrectAnswer { get; set; }
    
    public int MinScore { get; set; }
    
    public int MaxScore { get; set; }
    
    public int ExactScore { get; set; }
    
    public int FromScore { get; set; }
    
    public int ToScore { get; set; }
}