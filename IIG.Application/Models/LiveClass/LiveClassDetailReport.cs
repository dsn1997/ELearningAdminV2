namespace IIG.Web.Data.Models.LiveClass
{
    public class LiveClassDetailReport
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int AttendancePercentage { get; set; }
        public int MissionCompletedPercentage { get; set; }
        public int CorrectPercentage { get; set; }
        public int Progress { get; set; }
        public DateTime? StudentEndDate { get; set; }
    }
}
