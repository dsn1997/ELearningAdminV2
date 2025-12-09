using System.ComponentModel;

namespace IIG.Core.Common.Enums
{
public enum ESelfStudyStudentsStatus
{
    [Description("Đang học")]
    Studying = 0,

    [Description("Chưa học")]
    NotStarted = 1,

    [Description("Bảo lưu")]
    Reserved = 2,

    [Description("Hết hạn")]
    Expired = 3,

    [Description("Khóa")]
    Locked = 4
}

}