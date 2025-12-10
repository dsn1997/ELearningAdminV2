namespace IIG.Web.Data.Models.Practices.Lesson;

public class LessonDto
{
    public Guid Id { get; set; }

    public Guid UnitId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public int? SortOrder { get; set; }

    public int? LessonCompletion { get; set; }

    public bool CanStart { get; set; }
}