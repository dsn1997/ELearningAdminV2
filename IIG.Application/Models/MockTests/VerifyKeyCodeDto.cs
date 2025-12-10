using IIG.Core.Common.Enums;

namespace IIG.Application.Models;

public class VerifyKeyCodeDto
{
    public int TotalQuestions { get; set; }
    public int TotalSeconds { get; set; }
    public DateTime? StartedDoingExamDate { get; set; }
    public DateTime? SubmittedDate { get; set; }
    public EMockTestKeyCodeStatus Status { get; set; }
    public Guid MockTestId { get; set; }
    public string MocktestName { get; set; }

    public CourseScoringModel CourseScoringInfo { get; set; }
}

public class CourseScoringModel
{
    public Guid? CourseScoringId { get; set; }
    public ECourseScoringStatus? ScoringStatus { get; set; }
}