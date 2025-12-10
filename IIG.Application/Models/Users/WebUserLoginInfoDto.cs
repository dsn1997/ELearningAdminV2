namespace IIG.Web.Data.Models.Users;

public class WebUserLoginInfoDto
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
    public int AccessFailedCount { get; set; }
}