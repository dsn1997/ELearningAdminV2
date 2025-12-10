using System.ComponentModel;

namespace IIG.Web.Data.Models.VerificationCode
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
