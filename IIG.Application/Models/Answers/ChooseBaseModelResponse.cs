using Newtonsoft.Json;

namespace IIG.Application.Models;

public  class ChooseBaseModelResponse
{
    public Guid AnswerId { get; set; }

    public Guid QuestionId { get; set; }

    public string AnswerText { get; set; }

    public bool CorrectAnswer { get; set; }

    public Guid? MatchingQuestionId { get; set; }

    [JsonIgnore] 
    public Guid? CorrectAnswerId { get; set; }

    [JsonIgnore] 
    public List<string> CorrectAnswerText { get; set; } = new();
}