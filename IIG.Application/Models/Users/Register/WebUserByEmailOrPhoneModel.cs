namespace IIG.Application.Models.Register
{
    public class WebUserByEmailOrPhoneModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool EmailVerified { get; set; }
    }
}
