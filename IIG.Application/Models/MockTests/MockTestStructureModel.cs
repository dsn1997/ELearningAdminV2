using IIG.Core.Common.Enums;
using IIG.Core.Common.Models.SpeakingAndWriting;

namespace IIG.Application.Models;

public class MockTestStructureModel
{
    public Guid MockTestId { get; set; }

    public string MockTestName { get; set; }

    public List<MockTestSectionStructureModel> MockTestSections { get; set; } = new List<MockTestSectionStructureModel>();
}

public class MockTestSectionStructureModel
{
    public Guid Id { get; set; }
    public Guid MockTestId { get; set; }
    public Guid RankingScoreId { get; set; }
    public string Name { get; set; }
    public int SortOrder { get; set; }
    public EMockTestSectionType Type { get; set; }
    public int NumberOfQuestions { get; set; }
    public int NumberOfTime { get; set; }
    public int NumberOfTimeEstimate { get; set; }
    public List<MockTestPartStructureModel> Parts { get; set; } = new List<MockTestPartStructureModel>();
}

public class MockTestPartStructureModel : ITotalPartTimeProperty
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int SortOrder { get; set; }
    public int NumOfQuestions { get; set; }
    public int NumOfTime { get; set; }
    public string Content { get; set; }
    public Guid? IntroAudioFileId { get; set; }
    public int DelayTimeEachQuestion { get; set; }
    public TimeSpan? TotalPartTime { get; set; }
}

