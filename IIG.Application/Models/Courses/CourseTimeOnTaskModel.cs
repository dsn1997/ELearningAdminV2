using System.Text.Json.Serialization;

namespace IIG.Application.Models;

public class CourseTimeOnTaskModel
{
    public Guid CourseId { get; set; }

    [JsonIgnore]
    public Guid WebUserId { get; set; }

    public int TimeDuration { get; set; }
}
