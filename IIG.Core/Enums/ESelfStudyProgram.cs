using System.ComponentModel;

namespace IIG.Core.Common.Enums
{
    public enum ESeftStudyProgramStatus
    {
        [Description("Không hoạt động")]
        UnActive = 0,

        [Description("Đang hoạt động")]
        Active,
    }

    public enum ESeftStudyProgramType:short
    {
        [Description("Chương trình tự học")]
        ChuongTrinhTuHoc = 1,
        [Description("Học cùng giáo viên")]
        HocCungGiaoVien,
        [Description("Công cụ ôn thi")]
        CongCuOnThi,
    }

}