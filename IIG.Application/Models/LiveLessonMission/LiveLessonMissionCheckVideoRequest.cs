using IIG.Application.Models.Assessment;

namespace IIG.Application.Models;

public class LiveLessonMissionCheckVideoRequest
{
    public Guid LiveLessonMissionId { get; set; }

    public Guid QuestionnaireId { get; set; }

    public List<AnswerChooseRequest> AnswerChooseRequest { get; set; }
}
