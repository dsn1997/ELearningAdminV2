using System.ComponentModel;

namespace IIG.Core.Common.Enums;
public enum ELiveLessonMissionType
{
    [Description("Nhiệm vụ sau buổi học")]
    AfterLesson = 0,

    [Description("Nhiệm vụ trước buổi học")]
    BeforeLesson = 1,
}
