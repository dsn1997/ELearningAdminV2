using IIG.Core.Common.Enums;
using IIG.Core.Common.Models.SpeakingAndWriting;
using IIG.Application.Models.KeyCodes;

namespace IIG.Application.Models;
public class LiveClassTestViewResultDto
    : IWatchCount
{
    public string TestName { get; set; }
    public Guid MockTestId { get; set; }
    public string MockTestName { get; set; }
    public string TotalScore { get; set; }
    public int MinScore { get; set; }
    public int MaxScore { get; set; }
    public EMockTestScoreType ScoreType { get; set; }
    public IEnumerable<MockTestSectionResultDto> Sections { get; set; }
    public string TextViewMore { get; set; }
    public string LinkUrlViewMore { get; set; }
    public string LiveClassDetailName { get; set; }
    public ECourseScoringStatus? ScoringStatus { get; set; }
    public Guid? ScoringId { get; set; }
    public int? WatchCount { get; set; }
    public EMockTestSectionType MockTestSectionType { get; set; }
}
