namespace IIG.Web.Data.Models.LiveClassBooking;
public class LiveClassBookingInsertModel
{
    public Guid Id { get; set; }
    public Guid ClassTypeId { get; set; }
    public Guid WebUserId { get; set; }
    public Guid CoursePriceId { get; set; }
    public string CoursePriceName { get; set; }
    public Guid CourseId { get; set; }
    public Guid OrderId { get; set; }
    public string CourseCode { get; set; }
    public string Name { get; set; }
}
