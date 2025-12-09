using System.ComponentModel;

namespace IIG.Core.Common.Enums;

public enum EMockTestKeyCodeType
{
    [Description("Toefl Challenge")]
    ToeflChallenge = 1,

    [Description("Khóa đào tạo")]
    TrainingCourse,

    [Description("Tự do")]
    FreeStyle
}