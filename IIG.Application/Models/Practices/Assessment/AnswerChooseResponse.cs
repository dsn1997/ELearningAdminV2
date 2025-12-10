namespace IIG.Web.Data.Models.Practices.Assessment;

public class AnswerChooseResponse
{
    public Guid QuestionId { get; set; }
    public Guid AnswerId { get; set; }
    public string AnswerText { get; set; }
    public Guid? MatchingQuestionId { get; set; }
    public Guid? CorrectAnswerId { get; set; }
    public List<string> CorrectAnswerText { get; set; } = new();
    public bool IsCorrect { get; set; }
}