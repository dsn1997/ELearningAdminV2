using IIG.Core.Providers.MongoDbProvider.Infrastructure;
using IIG.Core.Providers.MongoDbProvider.Models;

namespace IIG.Core.Common.MongoDataModels.Keycodes
{
    public class MgKeycodeAnswerRequest
    {
        public string Keycode { get; set; }
        public Guid AnswerId { get; set; }
        public Guid QuestionId { get; set; }
        public string AnswerText { get; set; }
        public Guid? MatchingQuestionId { get; set; }
    }
}
