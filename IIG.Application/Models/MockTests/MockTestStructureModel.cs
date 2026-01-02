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

public class MockTestViewResultDetailModel
{
    public Guid MockTestId { get; set; }
    public IEnumerable<MockTestQuestionDetailModel> Questions { get; set; }
}

public class MockTestQuestionDetailModel
{
    public short? STT { get; set; }
    public Guid QuestionId { get; set; }
    public Guid QuestionnaireId { get; set; }
    public bool? IsUsingAnswerAsQuestion { get; set; }
    public string PartName { get; set; }
    public Guid PartId { get; set; }
    public string SectionName { get; set; }
    public Guid SectionId { get; set; }
    public bool? IsCorrect { get; set; }
    public IEnumerable<string> Tags { get; set; }
}