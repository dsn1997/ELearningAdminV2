namespace IIG.Application.Models.KeyCodes;

public class KeyCodeResultModel
{
    public string KeyCode { get; set; }
    
    public int TotalCorrectAnswer { get; set; }
    
    public int TotalQuestion { get; set; }
    
    public string RankingScore { get; set; }
    
    public IEnumerable<SectionScoreBasicDto> Sections { get; set; }
}

public class SectionScoreBasicDto
{
    public Guid SectionId { get; set; }
   
    public int Score { get; set; }
    public int ExactScore { get; set; }
    public string RankingScore { get; set; }
    public int MinScore { get; set; }
    public int MaxScore { get; set; }
    public int NumberOfCorrectAnswer { get; set; }
}