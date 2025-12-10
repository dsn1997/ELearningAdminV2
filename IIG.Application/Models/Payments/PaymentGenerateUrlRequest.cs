using IIG.Core.Common.Enums;

namespace IIG.Web.Data.Models.Payments
{
    public class PaymentGenerateUrlRequest
    {
        public string OrderCode { get; set; }
        public EOrderPaymentMethod PaymentMethod { get; set; }
    }
}
