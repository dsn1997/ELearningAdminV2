namespace IIG.Web.Data.Models.Practices.MyCourse;
public class AboutMyCourseInfoModel
{
    public Guid Id { get; set; }
    public Guid CourseId { get; set; }
    public string Name { get; set; }
    public Guid? CurrentUnitId { get; set; }
    public string CurrentUnitTitle { get; set; }
    public Guid? CurrentLessonId { get; set; }
    public Guid? CurrentStepId { get; set; }
    public Guid? QuestionnaireId { get; set; }
    public Guid? CurrentQuestionnaireId { get; set; }
    public Guid? QuestionId { get; set; }
    public Guid? CurrentQuestionId { get; set; }
}
