namespace IIG.Web.Data.Models.Practices.Dictionary;
public class MyDictionaryInsertRequest
{
    public Guid StepId { get; set; }

    public Guid QuestionnaireId { get; set; }

    public Guid QuestionId { get; set; }
}
