using IIG.Core.Common.Models.SpeakingAndWriting;

namespace IIG.Application.Models.MockTestPart;
public class MockTestPartInfoDto
    : IIsMarkByAIProperty, ITotalPartTimeProperty
{

    public Guid Id { get; set; }
    public string Name { get; set; }
    public int SortOrder { get; set; }
    public int NumOfQuestions { get; set; }
    public int NumOfTime { get; set; }
    public int DelayTimeEachQuestion { get; set; }
    public bool? IsMarkByAI { get; set; }
    public TimeSpan? TotalPartTime { get; set; }
}