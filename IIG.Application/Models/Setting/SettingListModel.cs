using IIG.Core.Common.Enums;

namespace IIG.Web.Data.Models.Setting;
public class SettingListModel
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Value { get; set; }

    public ESettingType Type { get; set; }

    public ESettingDataType DataType { get; set; }

    public string ImageUrl { get; set; }

    public int SortOrder { get; set; }
}