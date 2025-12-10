using IIG.Core.Common.Models.Paging;

namespace IIG.Application.Models.MyMission;
public class MyMissionFinishedListRequest : BasePaginationRequest
{
    public DateTime? FromDate { get; set; }

    public DateTime? ToDate { get; set; }
}
