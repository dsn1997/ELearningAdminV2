using IIG.Core.Common.Enums;
using IIG.Core.Common.Models.SpeakingAndWriting;
using IIG.Core.Providers.MongoDbProvider.Infrastructure;
using IIG.Core.Providers.MongoDbProvider.Models;
using MongoDB.Bson.Serialization.Attributes;

namespace IIG.Core.Common.MongoDataModels.CourseTests;

[BsonCollection("courseTestAnswers")]
[BsonIgnoreExtraElements]
public class MgCourseTestAnswerModel : Document, IRecordingFile, ILastSaveAnswerDate
{
    public Guid WebUserId { get; set; }
    public Guid? AnswerId { get; set; }
    public Guid CourseTestId { get; set; }
    public Guid QuestionId { get; set; }
    public string AnswerText { get; set; }
    public Guid? MatchingQuestionId { get; set; }
    public Guid? RecordingFileId { get; set; }


    public Guid QuestionnaireId { get; set; }
    public EQuestionnaireType QuestionnaireType { get; set; }
    public Guid MockTestPartId { get; set; }
    public Guid MockTestSectionId { get; set; }
    public DateTime? LastSaveDate { get; set; }
}