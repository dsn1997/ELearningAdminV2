using System.ComponentModel;

namespace IIG.Core.Common.Enums;

public enum EUnitWithLearningPathStatus
{
    [Description("Đã hoàn thành")]
    Done = 1,

    [Description("Chưa đạt yêu cầu")]
    Unqualified,

    [Description("Học tiếp")]
    LearnContinue,

    [Description("Chưa học")]
    HaveNotStudiedYet
}