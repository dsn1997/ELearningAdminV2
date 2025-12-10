using IIG.Core.Common.Enums;
using IIG.Core.Common.Models.SpeakingAndWriting;

namespace IIG.Web.Data.Models.LiveClass
{
    public class LiveClassTimeTableModel
        : IWatchCount
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int? NumberLesson { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ELessonStatusDisplay Status { get; set; }
        public string Score { get; set; }
        public bool IsMockTest { get; set; } = false;
        public bool? HasMission { get; set; }
        public int? WatchCount { get; set; }
        public Guid? LiveClassScoringId { get; set; }
        public ECourseScoringStatus? ScoringStatus { get; set; }
        public bool IsSWType { get; set; } = false;
    }
}
