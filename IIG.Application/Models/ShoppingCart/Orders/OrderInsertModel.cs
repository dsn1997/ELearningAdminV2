using IIG.Core.Common.Enums;

namespace IIG.Application.Models.Orders;

public class OrderInsertModel
{
    public Guid Id { get; set; }

    public Guid WebUserId { get; set; }

    public string WebUserIdentifier { get; set; }
  
    public string WebUserFullName { get; set; }
 
    public string WebUserEmail { get; set; }
 
    public string WebUserPhoneNumber { get; set; }


    public DateTime OrderDate { get; set; }

    public string OrderCode { get; set; }

    public int? Shipping { get; set; }
   
    public EOrderPaymentMethod? PaymentMethod { get; set; }

    public decimal TotalPayment { get; set; }

    public decimal? TotalDiscount { get; set; }

    public decimal PriceShipping { get; set; }

    public int TotalProducts { get; set; }

    public EOrderStatus Status { get; set; }

    public long? ReceiptCode { get; set; }

    public decimal TotalPrice { get; set; }
  
    public string ApplyDiscountCode { get; set; }
   
    public EDiscountType? ApplyDiscountMethod { get; set; }
  
    public decimal? ApplyDiscountPercent { get; set; }
   
    public decimal? ApplyDiscountPrice { get; set; }
    
    public bool IsFreeOrder { get; set; }
}