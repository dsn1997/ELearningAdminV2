namespace IIG.Application.Models.ExamTool;
public class MyExamToolListModel
{
    public Guid CourseId { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public bool IsMockTestType { get; set; }
    
     public int? Status { get; set; }
     public DateTime? PreserveStart { get; set; }
     public DateTime? PreserveEnd { get; set; }


}
