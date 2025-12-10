using IIG.Core.Common.Enums;

namespace IIG.Web.Data.Models.MockTests.MockTestSection;

public class MockTestSectionDto
{
    public Guid Id { get; set; }
    public Guid MockTestId { get; set; }
    public Guid RankingScoreId { get; set; }
    public string Name { get; set; }
    public int SortOrder { get; set; }
    public EMockTestSectionType Type { get; set; }
    public int NumberOfQuestions { get; set; }
    public int NumberOfTime { get; set; }
}


public class MockTestSectionForSubmitDto
{
    public Guid Id { get; set; }
    public Guid MockTestId { get; set; }
    public Guid RankingScoreId { get; set; }
    public string Name { get; set; }
    public int SortOrder { get; set; }
    public EMockTestSectionType Type { get; set; }
    public int NumberOfQuestions { get; set; }
    public int NumberOfTime { get; set; }
    public virtual ICollection<MocktestPartForSubmitDto> MocktestParts { get; set; }

}

public class MocktestPartForSubmitDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int SortOrder { get; set; }

    public virtual ICollection<MocktestPartQuestionnaireForSubmitDto> MocktestPartQuestionnaires { get; set; }

}

public class MocktestPartQuestionnaireForSubmitDto
{
    public Guid Id { get; set; }
    public int SortOrder { get; set; }
    public virtual QuestionnaireForSubmitDto Questionnaire { get; set; }

}
public class QuestionnaireForSubmitDto
{
    public Guid Id { get; set; }
    public Guid QuestionnaireId { get; set; }
    public int? SortOrder { get; set; }
    public TimeSpan? RecordingTime { get; set; }
    public virtual ICollection<QuestionForSubmitDto> Questions { get; set; }

}

public class QuestionForSubmitDto
{
    public Guid Id { get; set; }
    public Guid QuestionnaireId { get; set; }
    public int? SortOrder { get; set; }
    public TimeSpan? RecordingTime { get; set; }


}


