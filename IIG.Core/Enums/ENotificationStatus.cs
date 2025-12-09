using System.ComponentModel;

namespace IIG.Core.Common.Enums;

public enum ENotificationStatus
{
    [Description("Chưa gửi")]
    Unsent = 1,

    [Description("Đã gửi")]
    Sent,

    [Description("Lỗi")]
    Error
}