namespace IIG.Web.Data.Models.LiveClass.WebUserLessonMissionResult;
public class WebUserLessonMissionResultInsertModel
{
    public Guid WebUserId { get; set; }

    public Guid LiveLessonMissionId { get; set; }

    public int TotalQuestions { get; set; }

    public int TotalCorrectAnswer { get; set; }

    public string QuestionnaireIds { get; set; }

    public int CorrectPercentage { get; set; }
}
