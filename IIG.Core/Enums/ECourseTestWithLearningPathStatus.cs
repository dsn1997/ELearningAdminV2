using System.ComponentModel;

namespace IIG.Core.Common.Enums;

public enum ECourseTestWithLearningPathStatus
{
    [Description("Chưa thi")] 
    NotTested = 1,

    [Description("Đã thi")] 
    Tested,
}