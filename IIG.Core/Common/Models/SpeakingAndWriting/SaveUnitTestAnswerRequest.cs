using IIG.Core.Common.Enums;
using IIG.Core.Common.Models.Files;
using System.Text.Json.Serialization;

namespace IIG.Core.Common.Models.SpeakingAndWriting
{
    public class SaveUnitTestAnswerRequest
    {
        [JsonIgnore]
        public Guid WebUserId { get; set; }
        public Guid UnitTestId { get; set; }
        public Guid QuestionnaireId { get; set; }
        public Guid QuestionId { get; set; }
        public string AnswerText { get; set; }
        public FileInsertModel FileInfo { get; set; }
    }
}
