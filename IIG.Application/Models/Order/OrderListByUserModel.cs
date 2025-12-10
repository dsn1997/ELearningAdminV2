using IIG.Core.Common.Enums;
using IIG.Application.Models;

namespace IIG.Application.Models;

public class OrderListByUserModel
{
    public Guid Id { get; set; }
    public string OrderCode { get; set; }
    public int ReceiptCode { get; set; }
    public string WebUserFullName { get; set; }
    public string WebUserPhoneNumber { get; set; }
    public decimal TotalPayment { get; set; }
    public decimal TotalDiscount { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime OrderDate { get; set; }
    public EOrderPaymentMethod PaymentMethod { get; set; }
    public EOrderStatus Status { get; set; }
    public string ApplyDiscountCode { get; set; }
    public decimal PriceShipping { get; set; }
    public int TotalProducts { get; set; }
    public string TransactionCode { get; set; }
    public IEnumerable<CourseListInOrderDto> Courses { get; set; }
}