using IIG.Core.Common.Models.MockTests;
using IIG.Web.Data.Models.MockTests;

namespace IIG.Web.Data.Models.CourseTests;

public class CourseTestMockTestStructureModel : MockTestStructureModel, IRedoSettingProperty
{
    public Guid CourseTestId { get; set; }

    public string CourseTestTitle { get; set; }

    public Guid CourseId { get; set; }
    public int? RedoNumber { get; set; }
}

