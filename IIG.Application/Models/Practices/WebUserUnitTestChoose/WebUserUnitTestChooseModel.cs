namespace IIG.Application.Models.WebUserUnitTestChoose;
public class WebUserUnitTestChooseModel
{
    public Guid QuestionId { get; set; }

    public Guid AnswerId { get; set; }

    public string AnswerText { get; set; }

    public bool CorrectAnswer { get; set; }

    public Guid? MatchingQuestionId { get; set; }
    public Guid? RecordingFileId { get; set; }
}