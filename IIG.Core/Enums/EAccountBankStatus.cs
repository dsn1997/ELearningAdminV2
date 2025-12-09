using System.ComponentModel;

namespace IIG.Core.Common.Enums;

public enum EAccountBankStatus
{
    [Description("Chưa bán")]
    NotSell,

    [Description("Đã bán")]
    Sold,

    [Description("Hết hạn")]
    Expiry
}