namespace IIG.Web.Data.Models.Practices.WebUserChoose;

public class WebUserChooseDelete
{
    public Guid WebUserId { get; set; }

    public Guid QuestionId { get; set; }

    public Guid StepId { get; set; }
     
    public Guid? AnswerId { get; set; }

    public Guid? MatchingQuestionId { get; set; }
}