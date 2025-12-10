using IIG.Core.Common.MongoDataModels.Keycodes;

namespace IIG.Web.Data.Models.MockTests.Keycodes;
public class GetQuestionnaireDetailRequest : MockTestKeyCodeBaseRequest
{
    public Guid QuestionnaireId { get; set; }
}
