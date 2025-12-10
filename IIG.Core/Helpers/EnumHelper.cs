using System.Runtime.Serialization;

namespace IIG.Core.Helpers
{
    public static class EnumHelper
    {
        public static string ToEnumMemberAttrValue(this Enum @enum)
        {
            var attr =
                @enum.GetType().GetMember(@enum.ToString()).FirstOrDefault()?.
                    GetCustomAttributes(false).OfType<EnumMemberAttribute>().
                    FirstOrDefault();
            if (attr == null)
                return @enum.ToString();
            return attr.Value;
        }

        public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> self)
               => self.Select((item, index) => (item, index));
    }
}
