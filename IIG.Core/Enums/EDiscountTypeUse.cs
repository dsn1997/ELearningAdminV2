using System.ComponentModel;

namespace IIG.Core.Common.Enums;

public enum EDiscountTypeUse
{
    [Description("Không giới hạn")]
    NoLimited = 1,

    [Description("Trên mã giảm giá")]
    OnCode = 2,

    [Description("Trên ngày")]
    OnDay = 3,
}