namespace IIG.Application.Models.Social;

public class SocialRegisterRequest
{
    public string Email { get; set; }

    public string Password { get; set; }

    public string GoogleId { get; set; }

    public string FacebookId { get; set; }
}