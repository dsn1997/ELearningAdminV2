using IIG.Core.Common.Enums;

namespace IIG.Web.Data.Models.Practices.MyCourse;
public class MyTodayClassListModel
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string ImageUrl { get; set; }

    public DateTime? LiveClassStartDate { get; set; }

    public DateTime? LiveClassEndDate { get; set; }

    public bool? IsPreTenMinute { get; set; }
}
