using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IIG.Core.Common.Enums
{
    public enum EUserSubmitLessonStatus
    {
        [Display(Name = "Chờ chấm")]
        WaitingEvaluate = 1,

        [Display(Name = "Đã chấm")]
        Evaluated = 2,
    }
}
