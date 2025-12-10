using IIG.Core.Common.Models.MockTests;

namespace IIG.Application.Models;

public class CourseTestDetailDto
    : IRedoSettingProperty 
{
    public Guid Id { get; set; }
    
    public Guid CourseId { get; set; }
    
    public Guid MockTestId { get; set; }
    
    public string Title { get; set; }
    
    public string Description { get; set; }
    
    public int? SortOrder { get; set; }
    
    public string TagName { get; set; }
    public int? RedoNumber { get; set; }
}