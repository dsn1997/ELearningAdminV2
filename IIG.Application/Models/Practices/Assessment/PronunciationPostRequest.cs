using Microsoft.AspNetCore.Http;

namespace IIG.Application.Models.Assessment;
public class PronunciationPostRequest
{
    public Guid StepId { get; set; }

    public Guid QuestionnaireId { get; set; }

    public Guid QuestionId { get; set; }

    public IFormFile File { get; set; }
}
