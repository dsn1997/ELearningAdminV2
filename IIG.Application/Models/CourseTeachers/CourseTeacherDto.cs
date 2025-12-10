namespace IIG.Web.Data.Models.CourseTeachers;

public class CourseTeacherDto
{
    public Guid Id { get; set; }
  
    public Guid CourseGuid { get; set; }
  
    public string Name { get; set; }
  
    public string University { get; set; }
   
    public string SortOrder { get; set; }
   
    public string Description { get; set; }
   
    public string ImageUrl { get; set; }
}
