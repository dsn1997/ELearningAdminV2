using IIG.Application.Models;

namespace IIG.Application.Models.WebUserLessonMissionChoose;
public class WebUserLessonMissionChooseInsetModel
{
    public Guid WebUserId { get; set; }
    public Guid LiveLessonMissionId { get; set; }
    public List<ChooseBaseModelResponse> ChooseBaseModelResponse { get; set; }
}
