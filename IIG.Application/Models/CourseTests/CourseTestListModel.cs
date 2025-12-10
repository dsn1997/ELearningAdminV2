using IIG.Core.Common.Enums;

namespace IIG.Web.Data.Models.CourseTests;
public class CourseTestListModel
{
    public Guid Id { get; set; }
    public Guid CourseId { get; set; }
    public Guid MockTestId { get; set; }
    public string Title { get; set; }
    public int SortOrder { get; set; }
    public string ImageUrl { get; set; }
    public int NumberOfReadingQuestions { get; set; }
    public int NumberOfListeningQuestions { get; set; }
    public int TotalTime { get; set; }
    public bool IsFinished { get; set; }
    public bool CanStart { get; set; }
    public Guid ImageFileId { get; set; }
    public int? RedoNumber { get; set; } = null;
    public bool IsSWType { get; set; } = false;
    public int? WatchCount { get; set; } = null;
    public ECourseScoringStatus? ScoringStatus { get; set; }
    public int NumberOfSpeakingQuestions { get; set; }
    public int NumberOfWritingQuestions { get; set; }
}
