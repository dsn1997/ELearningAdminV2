using IIG.Web.Data.Models.Answers;

namespace IIG.Web.Data.Models.LiveClass.WebUserLessonMissionChoose;
public class WebUserLessonMissionChooseInsetModel
{
    public Guid WebUserId { get; set; }
    public Guid LiveLessonMissionId { get; set; }
    public List<ChooseBaseModelResponse> ChooseBaseModelResponse { get; set; }
}
