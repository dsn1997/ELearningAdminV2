using System.ComponentModel;

namespace IIG.Core.Common.Enums;
public enum EMockTestVersionStatus
{
    [Description("Đang publish")]
    Publishing = 1,

    [Description("Chưa publish")]
    HasDraft = 2,

    [Description("Không có")]
    Latest = 3
}
