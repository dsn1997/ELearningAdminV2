using IIG.Web.Data.Models.Practices.Lesson;
using IIG.Web.Data.Models.Practices.UnitTest;

namespace IIG.Web.Data.Models.CourseDetails;

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