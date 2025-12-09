using System.ComponentModel;

namespace IIG.Core.Common.Enums;

public enum EExamToolThirdPartyListAccount
{
    [Description("Đang học")]
    Studying = 1,

    [Description("Hết hạn")]
    Expired,

    [Description("Đang chờ cấp")]
    WaitingForGrant
}