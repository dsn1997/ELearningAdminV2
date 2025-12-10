using IIG.Core.Common.Enums;
using IIG.Web.Data.Models.Practices.Versioning;

namespace IIG.Web.Data.Models.LiveLessonMission;
public class QuestionnaireAndAnswerVersionResponse
{
    public QuestionnaireVersionInfo QuestionnaireInfo { get; set; }

    public IEnumerable<WebUserLessonMissionChooseModel> AnswerSubmittedData { get; set; }
    public ECourseScoringStatus? ScoringStatus { get; set; }
    public int? RedoNumber { get; set; }
}
