using System.ComponentModel;

namespace IIG.Core.Common.Enums;
public enum ELiveClassDetailTestStatus
{
    [Description("Chưa diễn ra")]
    UnFinished = 1,

    [Description("Đang diễn ra")]
    InProgress,

    [Description("Đã diễn ra")]
    Finished,
}
