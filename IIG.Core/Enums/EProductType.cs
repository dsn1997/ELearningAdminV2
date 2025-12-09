using System.ComponentModel;

namespace IIG.Core.Common.Enums
{
    public enum EProductType
    {
        [Description("Khóa tự học")]
        SelfStudyCourse = 1,
        
        [Description("Công cụ ôn thi")]
        ExamTool,
        
        [Description("Học cùng giáo viên")]
        LearnWithTeacher,
        
        [Description("Thi thử online")]
        MockTest
    }
}
