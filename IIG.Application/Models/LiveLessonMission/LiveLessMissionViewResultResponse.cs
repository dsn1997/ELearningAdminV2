using IIG.Core.Common.Enums;
using IIG.Core.Common.Models.SpeakingAndWriting;

namespace IIG.Web.Data.Models.LiveLessonMission;
public class QuestionViewMission
{
    public Guid Id { get; set; }
    public int SortOrder { get; set; }
   
}

public class QuestionnaireViewMission
{
    public Guid QuestionnaireId { get; set; }
    public List<QuestionViewMission> Questions { get; set; }
}
public class LiveLessMissionViewResultResponse
    : IWatchCount
{
    public string LiveClassDetailName { get; set; }
    public string LiveLessonMissionName { get; set; }
    public List<Guid> QuestionnaireIds { get; set; }    
    public List<QuestionnaireViewMission> ListQuestion { get; set; }    
    public ECourseScoringStatus? ScoringStatus { get; set; }
    public Guid? ScoringId { get; set; }
    public int? WatchCount { get; set; }
}
