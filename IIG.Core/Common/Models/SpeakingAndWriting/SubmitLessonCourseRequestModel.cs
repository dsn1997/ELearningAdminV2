using IIG.Core.Common.Enums;
using IIG.Core.Common.Models.Files;
using Newtonsoft.Json;

namespace IIG.Core.Common.Models.SpeakingAndWriting
{
    public class SubmitLessonCourseRequestModel
    {
        public Guid CourseId { get; set; }
        public Guid UnitId { get; set; }
        public Guid LessonId { get; set; }
        public Guid StepQuestionnaireId { get; set; }
        public Guid QuestionnaireId { get; set; }
        public EQuestionnaireType QuestionnaireType { get; set; }
        public List<SubmitLessonQuestionModel> Questions { get; set; } = new();
    }

    public class SubmitLessonQuestionModel
    {
        public Guid QuestionId { get; set; }
        public string TextAnswer { get; set; }
        public FileInsertModel FileInfo { get; set; }
    }
}
