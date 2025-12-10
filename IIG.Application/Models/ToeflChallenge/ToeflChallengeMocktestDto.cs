namespace IIG.Web.Data.Models.ToeflChallenge
{
    public class ToeflChallengeMocktestDto
    {
        public Guid ContestId { get; set; }
        public Guid MocktestId { get; set; }
        public Guid MocktestObjectId { get; set; }
        public string MocktestName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
