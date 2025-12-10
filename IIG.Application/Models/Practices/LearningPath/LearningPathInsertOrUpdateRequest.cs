using IIG.Core.Common.Enums;

namespace IIG.Web.Data.Models.Practices.LearningPath;
public class LearningPathInsertOrUpdateRequest
{
    public Guid CourseId { get; set; }
    public DateTime StartDate { get; set; }
    public EDaysPerWeek DaysPerWeek { get; set; }
}
