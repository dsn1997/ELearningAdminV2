namespace IIG.Web.Data.Models.Auth.Social;

public class UserBySocialIdDto
{
    public Guid Id { get; set; }
        
    public string Email { get; set; }

    public bool IsActive { get; set; }
}