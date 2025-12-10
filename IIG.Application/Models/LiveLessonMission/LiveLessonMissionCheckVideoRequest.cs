using IIG.Web.Data.Models.Practices.Assessment;

namespace IIG.Web.Data.Models.LiveLessonMission;

public class LiveLessonMissionCheckVideoRequest
{
    public Guid LiveLessonMissionId { get; set; }

    public Guid QuestionnaireId { get; set; }

    public List<AnswerChooseRequest> AnswerChooseRequest { get; set; }
}
