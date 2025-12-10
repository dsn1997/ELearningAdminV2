using IIG.Core.Common.Enums;

namespace IIG.Web.Data.Models.HomePage;
public class SearchingCourseHomePageResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string NameNonAscii { get; set; }

    public EHomeSearchProductType ProductType { get; set; }
}
