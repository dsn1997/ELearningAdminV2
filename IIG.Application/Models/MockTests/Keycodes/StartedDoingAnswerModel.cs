namespace IIG.Application.Models.Keycodes;
public class StartedDoingAnswerModel
{
    public string KeyCode { get; set; }
    public DateTime StartedDoingExamDate { get; set; }
    public string ClientIp { get; set; }
    public string Browser { get; set; }
    public int TimeRemaining { get; set; }
    public Guid Cookie { get; set; }
    public string? RegistrationToken { get; set; }
}
