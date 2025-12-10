using IIG.Core.Common.Models.Files;

namespace IIG.Web.Data.Models.Practices.Versioning;

public class LeftSectionVersionInfo
{
    public string Id { get; set; }
    public Guid LeftSectionId { get; set; }
    public Guid QuestionnaireId { get; set; }
    public string Title { get; set; }
    public Guid? AudioFileId { get; set; }
    public Guid? VideoId { get; set; }
    public Guid? ImageFileId { get; set; }
    public FileDto ImageInfo { get; set; }
    public string TextScript { get; set; }
    public string TextContent { get; set; }
}