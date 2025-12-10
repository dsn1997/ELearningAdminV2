using IIG.Core.Common.Enums;

namespace IIG.Application.Models;

public class WebUserInsertModel
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public bool IsActive { get; set; }
    public string FacebookId { get; set; }
    public string GoogleId { get; set; }
    public bool EmailVerified { get; set; }
    public DateTime? Birthday { get; set; }
    public string Gender { get; set; } 
    public string CurrentAddress { get; set; }
    public string JobName { get; set; }
    public ERegisterType RegisterType { get; set; }
    public string Identifier { get; set; }
}