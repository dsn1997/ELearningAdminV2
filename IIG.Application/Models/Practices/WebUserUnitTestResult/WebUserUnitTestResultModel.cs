using IIG.Core.Common.Enums;
using IIG.Core.Common.Models.SpeakingAndWriting;

namespace IIG.Application.Models.WebUserUnitTestResult;
public class WebUserUnitTestResultModel
    : IWatchCount
{
    public int ResultScore { get; set; }

    public int TotalScore { get; set; }

    public DateTime SubmittedDate { get; set; }

    public ECourseScoringStatus? ScoringStatus { get; set; }
    public Guid? ScoringId { get; set; }
    public int? WatchCount { get; set; }
}
