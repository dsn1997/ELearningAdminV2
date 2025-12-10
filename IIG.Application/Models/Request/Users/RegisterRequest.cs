using IIG.Web.Data.Models.VerificationCode;

namespace IIG.Web.Data.Models.Request.Users;

public class RegisterUserRequest
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}