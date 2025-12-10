namespace IIG.Web.Data.Models.LiveClassTest;
public class LiveClassTestModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid LiveClassDetailId { get; set; }
    public Guid MocktestId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}

public class LiveClassTestQuestionType
{
    public short Type { get; set; }
    public bool? QuestionnaireAI { get; set; }
    public bool? TestAI { get; set; }

}