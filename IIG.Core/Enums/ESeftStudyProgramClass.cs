using System.ComponentModel;

namespace IIG.Core.Common.Enums;

public enum ESeftStudyProgramClassType : short
{
    [Description("Phân loại")]
    PhanLoai = 0,

    [Description("Combo")]
    Combo,

    [Description("Combo item")]
    ComboItem
}

public enum ESeftStudyProgramClassStatus : short
{
    [Description("Không hoạt động")]
    UnActive =0,

    [Description("Đang hoạt động")]
    Active,

}