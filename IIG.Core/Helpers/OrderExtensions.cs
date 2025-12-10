using IIG.Core.Common.Enums;

namespace IIG.Core.Helpers;

public static class OrderExtensions
{
    public static EOrderPaymentMethod TryGetOrderPaymentMethod(this string paymentMethodStr)
    {
        try
        {
            return paymentMethodStr.GetValueFromDescription<EOrderPaymentMethod>();
        }
        catch (Exception)
        {
            return EOrderPaymentMethod.Cash;
        }
    }
}