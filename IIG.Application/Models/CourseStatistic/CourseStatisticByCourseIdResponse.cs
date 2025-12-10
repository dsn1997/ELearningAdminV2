namespace IIG.Web.Data.Models.CourseStatistic;
public class CourseStatisticByCourseIdResponse
{
    public decimal CourseCompletion { get; set; }
    public int AnsweredQuestions { get; set; }
    public decimal CorrectPercentage { get; set; }
    public long TimeOnTask { get; set; }
}
