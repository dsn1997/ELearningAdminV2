using IIG.Core.Common.Enums;

namespace IIG.Application.Models;

public class ToeflChallengeKeycodeVerifyDto
{
    public string Keycode { get; set; }
    public Guid MockTestTypeId { get; set; }
    public Guid MockTestObjectId { get; set; }
}