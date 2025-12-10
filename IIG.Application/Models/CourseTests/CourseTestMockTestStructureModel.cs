using IIG.Core.Common.Models.MockTests;
using IIG.Application.Models;

namespace IIG.Application.Models;

public class CourseTestMockTestStructureModel : MockTestStructureModel, IRedoSettingProperty
{
    public Guid CourseTestId { get; set; }

    public string CourseTestTitle { get; set; }

    public Guid CourseId { get; set; }
    public int? RedoNumber { get; set; }
}

