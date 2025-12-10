namespace IIG.Web.Data.Models.MockTests.MockTestPartQuestionnaire;

public class MockTestQuestionAnswerIdModel
{
    public Guid Id { get; set; }
    public Guid QuestionnaireId { get; set; }
    public int SortOrder { get; set; }
    public string MatchingKey { get; set; } = string.Empty;
}