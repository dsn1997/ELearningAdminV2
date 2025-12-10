using IIG.Core.Common.Enums.OrderBy;
using IIG.Core.Common.Models.Paging;

namespace IIG.Web.Data.Models.Order;

public class SearchingOrderByUserRequest : BasePaginationRequest<EOrderOrderBy>
{
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}