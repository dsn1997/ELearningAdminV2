namespace IIG.Web.Data.Models.Practices.StepQuestionnaires
{
    public class StepQuestionnaireDto
    {
        public Guid QuestionnaireId { get; set; }
        public Guid StepId { get; set; }
        public Guid CourseId { get; set; }
        public int SortOrder { get; set; }
    }
}
