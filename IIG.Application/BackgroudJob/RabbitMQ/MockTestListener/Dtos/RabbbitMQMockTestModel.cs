using IIG.Core.Common.Enums;
using IIG.Core.Common.Models.Files;
using IIG.Core.Common.Models.SpeakingAndWriting;
using IIG.Core.Providers.MongoDbProvider.Infrastructure;
using IIG.Core.Providers.MongoDbProvider.Models;

namespace IIG.Application.BackgroundJob.Dtos
{
    public static class  RabbitMQMockTestAction
    {
        public const string MockTestKeyCode_StartDoingTest= "MockTestKeyCode_StartDoingTest";
        public const string MockTestKeyCode_SaveAnswer = "MockTestKeyCode_SaveAnswer";
        public const string MockTestKeyCode_MarkQuestion = "MockTestKeyCode_MarkQuestion";
        public const string MockTestKeyCode_Delete = "MockTestKeyCode_Delete";
    }

    public static class RabbitMQLiveClassTestAction
    {
        public const string LiveClassTest_SaveAnswer = "LiveClassTest_SaveAnswer";
        public const string LiveClassTest_MarkQuestion = "LiveClassTest_MarkQuestion";
        public const string LiveClassTest_Delete = "LiveClassTest_Delete";

    }


    public class RabbitMQMockTestActionModel
    {
        public string KeyCode { get; set; }
        public string Action { get; set; } //RabbitMQMockTestAction
        public string Data { get; set; }
    }

    public class RabbitMQLiveClassTestActionModel
    {
        public Guid UserId { get; set; }
        public string Action { get; set; } //RabbitMQLiveClassTestAction
        public string Data { get; set; }
        public string KeyCode { get; set; }
    }

    public class MgUserSubmitLessonQuestion
        : IRecordingFile
    {
        public Guid QuestionId { get; set; }
        public string TextAnswer { get; set; }
        public Guid? RecordingFileId { get; set; }
    }

    public class MgUserSubmitLessonQuestionRequestModel
    {
        
    }
}

