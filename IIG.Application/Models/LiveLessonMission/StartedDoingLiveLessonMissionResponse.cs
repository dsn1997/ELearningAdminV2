namespace IIG.Web.Data.Models.LiveLessonMission;
public class StartedDoingLiveLessonMissionResponse
{
    public string LiveClassDetailName { get; set; }

    public string LiveLessonMissionName { get; set; }

    public List<Guid> QuestionnaireIds { get; set; }

    public List<QuestionnaireViewMission> ListQuestion { get; set; }
    public int TotalQuestions { get; set; }

    public Boolean isSWType { get; set; }

}
