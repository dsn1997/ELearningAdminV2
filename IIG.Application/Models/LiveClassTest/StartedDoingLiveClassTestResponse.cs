using IIG.Core.Common.MongoDataModels.MockTests;

namespace IIG.Application.Models;
public class StartedDoingLiveClassTestResponse
{
    public Guid LiveClassId { get; set; }
    
    public string LiveClassName { get; set; }

    public string LiveClassTestName { get; set; }

    public int TotalSeconds { get; set; }

    public int TotalQuestions { get; set; }

    public MgMockTestMenuModel MockTestMenu { get; set; } = new();

    public Guid? LastestMockTestPartId { get; set; }
    public Guid? LastestQuestionnaireId { get; set; }
}
