using IIG.Core.Common.Enums;

namespace IIG.Application.Models.MockTestPartQuestionnaire;

public class MockTestPartQuestionnaireAssignedListModel
{
    public Guid Id { get; set; }

    public string Identifier { get; set; }

    public string Name { get; set; }

    public EQuestionnaireType Type { get; set; }

    public int NumberOfQuestions { get; set; }

    public int NumberOfTime { get; set; }

    public bool IsActive { get; set; }
    public TimeSpan? TotalTime { get; set; }
    public bool? IsMarkByAI { get; set; }
    public int SortOrder { get; set; }
}