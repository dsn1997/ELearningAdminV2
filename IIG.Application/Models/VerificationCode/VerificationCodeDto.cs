namespace IIG.Web.Data.Models.VerificationCode
{
    public class VerificationCodeDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Code { get; set; }
        public string Email { get; set; }
        public DateTime ExpiresAtTime { get; set; }
        public bool Used { get; set; }
    }
}
