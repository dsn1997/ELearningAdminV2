using IIG.Core.Common.Enums;
using IIG.Core.Common.Models.Files;
using IIG.Core.Common.Models.SpeakingAndWriting;
using IIG.Core.Common.MongoDataModels.Keycodes;

namespace IIG.Web.Data.Models.MockTests.Keycodes;
public class SaveAnswerRequest : MockTestKeyCodeBaseRequest
{
    public Guid MockTestSectionId { get; set; }
    public Guid MockTestPartId { get; set; }
    public Guid QuestionnaireId { get; set; }
    public EQuestionnaireType QuestionnaireType { get; set; }
    public Guid QuestionId { get; set; }
    public Guid? AnswerId { get; set; }
    public string AnswerText { get; set; }
    public Guid? MatchingQuestionId { get; set; }
    public FileInsertModel FileInfo { get; set; }
}
