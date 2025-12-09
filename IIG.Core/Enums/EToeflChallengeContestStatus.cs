using System.ComponentModel;

namespace IIG.Core.Common.Enums;
public enum EToeflChallengeContestStatus
{
    [Description("Sắp diễn ra")]
    GoingTo = 0,

    [Description("Đang diễn ra")]
    InProcessing = 1,

    [Description("Đã diễn ra")]
    Finished = 2
}
