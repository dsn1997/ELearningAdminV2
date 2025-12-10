using IIG.Core.Common.Enums;

namespace IIG.Web.Data.Models.Discount;

public class DiscountDto
{
    public string Name { get; set; }

    public string Code { get; set; }

    public EDiscountStatus Status { get; set; }

    public DateTime ValidFrom { get; set; }

    public DateTime ValidTo { get; set; }

    /// <summary>
    /// Phạm vi áp dụng
    /// </summary>
    public EDiscountScopeApply ScopeApply { get; set; }

    /// <summary>
    /// Loại hình giảm giá
    /// </summary>
    public EDiscountType Type { get; set; }

    /// <summary>
    /// Phần trăm giảm giá (type 1) // Số tiền giảm (type 2)
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Số tiền giảm tố đa (type 1)
    /// </summary>
    public decimal? PercentMaxPrice { get; set; }

    /// <summary>
    /// Số lượng mã giảm giá 1 người được sử dụng
    /// </summary>
    public EDiscountTypeUse TypeUse { get; set; }

    /// <summary>
    /// Số lần sử dụng theo type use
    /// </summary>
    public int PeriodNo { get; set; }

    /// <summary>
    /// Số lượng mã giảm giá
    /// </summary>
    public int LimitedUse { get; set; }
}