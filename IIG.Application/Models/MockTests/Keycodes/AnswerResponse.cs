using IIG.Core.Common.Enums;

namespace IIG.Web.Data.Models.MockTests.Keycodes;
public class AnswerResponse
{
    public Guid AnswerId { get; set; }
    public Guid QuestionId { get; set; }
    public string AnswerText { get; set; }
    public Guid? MatchingQuestionId { get; set; }

    public Guid QuestionnaireId { get; set; }
    public EQuestionnaireType QuestionnaireType { get; set; }
    public Guid MockTestPartId { get; set; }
    public Guid MockTestSectionId { get; set; }
}
