using IIG.Core.Common.Enums;

namespace IIG.Web.Data.Models.Reviews;
public class ReviewInsertModel
{
    public Guid CourseId { get; set; }
    public Guid? WebUserId { get; set; }
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Content { get; set; }
    public decimal Rate { get; set; }
    public EReviewStatus Status { get; set; }
}
