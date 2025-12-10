namespace IIG.Web.Data.Models.CourseStatistic;

public class CourseStatisticByCourseIdDto
{
    public int NumberOfQuestions { get; set; }
    public int TotalCompletedAnswers { get; set; }
    public int TotalCorrectAnswers { get; set; }
    public long TimeOnTask { get; set; }
}