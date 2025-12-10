using IIG.Core.Common.Enums;
using IIG.Core.Common.Models.MockTests;

namespace IIG.Application.Models;
public class UnitTestStatisticInUnitDetailDto
    : IRedoSettingProperty
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public Guid UnitId { get; set; }
    public int TotalQuestions { get; set; }
    public int TotalCorrectAnswers { get; set; }
    public bool IsSubmitted { get; set; }
    public long CurrentScore { get; set; }
    public long TotalScore { get; set; }
    public int SortOrder { get; set; }
    public bool CanStart { get; set; }
    public int? RedoNumber { get; set; }
    public bool IsSWType { get; set; } = false;
    public ECourseScoringStatus? ScoringStatus { get; set; }
}
