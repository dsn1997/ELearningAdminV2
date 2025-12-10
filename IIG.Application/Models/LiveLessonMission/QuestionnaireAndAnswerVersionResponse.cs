using IIG.Core.Common.Enums;
using IIG.Application.Models.Versioning;

namespace IIG.Application.Models;
public class QuestionnaireAndAnswerVersionResponse
{
    public QuestionnaireVersionInfo QuestionnaireInfo { get; set; }

    public IEnumerable<WebUserLessonMissionChooseModel> AnswerSubmittedData { get; set; }
    public ECourseScoringStatus? ScoringStatus { get; set; }
    public int? RedoNumber { get; set; }
}
