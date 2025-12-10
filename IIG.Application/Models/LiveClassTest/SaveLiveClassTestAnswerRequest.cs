using System.Text.Json.Serialization;
using IIG.Core.Common.Enums;
using IIG.Core.Common.Models.Files;

namespace IIG.Web.Data.Models.LiveClassTest;
public class SaveLiveClassTestAnswerRequest
{
    [JsonIgnore]
    public Guid WebUserId { get; set; }

    public Guid? AnswerId { get; set; }

    public Guid LiveClassTestId { get; set; }

    public Guid MockTestSectionId { get; set; }

    public Guid MockTestPartId { get; set; }

    public Guid QuestionnaireId { get; set; }

    public EQuestionnaireType QuestionnaireType { get; set; }

    public Guid QuestionId { get; set; }

    public string AnswerText { get; set; }

    public Guid? MatchingQuestionId { get; set; }
    public FileInsertModel FileInfo { get; set; }
}
