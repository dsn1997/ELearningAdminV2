using System.ComponentModel;

namespace IIG.Core.Common.Enums;

public enum EMockTestKeyCodeStatus
{
    [Description("Chưa thi")]
    NotUsed = 1,
    
    [Description("Đang thi")]
    Using  = 2,

    [Description("Đã thi")]
    Used = 3,

    [Description("Hết hạn")]
    Expired = 4
}