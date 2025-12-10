using IIG.Web.Data.Models.Answers;

namespace IIG.Web.Data.Models.Practices.WebUserUnitTestChoose;
public class AnswerSubmitRequest : ChooseBaseModelRequest
{
    public Guid QuestionnaireId { get; set; }
}
