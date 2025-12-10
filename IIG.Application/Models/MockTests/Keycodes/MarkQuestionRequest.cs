using IIG.Core.Common.MongoDataModels.Keycodes;

namespace IIG.Application.Models.Keycodes;
public class MarkQuestionRequest : MockTestKeyCodeBaseRequest
{
    public Guid MockTestPartId { get; set; }
    public Guid QuestionnaireId { get; set; }
    public bool Marked { get; set; }
}
