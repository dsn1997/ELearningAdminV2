using IIG.Core.Common.Models.SpeakingAndWriting;

namespace IIG.Web.Data.Models.LiveLessonMission;
public class WebUserLessonMissionChooseModel
{
    public Guid QuestionId { get; set; }

    public Guid AnswerId { get; set; }

    public string AnswerText { get; set; }

    public bool CorrectAnswer { get; set; }

    public Guid? MatchingQuestionId { get; set; }

    public SubmittedQuestionModel SubmittedQuestionModel { get; set; }
}
