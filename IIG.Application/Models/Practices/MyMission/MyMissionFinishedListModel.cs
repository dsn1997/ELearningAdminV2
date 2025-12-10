using IIG.Core.Common.Enums;
using IIG.Core.Common.Models.SpeakingAndWriting;

namespace IIG.Web.Data.Models.Practices.MyMission;
public class MyMissionFinishedListModel
    : IWatchCount
{
    public Guid Id { get; set; }

    public string MissionName { get; set; }

    public string ClassName { get; set; }

    public string TeacherFullName { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public int CorrectPercentage { get; set; }

    public ECourseScoringStatus? ScoringStatus { get; set; }
    public int? WatchCount { get; set; }
}