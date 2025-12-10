using IIG.Core.Common.MongoDataModels.Keycodes;

namespace IIG.Web.Data.Models.MockTests.Keycodes;
public class GetAnswerRequest : MockTestKeyCodeBaseRequest
{
    public Guid MockTestPartId { get; set; }
    public Guid QuestionnaireId { get; set; }
}
