using IIG.Core.Common.Enums;

namespace IIG.Application.Models.ExamTool.MockTest;

public class MockTestKeyCodeDto
{
    public Guid MockTestId { get; set; }

    public string Name { get; set; }
   
    public string Code { get; set; }
   
    public EMockTestKeyCodeStatus? Status { get; set; }
    
    public DateTime? ExamDate { get; set; }
    
    public DateTime? ExpiryDate { get; set; }
    
    public string RankingScore { get; set; }
    public int? WatchCount { get; set; }
    public ECourseScoringStatus? ScoringStatus { get; set; }
    public DateTime? SubmittedDate { get; set; }
    public bool IsSWType { get; set; } = false;
}