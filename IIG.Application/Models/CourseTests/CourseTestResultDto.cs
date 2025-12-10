using IIG.Core.Common.Enums;
using IIG.Core.Common.Models.SpeakingAndWriting;
using Newtonsoft.Json;

namespace IIG.Web.Data.Models.CourseTests;

public class CourseTestResultDto
    : IWatchCount
{
    public string Name { get; set; }
   
    public string Score { get; set; }
   
    public int MinScore { get; set; }
   
    public int MaxScore { get; set; }

    public ECourseScoringStatus? ScoringStatus { get; set; }
    public Guid? ScoringId { get; set; }
    public int? WatchCount { get; set; }
    public EMockTestSectionType SectionType { get; set; }
    public int? MaxRankingScore { get; set;}
    public int? MinRankingScore { get; set;}
    public int? RedoNumber { get; set; }
    public string Comment { get; set; }
}