using IIG.Core.Common.Models.SpeakingAndWriting;

namespace IIG.Application.Models
{
    public class LiveClassMissionListModel
        : IWatchCount
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ELessonStatusDisplay Status { get; set; }
        public string Score { get; set; }
        public bool IsPreMission { get; set; }
        public int? WatchCount { get; set; }
        public Guid? LiveClassScoringId { get; set; }
    }
}
