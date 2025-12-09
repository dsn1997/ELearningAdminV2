using System.ComponentModel;

namespace IIG.Core.Common.Enums;

public enum EDiscountType
{
    [Description("Giảm theo phần trăm (%)")]
    ByPercent = 1,

    [Description("Giảm theo số tiền")]
    ByValue = 2,
}