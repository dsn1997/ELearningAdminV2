using IIG.Application.Models.Step;

namespace IIG.Application.Models.Lesson;

public class LessonListModel
{
    public Guid Id { get; set; }
    public Guid UnitId { get; set; }
    public string Title { get; set; }
    public List<StepListModel> Steps { get; set; }
}


public class LessonListShortModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public List<StepListShortModel> Steps { get; set; }
}