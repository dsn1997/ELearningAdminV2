namespace IIG.Application.Models;
public class NotificationListModel
{
    public Guid Id { get; set; }

    public DateTime DateTimeTrigger { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public string LinkUrl { get; set; }

    public string ImageUrl { get; set; }

    public bool IsRead { get; set; }
    public int? Type { get; set; }

    public bool? isLocked { get; set; }
}