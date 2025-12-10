using IIG.Core.Common.Enums;
using IIG.Core.Common.Models.SpeakingAndWriting;
using IIG.Web.Data.Models.MockTests.MockTestSection;

namespace IIG.Web.Data.Models.CourseTests
{

    public class CourseTestResponseCheckDto
    {
        public List<CourseTestSection> Sections { get; set; }
    }

    public class CourseTestSection
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int SortOrder { get; set; }
        public int? NumberOfTime { get; set; }
        public int? NumberOfTimeEstimate { get; set; }
        public EMockTestSectionType Type { get; set; }
        public List<CourseTestPart> Parts { get; set; }
    }

    public class ResponseCheckQuestionModel : IRecordingFile
    {
        public Guid Id { get; set; }
        public Guid MocktestSectionId { get; set; }
        public Guid MocktestPartId { get; set; }
        public Guid QuestionnaireId { get; set; }
        public string AnswerText { get; set; }
        public Guid? RecordingFileId { get; set; }
        public int? SortOrder { get; set; }
        public TimeSpan? RecordingTime { get; set; }
        public int No { get; set; }
        public string RecordingFileUrl { get; set; }
        public int? QuestionnaireSortOrder { get; set; }
    }

    public class CourseTestPart
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public object Content { get; set; }
        public int SortOrder { get; set; }
        public int DelayTimeEachQuestion { get; set; }
        public List<CourseTestQuestionnaire> Questionnaires { get; set; }
        public Guid? IntroAudioFileId { get; set; }
    }

    public class CourseTestQuestionnaire
    {
        public Guid Id { get; set; }
        public int SortOrder { get; set; }
        public List<CourseTestQuestion> Questions { get; set; }
    }

    public class CourseTestQuestion
    {
        public Guid Id { get; set; }
        public int? SortOrder { get; set; }
        public bool? Marked { get; set; }
        public EAnswerStatus? Status { get; set; }
    }

    public class ResponseCheckMocktestModel
    {
        public Guid Id { get; set; }
        public string MocktestName { get; set; }
        public Guid MocktestSectionId { get; set; }
        public EMockTestSectionType MocktestType { get; set; }
        public int SortOrder { get; set; }
        public List<ResponseCheckQuestionModel> Questions { get; set; }
        public string TestName { get; set; }
    }

    public class CourseTestResponseCheckModel
    {
        public List<MockTestSectionDto> Sections { get; set; }

    }

}
