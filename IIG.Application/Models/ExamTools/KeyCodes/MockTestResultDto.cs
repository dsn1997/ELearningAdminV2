using IIG.Core.Common.Enums;
using IIG.Core.Common.Models.SpeakingAndWriting;

namespace IIG.Web.Data.Models.ExamTools.KeyCodes;

public class MockTestResultDto
    : IWatchCount
{
    public string MockTestName { get; set; }

    public Guid MockTestId { get; set; }

    public string TotalScore { get; set; }

    public int MinScore { get; set; }

    public int MaxScore { get; set; }

    public EMockTestScoreType ScoreType { get; set; }

    public IEnumerable<MockTestSectionResultDto> Sections { get; set; }

    public string TextViewMore { get; set; }

    public string LinkUrlViewMore { get; set; }
    public string ExtractScore { get; set; }

    public ECourseScoringStatus? ScoringStatus { get; set; }
    public Guid? ScoringId { get; set; }
    public int? WatchCount { get; set; }
}