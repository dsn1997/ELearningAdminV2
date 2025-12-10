using System.Text.Json.Serialization;

namespace IIG.Web.Data.Models.CoursePrice;

public class CoursePriceBasicDto
{
    public Guid Id { get; set; }
   
    public string Name { get; set; }
   
    public bool? IsKeyCodeType { get; set; }
    
    [JsonIgnore]
    public string CourseName { get; set; }
}