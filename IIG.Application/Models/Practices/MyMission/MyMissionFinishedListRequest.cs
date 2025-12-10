using IIG.Core.Common.Models.Paging;

namespace IIG.Web.Data.Models.Practices.MyMission;
public class MyMissionFinishedListRequest : BasePaginationRequest
{
    public DateTime? FromDate { get; set; }

    public DateTime? ToDate { get; set; }
}
