namespace IIG.Application.Models.KeyCodes;

public class KeyCodeResultDto
{
    public string KeyCode { get; set; }
    
    public int TotalCorrectAnswer { get; set; }
    
    public int TotalQuestion { get; set; }
    
    public string RankingScore { get; set; }
    
    public string ComponentsDetails { get; set; }
}