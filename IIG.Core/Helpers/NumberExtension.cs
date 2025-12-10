namespace IIG.Core.Helpers;

public static class NumberExtension
{
    public static string TryGetStringFromDecimal(this decimal? inputValue)
    {
        try
        {
            if (!inputValue.HasValue)
            {
                return "";
            }
            return inputValue.GetValueOrDefault().ToString("0.####");
        }
        catch (Exception)
        {
            return "";
        }
    }

    public static string GetStringFromDecimal(this decimal inputValue)
    {
        try
        {
            return inputValue.ToString("0.####");
        }
        catch (Exception)
        {
            return "";
        }
    }

    public static string FormatDecimalToVietNamMoney(this decimal input)
    {
        try
        {
            return string.Format("{0:#,##0}", input);
        }
        catch (Exception)
        {
            return "";
        }
    }
}