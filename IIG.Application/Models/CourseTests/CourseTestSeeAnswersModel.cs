using IIG.Core.Common.MongoDataModels.MockTests;

namespace IIG.Web.Data.Models.CourseTests;

public class CourseTestSeeAnswersModel
{
    public string CourseTestTitle { get; set; }

    public int TotalQuestion { get; set; }

    public MgMockTestMenuModel Menu { get; set; }
}