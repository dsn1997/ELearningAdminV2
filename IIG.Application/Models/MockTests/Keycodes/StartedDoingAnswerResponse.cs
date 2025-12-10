using IIG.Core.Common.MongoDataModels.MockTests;

namespace IIG.Web.Data.Models.MockTests.Keycodes;
public class StartedDoingAnswerResponse
{
    public string MockTestName { get; set; }
    public int TimeRemaining { get; set; }
    public Guid? CurrentMockTestPartId { get; set; }
    public Guid? CurrentQuestionnaireId { get; set; }
    public MgMockTestMenuModel MockTestMenu { get; set; } = new();
}
