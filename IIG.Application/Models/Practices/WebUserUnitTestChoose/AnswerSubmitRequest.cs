using IIG.Application.Models;

namespace IIG.Application.Models.WebUserUnitTestChoose;
public class AnswerSubmitRequest : ChooseBaseModelRequest
{
    public Guid QuestionnaireId { get; set; }
}
