using IIG.Core.Common.Enums;
using IIG.Core.Common.Models.Files;
using IIG.Core.Common.Models.SpeakingAndWriting;
using IIG.Core.Providers.MongoDbProvider.Infrastructure;
using IIG.Core.Providers.MongoDbProvider.Models;

namespace IIG.Core.Common.MongoDataModels
{
    [BsonCollection("userSubmitLessons")]
    public class MgUserSubmitLesson : Document
    {
        public Guid CourseId { get; set; }
        public Guid StepQuestionnaireId { get; set; }
        public Guid UserId { get; set; }
        public Guid QuestionnaireId { get; set; }
        public EQuestionnaireType QuestionnaireType { get; set; }
        public EUserSubmitLessonStatus Status { get; set; }
        public Guid QuestionId { get; set; }
        public string TextAnswer { get; set; }
        public Guid? RecordingFileId { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class MgUserSubmitLessonQuestion
        : IRecordingFile
    {
        public Guid QuestionId { get; set; }
        public string TextAnswer { get; set; }
        public Guid? RecordingFileId { get; set; }
    }

    public class MgUserSubmitLessonQuestionRequestModel
    {
        
    }
}
