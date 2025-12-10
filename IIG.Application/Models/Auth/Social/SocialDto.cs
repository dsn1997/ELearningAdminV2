using IIG.Core.Common.Enums;
using IIG.Web.Data.Models.Auth.Facebook;

namespace IIG.Web.Data.Models.Auth.Social;

public class SocialDto: TokenDto
{
    public GoogleInfoDto GoogleInfo { get; set; }

    public FacebookInfoDto FacebookInfo { get; set; }

    public EnumRegister Step { get; set; }

    public SocialInfo SocialInfo { get; set; }
    
    public string ImageUrl { get; set; }

    public string FullName { get; set; }
}