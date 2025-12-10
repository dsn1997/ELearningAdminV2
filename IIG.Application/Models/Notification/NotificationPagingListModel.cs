using IIG.Core.Common.Models.Paging;

namespace IIG.Web.Data.Models.Notification;
public class NotificationPagingListModel : PaginationSet<NotificationListModel>
{
    public int TotalUnread { get; set; } = 0;
}
