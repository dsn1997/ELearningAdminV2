namespace IIG.Application.Models;
public class LiveLessonMissionModel
{
    public Guid Id { get; set; }
    public string  Name { get; set; }
    public Guid LiveLessonDetailId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsPreMission { get; set; }
}
