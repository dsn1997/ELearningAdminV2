namespace IIG.Web.Data.Models.LiveLessonMission;
public class LiveLessonMissionSubmitResponse
{
    public Guid Id { get; set; }
    public int TotalQuestions { get; set; }
    public int TotalCorrectAnswer { get; set; }
    public int CorrectPercentage { get; set; }
}
