using System.ComponentModel;

namespace IIG.Core.Common.Enums;
public enum EReviewStatus
{
    [Description("Ngừng hoạt động")]
    InActive = 0,

    [Description("Đang hoạt động")]
    Active,

    [Description("Top bình luận")]
    TopComment
}
