namespace IIG.Web.Data.Models.ToeflChallenge
{
    public class ToeflChallengeUserInfoRequest
    {
        public Guid ContestId { get; set; }
        public string ParentName { get; set; }
        public string ParentPhoneNumber { get; set; }
        public string ParentEmail { get; set; }
        public string Name { get; set; }
        public DateTime? Birthday { get; set; }
        public string Gender { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public string Block { get; set; }
        public string School { get; set; }
        public Guid MocktestId { get; set; }
    }
}
