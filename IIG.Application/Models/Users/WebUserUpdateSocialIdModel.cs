namespace IIG.Web.Data.Models.Users;

public class WebUserUpdateSocialIdModel
{
    public Guid Id { get; set; }
    public string FacebookId { get; set; }
    public string GoogleId { get; set; }
}