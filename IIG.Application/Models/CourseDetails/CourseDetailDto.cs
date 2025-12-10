namespace IIG.Web.Data.Models.CourseDetails;

public class CourseDetailDto
{
    public Guid Id { get; set; }
   
    public Guid CourseId { get; set; }
   
    public string TabName { get; set; }
   
    public string Title { get; set; }
  
    public int SortOrder { get; set; }
   
    public string Content { get; set; }
}
