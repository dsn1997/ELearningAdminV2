using IIG.Core.Common.Enums;

namespace IIG.Web.Data.Models.MockTests;

public class MockTestKeyCodeInsert
{
    public string Code { get; set; }
    public Guid MocktestId { get; set; }
    public Guid? WebUserId { get; set; }
    public Guid? CourseId { get; set; }
    public Guid? OrderId { get; set; }
    public Guid? CoursePriceId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime? StartedDoingExamDate { get; set; }
    public DateTime? SubmittedDate { get; set; }
    public string ClientIp { get; set; }
    public string Browser { get; set; }
    public int? TimeRemaining { get; set; }
    public DateTime? MocktestPublishedAt { get; set; }
    public EMockTestKeyCodeType Type { get; set; }
    public string ChallengeName { get; set; }
    public string IdentificationNumber { get; set; }
}