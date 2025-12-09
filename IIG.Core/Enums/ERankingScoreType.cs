using System.ComponentModel.DataAnnotations;

namespace IIG.Core.Common.Enums
{
    public enum ERankingScoreType
    {
        [Display(Name = "Số câu đúng")]
        SinglePoint = 1,

        [Display(Name = "Nhóm điểm")]
        GroupPoint = 2,
    }
}
