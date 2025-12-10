using IIG.Core.Common.Enums;

namespace IIG.Web.Data.Models.ToeflChallenge;

public class ToeflChallengeKeycodeVerifyDto
{
    public string Keycode { get; set; }
    public Guid MockTestTypeId { get; set; }
    public Guid MockTestObjectId { get; set; }
}