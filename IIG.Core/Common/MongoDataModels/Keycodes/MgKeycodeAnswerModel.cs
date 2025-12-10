using IIG.Core.Common.Enums;
using IIG.Core.Common.Models.SpeakingAndWriting;
using IIG.Core.Providers.MongoDbProvider.Infrastructure;
using IIG.Core.Providers.MongoDbProvider.Models;

namespace IIG.Core.Common.MongoDataModels.Keycodes
{
    [BsonCollection("keyCodeAnswers")]
    public class MgKeyCodeAnswerModel : Document, IRecordingFile, ILastSaveAnswerDate
    {
        public string KeyCode { get; set; }
        public Guid AnswerId { get; set; }
        public Guid QuestionId { get; set; }
        public string AnswerText { get; set; }
        public Guid? MatchingQuestionId { get; set; }
        
        public Guid QuestionnaireId { get; set; }
        public EQuestionnaireType QuestionnaireType { get; set; }
        public Guid MockTestPartId { get; set; }
        public Guid MockTestSectionId { get; set; }
        public Guid? RecordingFileId { get; set; }
        public DateTime? LastSaveDate { get; set; }
    }
}
