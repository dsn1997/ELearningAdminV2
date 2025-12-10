using IIG.Core.Common.Enums;
using IIG.Application.Models.Facebook;

namespace IIG.Application.Models.Social;

public class SocialDto: TokenDto
{
    public GoogleInfoDto GoogleInfo { get; set; }

    public FacebookInfoDto FacebookInfo { get; set; }

    public EnumRegister Step { get; set; }

    public SocialInfo SocialInfo { get; set; }
    
    public string ImageUrl { get; set; }

    public string FullName { get; set; }
}