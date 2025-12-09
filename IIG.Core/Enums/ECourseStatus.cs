using System.ComponentModel;

namespace IIG.Core.Common.Enums;
public enum ECourseStatus
{
    [Description("Đang hoạt động")]
    Active = 1,

    [Description("Chưa kích hoạt")]
    InActive = 2,

    [Description("Dừng hoạt động")]
    StopWorking = 3
}
