namespace IIG.Web.Data.Models.Auth;

public class TokenDto
{
    public string Token { get; set; }
    public string AccessToken { get; set; }
    public DateTime ExpiredDate { get; set; }
    public string RefreshToken { get; set; }
    public string ErrorMessage { get; set; } = string.Empty;
    public UserLoginInfo UserInfo { get; set; }
}