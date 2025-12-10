using IIG.Core.Common.Models.Paging;

namespace IIG.Application.Models;
public class NotificationListModelRequest : BasePaginationRequest
{
    public bool? IsRead { get; set; }
}
