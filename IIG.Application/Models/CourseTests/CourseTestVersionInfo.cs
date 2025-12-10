using IIG.Web.Data.Models.Practices.Versioning;
using IIG.Web.Data.Models.Practices.WebUserUnitTestChoose;

namespace IIG.Web.Data.Models.CourseTests;

public class CourseTestVersionInfo
{
    public QuestionnaireVersionInfo QuestionnaireInfo { get; set; }
    
    public IEnumerable<WebUserCourseTestChooseModel> CourseTestSubmittedData { get; set; }
}