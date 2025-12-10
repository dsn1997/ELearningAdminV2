namespace IIG.Application.Models.Users;
public class ForgotPasswordRequest
{
    public string Code { get; set; }
    public string Email { get; set; }
    public string NewPassword { get; set; }
    public string ConfirmPassword { get; set; }
}
