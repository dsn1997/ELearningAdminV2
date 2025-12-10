using IIG.Web.Data.Models.Practices.Assessment;

namespace IIG.Web.Data.Models.LiveLessonMission;

public class LiveLessonMissionCheckVideoResponse
{
    public Guid LiveLessonMissionId { get; set; }

    public Guid QuestionnaireId { get; set; }

    public List<AnswerChooseResponse> AnswerChooseResponse { get; set; } = new();
}
