using IIG.Core.Common.Models.Files;
using IIG.Core.Common.Models.SpeakingAndWriting;
using IIG.Core.Providers.MongoDbProvider.Infrastructure;
using IIG.Core.Providers.MongoDbProvider.Models;

namespace IIG.Core.Common.MongoDataModels
{
    [BsonCollection("unitTestAnswers")]
    public class MgUnitTestAnswer : Document,
        IRecordingFile, ILastSaveAnswerDate
    {
        public Guid WebUserId { get; set; }
        public Guid UnitTestId { get; set; }
        public Guid QuestionnaireId { get; set; }
        public Guid QuestionId { get; set; }
        public string TextAnswer { get; set; }
        public Guid? RecordingFileId { get; set; }
        public DateTime? LastSaveDate { get; set; }
        public int? RemainingExamInSeconds { get; set; }
    }
}
