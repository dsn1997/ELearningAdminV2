using System.ComponentModel;

namespace IIG.Core.Common.Enums;

public enum  EDiscountStatus
{
    [Description("Chưa kích hoạt")]
    NotActive = 1,

    [Description("Đang hoạt động")]
    Active = 2,

    [Description("Dừng hoạt động")]
    StopWorking = 3
}