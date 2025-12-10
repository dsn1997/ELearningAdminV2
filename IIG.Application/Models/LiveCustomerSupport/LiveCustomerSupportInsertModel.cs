namespace IIG.Web.Data.Models.LiveCustomerSupport;

public class LiveCustomerSupportInsertModel
{
    public Guid CourseId { get; set; }

    public Guid CoursePriceId { get; set; }

    public string FullName { get; set; }

    public string PhoneNumber { get; set; }

    public string Email { get; set; }
}