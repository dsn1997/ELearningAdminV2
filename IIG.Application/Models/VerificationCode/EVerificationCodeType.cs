using System.ComponentModel;

namespace IIG.Application.Models
{
    public enum EVerificationCodeType
    {
        Register = 1,
        ForgotPassword = 2
    }

    public enum EVerificationType
    {
        [Description("Email")]
        Email = 1,
        [Description("SMS")]
        Sms
    }
}
