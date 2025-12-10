using IIG.Core.Common.MongoDataModels;

namespace IIG.Web.Data.Models.Practices.Assessment;

public class AssessmentSeeAnswerResponse
{
    public Guid QuestionId { get; set; }
    
    public Guid? AnswerId { get; set; }

    public Guid? CorrectAnswerId { get; set; }
   
    public List<string> CorrectAnswerText { get; set; } = new();

    public List<MgCorrectAnswerQuestionMatching> CorrectAnswerQuestionMatchings { get; set; }
}