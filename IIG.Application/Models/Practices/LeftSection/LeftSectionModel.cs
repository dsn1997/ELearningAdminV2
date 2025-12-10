namespace IIG.Application.Models.LeftSection;
public class LeftSectionModel
{
    public Guid Id { get; set; }
    public Guid QuestionnaireId { get; set; }
    public int? SortOrder { get; set; }
    public string Title { get; set; }
    public Guid? AudioFileId { get; set; }
    public Guid? VideoId { get; set; }
    public Guid? ImageFileId { get; set; }
    public string TextScript { get; set; }
    public TimeSpan? SpaceTime { get; set; }
    public string TextContent { get; set; }
}
