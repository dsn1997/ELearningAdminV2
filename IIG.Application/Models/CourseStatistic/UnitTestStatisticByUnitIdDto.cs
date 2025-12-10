using IIG.Core.Common.Models.MockTests;

namespace IIG.Web.Data.Models.CourseStatistic;

public class UnitTestStatisticByUnitIdDto
    : IRedoSettingProperty
{
    public Guid Id { get; set; }

    public Guid UnitId { get; set; }

    public string Title { get; set; }

    public bool IsCompleted { get; set; }

    public bool CanStart { get; set; }
    public int? RedoNumber { get; set; }
    public int? WatchCount { get; set; }
    public bool IsSWType { get; set; } = false;
}