using Amazon.Runtime;
using IIG.Core.Common.ConfigureModels;
using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace IIG.Core.Helpers;

public static partial class StringExtensions
{
    private static readonly RNGCryptoServiceProvider random = new RNGCryptoServiceProvider();
    public static string EnumGetDescription<T>(this T enumerationValue) where T : struct
    {
        var type = enumerationValue.GetType();
        if (!type.IsEnum)
        {
            throw new ArgumentException($"{nameof(enumerationValue)} must be of Enum type", nameof(enumerationValue));
        }
        var memberInfo = type.GetMember(enumerationValue.ToString());

        if (memberInfo.Length <= 0)
            return enumerationValue.ToString();

        var attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

        return attrs.Length > 0 ? ((DescriptionAttribute)attrs[0]).Description : enumerationValue.ToString();
    }

    public static T ConvertTo<T>(this string input)
    {
        try
        {
            var converter = TypeDescriptor.GetConverter(typeof(T));
            return (T)converter.ConvertFromString(input);
        }
        catch (NotSupportedException)
        {
            return default;
        }
    }

    public static Guid ToGuid(this object request)
    {
        return request != null ? new Guid(request.ToString()) : Guid.Empty;
    }

    public static Guid? NullToGuid(this object request)
    {
        return request != null ? new Guid(request.ToString()) : null;
    }

    public static int? NullToInt(this object request)
    {
        return request != null ? request.ToString().ConvertTo<int>() : null;
    }

    public static decimal ToDecimal(this double request)
    {
        return request.ToString(CultureInfo.InvariantCulture).ConvertTo<decimal>();
    }

    public static string ParseDecimalToCurrencyType(this decimal request)
    {
        return request.ToString("N");
    }

    public static bool IsValidEmail(string email)
    {
        string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)" + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
        try
        {
            if (string.IsNullOrEmpty(email))
                return false;
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);
            return regex.IsMatch(email);
        }
        catch (Exception)
        {
            return false;
        }
    }

    public static string GeneratePassword(int lowercase = 5, int uppercase = 5, int numerics = 5, int specialCharacters = 0)
    {
        string lowers = "abcdefghijklmnopqrstuvwxyz";
        string uppers = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string number = "0123456789";
        string specials = "@&^#";

        Random random = new Random();

        string generated = "!";
        for (int i = 1; i <= lowercase; i++)
            generated = generated.Insert(
                random.Next(generated.Length),
                lowers[random.Next(lowers.Length - 1)].ToString()
            );

        for (int i = 1; i <= uppercase; i++)
            generated = generated.Insert(
                random.Next(generated.Length),
                uppers[random.Next(uppers.Length - 1)].ToString()
            );

        for (int i = 1; i <= numerics; i++)
            generated = generated.Insert(
                random.Next(generated.Length),
                number[random.Next(number.Length - 1)].ToString()
            );

        for (int i = 1; i <= specialCharacters; i++)
            generated = generated.Insert(
                random.Next(generated.Length),
                specials[random.Next(specials.Length - 1)].ToString()
            );

        return generated.Replace("!", string.Empty);
    }

    public static string TranslateToFullTextSearchQuery(this string input)
    {
        // transate into full-text search query
        if (string.IsNullOrEmpty(input))
        {
            input = string.Empty;
        }

        input = input.Replace("-", " ").Trim();
        var keywords = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
        if (keywords.Length == 0)
        {
            return string.Empty; // --> return empty string to prevent System.Data.SqlClient.SqlException (0x80131904): Null or empty full-text predicate.
        }

        StringBuilder ftsQuery = new StringBuilder();
        int count = 0;
        foreach (string key in keywords)
        {
            ftsQuery.Append(string.Format("\"*{0}*\"", key));
            if (count < keywords.Length - 1)
            {
                ftsQuery.Append(" and ");
            }

            count++;
        }

        return ftsQuery.ToString();
    }

    public static bool IsValidPhone(string Phone)
    {
        try
        {
            if (string.IsNullOrEmpty(Phone))
                return false;
            var r = new Regex(@"^[0-9]{9,15}$");
            return r.IsMatch(Phone);
        }
        catch (Exception)
        {
            return false;
        }
    }

    public static bool IsValidPassword(string password)
    {
        try
        {
            if (string.IsNullOrEmpty(password))
                return false;
            var r = new Regex(@"^(?=(.*[a-zA-Z]){1,})(?=(.*[0-9]){1,})(?=(.*[!""#$&'()+,-./:;<=>?@[\]^_`{|*}~]){1,}).{8,15}$");
            return r.IsMatch(password);
        }
        catch (Exception)
        {
            return false;
        }
    }

    public static string GenerateOtpCode(int length = Constants.Common.VerifyCodeLength)
    {
        //string alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        //string smallAlphabets = "abcdefghijklmnopqrstuvwxyz";
        string numbers = "1234567890";

        //string characters = numbers + alphabets + smallAlphabets + numbers;
        string characters = numbers;
        string otp = string.Empty;
        for (int i = 0; i < length; i++)
        {
            string character;
            do
            {
                int index = new Random().Next(0, characters.Length);
                character = characters.ToCharArray()[index].ToString();
            } while (otp.IndexOf(character) != -1);
            otp += character;
        }

        return otp;
    }

    private static readonly string[] VietNamChar = new string[]
        {
            "aAeEoOuUiIdDyY",
            "áàạảãâấầậẩẫăắằặẳẵ",
            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
            "éèẹẻẽêếềệểễ",
            "ÉÈẸẺẼÊẾỀỆỂỄ",
            "óòọỏõôốồộổỗơớờợởỡ",
            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
            "úùụủũưứừựửữ",
            "ÚÙỤỦŨƯỨỪỰỬỮ",
            "íìịỉĩ",
            "ÍÌỊỈĨ",
            "đ",
            "Đ",
            "ýỳỵỷỹ",
            "ÝỲỴỶỸ"
        };

    public static string ConvertNonASCII(this string str)
    {
        str = str.Trim();
        for (int i = 1; i < VietNamChar.Length; i++)
        {
            for (int j = 0; j < VietNamChar[i].Length; j++)
            {
                str = str.Replace(VietNamChar[i][j], VietNamChar[0][i - 1]);
            }
        }

        str = str.Replace("\u202f", "-");
        str = str.Replace(" ", "-");
        str = str.Replace("--", "-");
        str = str.Replace("?", "");
        str = str.Replace("&", "");
        str = str.Replace(",", "");
        str = str.Replace(":", "");
        str = str.Replace("!", "");
        str = str.Replace("'", "");
        str = str.Replace("\"", "");
        str = str.Replace("%", "");
        str = str.Replace("#", "");
        str = str.Replace("$", "");
        str = str.Replace("*", "");
        str = str.Replace("`", "");
        str = str.Replace("~", "");
        str = str.Replace("@", "");
        str = str.Replace("^", "");
        str = str.Replace(".", "");
        str = str.Replace("/", "");
        str = str.Replace(">", "");
        str = str.Replace("<", "");
        str = str.Replace("[", "");
        str = str.Replace("]", "");
        str = str.Replace(";", "");
        str = str.Replace("+", "");
        return str.ToLower();
    }

    public static string ConvertNonVietNamChar(this string str)
    {
        str = str.Trim();
        for (int i = 1; i < VietNamChar.Length; i++)
        {
            for (int j = 0; j < VietNamChar[i].Length; j++)
            {
                str = str.Replace(VietNamChar[i][j], VietNamChar[0][i - 1]);
            }
        }
        return str;
    }

    public static bool TryParseDateTimeWithFormat(this string dateTimeInput, string format, out DateTime? nDate)
    {
        bool isParsed = DateTime.TryParseExact(dateTimeInput, format, CultureInfo.InvariantCulture,
            DateTimeStyles.None, out var date);

        if (isParsed)
            nDate = date;
        else
            nDate = null;

        return isParsed;
    }

    public static string EnsureEndWithSlash(this string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return "/";
        }

        if (!input.EndsWith("/"))
        {
            return string.Format("{0}/", input);
        }

        return input;
    }

    public static bool IsNotContainSpecialCharaters(this string input)
    {
        Regex r = new Regex(@"^[a-zA-Z0-9-]*$");
        return r.IsMatch(input);
    }

    public static T GetValueFromDescription<T>(this string description) where T : Enum
    {
        foreach (var field in typeof(T).GetFields())
        {
            if (Attribute.GetCustomAttribute(field,
                    typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
            {
                if (attribute.Description == description)
                    return (T)field.GetValue(null);
            }
            else
            {
                if (field.Name == description)
                    return (T)field.GetValue(null);
            }
        }

        throw new ArgumentException("Not found.", nameof(description));
    }

    public static bool IsValidTimestamp(this string timestamp)
    {
        try
        {
            if (string.IsNullOrEmpty(timestamp))
                return false;
            var r = new Regex(@"^(\d\d):(\d\d):(\d\d)$");
            return r.IsMatch(timestamp);
        }
        catch (Exception)
        {
            return false;
        }
    }

    public static string RemoveSpecialCharacters(this string input)
    {
        Regex r = new Regex("(?:[^a-z0-9-]|(?<=['\"])s)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
        input = r.Replace(input, string.Empty);

        //replace multiple - characters with one -
        return Regex.Replace(input, @"-+", "-");
    }

    public static List<string> GenerateKeyCode(int length, int quantity = 1)
    {
        Random random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        List<string> list = new List<string>();
        while (list.Count < quantity)
        {
            var code = new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
            if (!list.Contains(code))
            {
                list.Add(code);
            }
        }

        return list;
    }

    public static string GenerateUniqueID(int length = 20)
    {
        // We chose an encoding that fits 6 bits into every character,
        // so we can fit length*6 bits in total.
        // Each byte is 8 bits, so...
        int sufficientBufferSizeInBytes = (length * 6 + 7) / 8;

        var buffer = new byte[sufficientBufferSizeInBytes];
        random.GetBytes(buffer);
        return Convert.ToBase64String(buffer).Substring(0, length);
    }

    public static string GetMd5Hash(this string input)
    {
        using MD5 md5Hash = MD5.Create();
        byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
        StringBuilder sBuilder = new StringBuilder();

        for (int i = 0; i < data.Length; i++)
        {
            sBuilder.Append(data[i].ToString("x2"));
        }
        return sBuilder.ToString();
    }

    public static string RebuildClassinUrl(string originalUrl, string email)
    {
        if (string.IsNullOrEmpty(originalUrl)) return string.Empty;

        const string baseUrlClassin = "https://www.eeo.cn/client/invoke/index.html";
        var url = new Uri(originalUrl);
        var query = url.Query.Split('&')?.ToList();
        if (query == null || !query.Any()) return baseUrlClassin;

        int phoneNumberIndex = query.FindIndex(x => !string.IsNullOrEmpty(x) && x.Contains("telephone"));
        if (phoneNumberIndex > -1)
        {
            query[phoneNumberIndex] = $"telephone={email}";
        }

        return $"{baseUrlClassin}?{string.Join('&', query)}";
    }

    public static byte[] GetHash(string inputString)
    {
        using (HashAlgorithm algorithm = SHA256.Create())
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
    }

    public static string GetHashString(string inputString)
    {
        StringBuilder sb = new StringBuilder();
        foreach (byte b in GetHash(inputString))
            sb.Append(b.ToString("X2"));

        return sb.ToString();
    }

    public static IEnumerable<TResult> ConvertSampleTemplate<TResult>(this string sampleTemplateObject)
    {
        if (string.IsNullOrEmpty(sampleTemplateObject))
            return Enumerable.Empty<TResult>();

        try
        {
            return JsonSerializer.Deserialize<IEnumerable<TResult>>(sampleTemplateObject, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
        catch (Exception ex)
        {
            throw new SystemException(ex.Message);
        }
    }
    

    /// <summary>
    /// Remove all redundant characters in a phone string (e.g. +, -, (, ), space). Vietnamese phone number supported only.
    /// </summary>
    /// <param name="phoneNumber"></param>
    /// <returns></returns>
    public static string NormalizePhoneNumber([NotNull] this string phoneNumber)
    {
        phoneNumber = phoneNumber.Trim();
        phoneNumber = phoneNumber.Replace("+", string.Empty);
        phoneNumber = phoneNumber.Replace("-", string.Empty);
        _ = phoneNumber.Replace(" ", string.Empty);

        var vnPhoneRegex = VietNamPhoneRegex();
        var mobilePhoneRegex = MobilePhoneRegex();

        if (vnPhoneRegex.IsMatch(phoneNumber))
        {
            return phoneNumber;
        }
        else if (mobilePhoneRegex.IsMatch(phoneNumber))
        {
            return $"{Constants.Common.VNPhoneHeader}{phoneNumber.Substring(1)}";
        }
        return phoneNumber;

    }

    [GeneratedRegex(Constants.Common.VNPhoneRegex)]
    private static partial Regex VietNamPhoneRegex();
    [GeneratedRegex(Constants.Common.MobilePhoneRegex)]
    private static partial Regex MobilePhoneRegex();

    // Existing methods...

    /// <summary>
    /// Extracts the course ID from a given URL.
    /// </summary>
    /// <param name="url">The URL to extract the course ID from.</param>
    /// <returns>The extracted course ID, or null if not found.</returns>
 public static string ExtractCourseIdFromUrl([NotNull] this string url)
    {  
     
        var match = StudyCourseRegex().Match(url);
        if (match.Success)
        {
            return match.Groups[2].Value;
        }
        return url;
    }

    [GeneratedRegex(Constants.Common.FullRegexPattern)]
    public static partial Regex StudyCourseRegex(); 
}