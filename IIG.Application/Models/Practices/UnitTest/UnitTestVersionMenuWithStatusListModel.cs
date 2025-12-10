using IIG.Core.Common.Enums;

namespace IIG.Application.Models.UnitTest;
public class UnitTestVersionMenuWithStatusListModel
{
    public Guid Id { get; set; }

    public Guid QuestionnaireId { get; set; }

    public Guid QuestionId { get; set; }

    public EQuestionnaireType QuestionnaireType { get; set; }

    public EAnswerStatus Status { get; set; }

    public int SortOrder { get; set; }

    public int QuestionSortOrder { get; set; }
}
