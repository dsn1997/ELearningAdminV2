using System.ComponentModel.DataAnnotations;

namespace IIG.Core.Common.Enums
{
    public enum EMarkbyAIEntityType
    {
        [Display(Name = "Nhiệm vụ trước buổi học")]
        PreLiveLessonMission = 1,

        [Display(Name = "Nhiệm vụ sau buổi học")]
        AfterLiveLessonMission = 2,

        [Display(Name = "Câu hỏi questionnaire Live lesson")]
        LiveLessonMissionQuestionnaire = 3,

        [Display(Name = "Detail test buổi học")]
        LiveLessonDetailTest = 4,

        [Display(Name = "Lesson - Unit course")]
        Lesson = 5,

        [Display(Name = "Step")]
        Step = 6,

        [Display(Name = "Câu hỏi questionnaire màn Step")]
        StepQuestionnaire = 7,

        [Display(Name = "Unit test")]
        UnitTest = 8,

        [Display(Name = "Câu hỏi questionnaire màn Unit test")]
        UnitTestQuestionnaire = 9,

        [Display(Name = "Course test cuối khóa")]
        CourseTest = 10,
    }
}
