using IIG.Core.Common.Enums;

namespace IIG.Application.Models;
public class LiveClassTestResultModel
{
    public string TestName { get; set; }
    public Guid MocktestId { get; set; }
    public string MocktestName { get; set; }
    public int TotalQuestion { get; set; }
    public int TotalCorrectAnswer { get; set; }
    public string RankingScore { get; set; }
    public DateTime SubmittedDate { get; set; }
    public IEnumerable<LiveClassTestSectionScoreDto> Sections { get; set; }
    public string LinkUrlViewMore { get; set; }
    public string TextViewMore { get; set; }
    public Guid LiveClassDetailId { get; set; }
    public string LiveClassDetailName { get; set; }
}

public class LiveClassTestSectionScoreDto
{
    public Guid SectionId { get; set; }
    public EMockTestSectionType SectionType { get; set; }
    public int Score { get; set; }
    public int ExactScore { get; set; }
    public string RankingScore { get; set; }
    public int MinScore { get; set; }
    public int MaxScore { get; set; }
}
