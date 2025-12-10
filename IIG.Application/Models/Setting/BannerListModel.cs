using IIG.Core.Common.Enums;

namespace IIG.Web.Data.Models.Setting;
public class BannerListModel
{
    public Guid Id { get; set; }

    public string ImageUrl { get; set; }

    public string Name { get; set; }

    public string LinkUrl { get; set; }

}