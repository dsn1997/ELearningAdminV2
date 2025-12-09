
using System.ComponentModel;
using System.Reflection;
namespace IIG.Core.Common.Enums;
public static partial class CommonEnum
{
    public class ItemObj<T>
    {
        public T Id { get; set; }
        public string Name { get; set; }
    }

    #region function
    public static List<ItemObj<short>> EnumToList(Type TypeObject)
    {
        List<ItemObj<short>> objTemList = new List<ItemObj<short>>();
        try
        {
            foreach (object iEnumItem in Enum.GetValues(TypeObject))
            {
                ItemObj<short> objTem = new ItemObj<short>();
                objTem.Id = ((short)iEnumItem);
                objTem.Name = GetEnumDescription((Enum)iEnumItem);
                objTemList.Add(objTem);
            }
            return objTemList;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static string GetEnumDescription(Enum en)
    {
        Type type = en.GetType();

        try
        {
            MemberInfo[] memInfo = type.GetMember(en.ToString());

            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;
            }
        }
        catch (Exception)
        {
            return string.Empty;
        }

        return en.ToString();
    }

    #endregion
}