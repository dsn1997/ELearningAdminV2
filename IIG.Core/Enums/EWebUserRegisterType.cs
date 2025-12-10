using System.ComponentModel;

namespace IIG.Core.Common.Enums;

public enum EWebUserRegisterType
{
    [Description("Kiểm tra trình độ miễn phí")]
    FreeLevelTest = 1,

    [Description("Tư vấn khóa học")]
    CourseAdvise,

    [Description("Tư vấn chương trình tự học")]
    SeftStudyProgramAdvise,

    [Description("Tư vấn công cụ ôn thi")]
    ExamToolAdvise,

    [Description("Tư vấn học cùng giáo viên")]
    LearnWithTeacherAdvise,
}