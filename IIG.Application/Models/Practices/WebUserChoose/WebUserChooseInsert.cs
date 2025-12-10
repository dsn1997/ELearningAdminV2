namespace IIG.Application.Models.WebUserChoose;

public class WebUserChooseInsert
{
    public Guid WebUserId { get; set; }

    public Guid QuestionId { get; set; }

    public Guid AnswerId { get; set; }

    public Guid StepId { get; set; }

    public Guid? MatchingQuestionId { get; set; }

    public string AnswerText { get; set; }

    public bool CorrectAnswer { get; set; }
    public Guid LessonId { get; set; }
    public Guid UnitId { get; set; }
    public Guid CourseId { get; set; }
}