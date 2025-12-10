using System.Text.Json.Serialization;

namespace IIG.Web.Data.Models.LiveLessonMission;
public class LiveLessonMissionInfoModel
{
    public string Name { get; set; }
    public int TotalQuestions { get; set; }
    public bool IsSubmitted { get; set; }
    public DateTime? SubmittedDate { get; set; }
    public string QuestionnaireIds { get; set; }

    [JsonIgnore]
    public string LiveClassDetailName { get; set; }
}
