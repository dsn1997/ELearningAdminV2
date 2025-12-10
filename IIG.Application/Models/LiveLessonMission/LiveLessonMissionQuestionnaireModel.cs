using IIG.Core.Common.Enums;

namespace IIG.Application.Models;
public class LiveLessonMissionQuestionnaireModel
{
    public Guid LiveLessonMissionId { get; set; }
    public Guid QuestionnaireId { get; set; }
    public Guid LiveLessonDetailId { get; set; }
    public int SortOrder { get; set; }
    public EQuestionnaireType QuestionnaireType { get; set; }
}
