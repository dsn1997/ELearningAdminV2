using System.ComponentModel;

namespace IIG.Core.Common.Enums
{
    public enum ECourseScoringType
    {
        //[Description("")]
        CourseTest = 1,
        UnitTest = 2,
        StepTest = 3,
        MockTest = 4
    }

    public enum ELiveClassScoringType
    {
        MissionTest = 1,
        FinalTest = 2
    }

    public enum ECourseScoringStatus
    {
        [Description("Chờ chấm")]
        WaitingScoring = 1,

        [Description("Đang chấm")]
        Scoring = 2,

        [Description("Quá hạn")]
        OutOfDate = 3,

        [Description("Nháp")]
        Draft = 4,

        [Description("Đã chấm")]
        Scored = 5
    }

    public enum EScoringAssigneeType
    {
        Course = 1,
        LiveClass = 2
    }
    public enum ECourseScoringNotificationType
    {
        Step = 1,
        UnitTest,
        CourseTest,
        Mocktest,

    }
    public enum ELiveClassScoringNotificationType
    {
        Mission = 1,
        FinalTest
    }

    public enum EGroupScoringDetailType
    {
        ScoringByQuestion = 1,
        ScoringByUser = 2
    }

    public enum ETestType
    {
        MockTest,
        CourseTest,
        UnitTest,
        StepTest,
        MissionTest
    }
}
