using System.ComponentModel;

namespace IIG.Core.Common.Enums;
public enum EMockTestScoreType
{
    [Description("Điểm chính xác")]
    CorrectScore = 1,

    [Description("Dải điểm")]
    ScoreRange = 2,

    [Description("Dải điểm ITP")]
    ITPScoreRange = 3
}
