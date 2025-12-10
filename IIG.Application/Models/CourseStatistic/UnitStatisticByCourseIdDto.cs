namespace IIG.Application.Models;
public class UnitStatisticByCourseIdDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public int TotalQuestions { get; set; }
    public int TotalQuestionsCompleted { get; set; }
    public decimal CompletionPercentage { get; set; }
    public int SortOrder { get; set; }

    public Guid? CourseId { get; set; }
}
