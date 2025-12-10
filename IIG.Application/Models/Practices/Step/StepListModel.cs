using IIG.Core.Common.Models.MockTests;

namespace IIG.Application.Models.Step;

public class StepListModel
    : IRedoSettingProperty
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool CanStart { get; set; }
    public int? RedoNumber { get; set; }
    public int? WatchCount { get; set; }
}


public class StepListShortModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
}