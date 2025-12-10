using IIG.Core.Common.Enums;

namespace IIG.Application.Models;
public class LiveClassTestResultDto
{
    public string TestName { get; set; }
    public Guid MocktestId { get; set; }
    public string MocktestName { get; set; }
    public int TotalQuestion { get; set; }
    public int TotalCorrectAnswer { get; set; }
    public string RankingScore { get; set; }
    public DateTime SubmittedDate { get; set; }
    public string ComponentsDetails { get; set; }
    public string MocktestMenu { get; set; }
    public EMockTestScoreType MocktestScoreType { get; set; }
    public string LinkUrlViewMore { get; set; }
    public string TextViewMore { get; set; }
    public Guid LiveClassDetailId { get; set; }
    public string LiveClassDetailName { get; set; }
}
