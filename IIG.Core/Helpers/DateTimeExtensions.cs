using IIG.Core.Common.ConfigureModels;
using System.Globalization;

namespace IIG.Core.Helpers;

public static class DateTimeExtensions
{
    public static string TryGetStringFromDateTime(this DateTime? dt, int hourDiff, string format)
    {
        try
        {
            return dt == null ? "" : dt.GetValueOrDefault().AddHours(hourDiff).ToString(format, CultureInfo.InvariantCulture);
        }
        catch (Exception)
        {
            return "";
        }
    }

    public static string GetStringFromDateTime(this DateTime dt, int hourDiff, string format)
    {
        try
        {
            return dt.AddHours(hourDiff).ToString(format, CultureInfo.InvariantCulture);
        }
        catch (Exception)
        {
            return "";
        }
    }
}