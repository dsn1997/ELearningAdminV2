using IIG.Core.Common.Enums;
using IIG.Core.Common.Models.Files;
using IIG.Core.Common.Models.SpeakingAndWriting;
using IIG.Core.Providers.MongoDbProvider.Infrastructure;
using IIG.Core.Providers.MongoDbProvider.Models;

namespace IIG.Application.BackgroundJob.Dtos
{
    public static class  RabbitMQMockTestWrapperKeyCodeAction
    {
        public const string StartDoingTest= "StartDoingTest";
        public const string SaveAnswer = "SaveAnswer";
        public const string MarkQuestion = "MarkQuestion";
        public const string Delete = "Delete";
    }

    public class RabbitMQMockTestWrapperKeyCodeActionModel
    {
        public string KeyCode { get; set; }
        public string Action { get; set; } //RabbitMQMockTestAction
        public string Data { get; set; }
    }

}

