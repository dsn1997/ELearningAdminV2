using IIG.Core.Common.Models.Paging;

namespace IIG.Web.Data.Models.Notification;
public class NotificationListModelRequest : BasePaginationRequest
{
    public bool? IsRead { get; set; }
}
