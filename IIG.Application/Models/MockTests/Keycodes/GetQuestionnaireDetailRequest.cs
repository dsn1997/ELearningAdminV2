using IIG.Core.Common.MongoDataModels.Keycodes;

namespace IIG.Application.Models.Keycodes;
public class GetQuestionnaireDetailRequest : MockTestKeyCodeBaseRequest
{
    public Guid QuestionnaireId { get; set; }
}
