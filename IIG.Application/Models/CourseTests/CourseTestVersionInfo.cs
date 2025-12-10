using IIG.Application.Models.Versioning;
using IIG.Application.Models.WebUserUnitTestChoose;

namespace IIG.Application.Models;

public class CourseTestVersionInfo
{
    public QuestionnaireVersionInfo QuestionnaireInfo { get; set; }
    
    public IEnumerable<WebUserCourseTestChooseModel> CourseTestSubmittedData { get; set; }
}