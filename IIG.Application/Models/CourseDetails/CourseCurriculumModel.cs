using IIG.Application.Models.Lesson;
using IIG.Application.Models.UnitTest;

namespace IIG.Application.Models;

public class CourseCurriculumModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public List<LessonListModel> Lessons { get; set; }
    public List<UnitTestListModel> UnitTests { get; set; }
}

public class CourseCurriculumShortModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public List<LessonListShortModel> Lessons { get; set; }
}