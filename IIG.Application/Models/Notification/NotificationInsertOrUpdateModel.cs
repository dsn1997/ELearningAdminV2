using IIG.Core.Common.Models.Files;
using System.Text.Json.Serialization;

namespace IIG.Application.Models;
public class NotificationInsertOrUpdateModel : BaseTranslation<NotificationTranslationDto>
{
    public string Name { get; set; }
    public DateTime DatetimeTrigger { get; set; }
    public List<ImageInfoModel> ImageInfoModels { get; set; }

    [JsonIgnore]
    public Guid? ImageFileId { get; set; }

    [JsonIgnore]
    public string FullTextSearch { get; set; }
    public short? Type { get; set; } = null;
}

public class NotificationTranslationDto : BaseConcreteTranslation
{
    public Guid NotificationId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public string LinkUrl { get; set; }
}
