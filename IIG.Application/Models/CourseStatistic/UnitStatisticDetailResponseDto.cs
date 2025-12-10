using IIG.Core.Common.Enums;

namespace IIG.Application.Models;

public class UnitStatisticDetailResponseDto
{
    public Guid UnitId { get; set; }
    public List<LessonStatisticInUnitDetailDto> Lessons { get; set; } = new();
    public List<UnitTestStatisticInUnitDetailDto> UnitTests { get; set; } = new();
    public EUnitLessonStepStatus Status { get; set; }
}