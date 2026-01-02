using IIG.Core.Common.Models.Files;
using IIG.Core.Common.MongoDataModels.MockTests;
using System.ComponentModel.DataAnnotations;

namespace IIG.Application.Models;

public class VerifyMockTestWrapperDto
{
    public Guid MocktestWrapperId { get; set; }
}

public class StartedDoingAnswerMockTestWrapperRequest
{
    public Guid MocktestWrapperId { get; set; }
    public string Browser { get; set; }
    public string firebaseToken { get; set; }
    public string ClientIp { get; set; }
}

public class StartedDoingAnswerMockTestWrapperResponse
{
    public string KeyCode { get; set; }
    public string MockTestName { get; set; }
    public int TimeRemaining { get; set; }
    public Guid? CurrentMockTestPartId { get; set; }
    public Guid? CurrentQuestionnaireId { get; set; }
    public MgMockTestMenuModel MockTestMenu { get; set; } = new();
}

public class  ViewMockTestQuestionResultDetailInputDto 
{
    public Guid MockTestId { get; set; }
    public string KeyCode { get; set; }
    public DateTime? SubmitDate { get; set; }
}
