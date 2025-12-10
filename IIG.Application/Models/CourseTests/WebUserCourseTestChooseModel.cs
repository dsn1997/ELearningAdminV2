using IIG.Core.Common.Models.SpeakingAndWriting;

namespace IIG.Application.Models;

public class WebUserCourseTestChooseModel
    : IRecordingFile
{
    public Guid? AnswerId { get; set; }

    public Guid CourseTestId { get; set; }

    public Guid MockTestPartId { get; set; }

    public Guid QuestionId { get; set; }

    public string AnswerText { get; set; }

    public Guid? MatchingQuestionId { get; set; }
   
    public bool CorrectAnswer { get; set; }
    public Guid? RecordingFileId { get; set; }
}