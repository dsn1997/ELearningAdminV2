namespace IIG.Web.Data.Models.Practices.Assessment;

public class AnswerChooseRequest
{
    public Guid QuestionId { get; set; }
   
    public Guid AnswerId { get; set; }

    public string AnswerText { get; set; }

    public Guid? MatchingQuestionId { get; set; }
}