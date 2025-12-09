using System.ComponentModel;

namespace IIG.Core.Common.Enums;
public enum EBannerType
{
    [Description("Banner homepage")]
    BannerHome = 1,

    [Description("Logo chương trình thi")]
    LogoExamProgram,

    [Description("Banner danh sách tin tức")]
    BannerNews,

    [Description("Banner chi tiết tin tức")]
    BannerNewsDetail,

    [Description("Tổng quan IIG")]
    IIG
}
