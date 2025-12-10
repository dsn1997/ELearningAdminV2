using IIG.Core.Common.Enums;
using IIG.Core.Common.Models;

namespace IIG.Application.Models;
public class VerificationCodeModel : BaseEntity
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Code { get; set; }
    public EServiceProvider ServiceProvider { get; set; }
    public string Email { get; set; }
    public DateTime ExpiresAtTime { get; set; }
    public bool Used { get; set; }
    public EVerificationCodeType Type { get; set; }
}
