namespace IIG.Web.Data.Models.Request.Users;
public class ForgotPasswordRequest
{
    public string Code { get; set; }
    public string Email { get; set; }
    public string NewPassword { get; set; }
    public string ConfirmPassword { get; set; }
}
