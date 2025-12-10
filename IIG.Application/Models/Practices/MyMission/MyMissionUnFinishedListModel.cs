namespace IIG.Application.Models.MyMission;
public class MyMissionUnFinishedListModel
{
    public Guid Id { get; set; }

    public string MissionName { get; set; }

    public string ClassName { get; set; }

    public string ImageUrl { get; set; }

    public string TeacherFullName { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }
}