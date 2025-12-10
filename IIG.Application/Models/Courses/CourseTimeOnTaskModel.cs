using System.Text.Json.Serialization;

namespace IIG.Web.Data.Models.Courses;

public class CourseTimeOnTaskModel
{
    public Guid CourseId { get; set; }

    [JsonIgnore]
    public Guid WebUserId { get; set; }

    public int TimeDuration { get; set; }
}
