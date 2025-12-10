using IIG.Core.Common.Models.Paging;

namespace IIG.Application.Models;
public class NotificationPagingListModel : PaginationSet<NotificationListModel>
{
    public int TotalUnread { get; set; } = 0;
}
