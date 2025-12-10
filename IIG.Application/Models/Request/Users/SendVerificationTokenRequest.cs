using IIG.Application.Models;

namespace IIG.Application.Models.Users;
public class SendVerificationTokenRequest
{
    public string Email { get; set; }
}

public class ForgotPasswordSendOTP
{
    public string EmailOrPhoneNumer { get; set; }
}

public class ResendOTPDto
{
    /// <summary>
    /// Mobile number or email
    /// </summary>
    public string UserName { get; set; }
    public EVerificationCodeType Type { get; set; }
    public EVerificationType Kind { get; set; } = EVerificationType.Email;
}

public class ResendOTPModel
{
    public string NewOTP { get; set; }
    public int OTPRemainCount { get; set; }
    public bool IsAvailable { get; set; }
}

public class SendOtpDto
{
    /// <summary>
    /// Mobile number or email
    /// </summary>
    public string Data { get; set; }
    public EVerificationCodeType Type { get; set; }
    public EVerificationType Kind { get; set; }
}
