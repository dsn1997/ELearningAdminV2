using IIG.Application.Models.Assessment;

namespace IIG.Application.Models;

public class LiveLessonMissionCheckVideoResponse
{
    public Guid LiveLessonMissionId { get; set; }

    public Guid QuestionnaireId { get; set; }

    public List<AnswerChooseResponse> AnswerChooseResponse { get; set; } = new();
}
