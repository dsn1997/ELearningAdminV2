using IIG.Core.Common.Enums;

namespace IIG.Application.Models
{
    public class PaymentGenerateUrlRequest
    {
        public string OrderCode { get; set; }
        public EOrderPaymentMethod PaymentMethod { get; set; }
    }
}
