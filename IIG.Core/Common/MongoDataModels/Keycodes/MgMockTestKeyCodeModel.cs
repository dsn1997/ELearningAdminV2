using IIG.Core.Common.MongoDataModels.MockTests;
using IIG.Core.Providers.MongoDbProvider.Infrastructure;
using IIG.Core.Providers.MongoDbProvider.Models;

namespace IIG.Core.Common.MongoDataModels.Keycodes;

[BsonCollection("mockTestKeyCodes")]
public class MgMockTestKeyCodeModel : Document
{
    public string KeyCode { get; set; }
    public string MockTestName { get; set; }
    public string Browser { get; set; }
    public string ClientIp { get; set; }
    public int TimeRemaining { get; set; }
    public Guid? Cookie { get; set; }
    public Guid MockTestId { get; set; }
    public DateTime? StartedDoingExamDate { get; set; }
    public DateTime? EndedDoingExamDate { get; set; }
    public Guid? CurrentMockTestPartId { get; set; }
    public Guid? CurrentQuestionnaireId { get; set; }
    public MgMockTestMenuModel MockTestMenu { get; set; } = new();
}