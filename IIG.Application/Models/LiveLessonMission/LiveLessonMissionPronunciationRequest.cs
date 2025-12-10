using Microsoft.AspNetCore.Http;

namespace IIG.Web.Data.Models.LiveLessonMission;

public class LiveLessonMissionPronunciationRequest
{
    public Guid LiveLessonMissionId { get; set; }
    
    public Guid QuestionnaireId { get; set; }

    public Guid QuestionId { get; set; }

    public IFormFile File { get; set; }
}
