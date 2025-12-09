using Microsoft.Extensions.DependencyInjection;

namespace IIG.Core.Providers.MongoDbProvider.Indexs
{
    public static class MongoCreateIndex
    {
        public static async Task EnsureIndexs(IServiceProvider serviceProvider)
        {
            await EnsureLeftSectionIndexs(serviceProvider);
            await EnsureQuestionIndexs(serviceProvider);
            await EnsureQuestionnaireIndexs(serviceProvider);
            await EnsureMockTestIndexs(serviceProvider);
            await EnsureMockTestKeyCodeIndexs(serviceProvider);
            await EnsureKeyCodeAnswerIndexs(serviceProvider);
            await EnsureCourseTestChooseIndexs(serviceProvider);
            await EnsureLiveClassTestChooseIndexs(serviceProvider);
        }

        private static async Task EnsureLeftSectionIndexs(IServiceProvider serviceProvider)
        {
            //var _mongoLeftSectionService = serviceProvider.GetRequiredService<IMongoLeftSectionService>();

            //await _mongoLeftSectionService.CreateIndexAsync(x => x.QuestionnaireId);
            //await _mongoLeftSectionService.CreateIndexAsync(x => x.LeftSectionId);
        }

        private static async Task EnsureQuestionIndexs(IServiceProvider serviceProvider)
        {
            //var _mongoQuestionService = serviceProvider.GetRequiredService<IMongoQuestionService>();

            //await _mongoQuestionService.CreateIndexAsync(x => x.QuestionnaireId);
            //await _mongoQuestionService.CreateIndexAsync(x => x.QuestionId);
        }

        private static async Task EnsureQuestionnaireIndexs(IServiceProvider serviceProvider)
        {
            //var _mongoQuestionnaireService = serviceProvider.GetRequiredService<IMongoQuestionnaireService>();
            //await _mongoQuestionnaireService.CreateIndexAsync(x => x.QuestionnaireId);
        }

        private static async Task EnsureMockTestIndexs(IServiceProvider serviceProvider)
        {
            //var _mongoMockTestService = serviceProvider.GetRequiredService<IMongoMockTestService>();
            //await _mongoMockTestService.CreateIndexAsync(x => x.MockTestId);
        }

        private static async Task EnsureMockTestKeyCodeIndexs(IServiceProvider serviceProvider)
        {
            //var _mongoMockTestKeyCodeService = serviceProvider.GetService<IMongoMockTestKeyCodeService>();
            //await _mongoMockTestKeyCodeService.CreateIndexAsync(x => x.KeyCode);
        }

        private static async Task EnsureKeyCodeAnswerIndexs(IServiceProvider serviceProvider)
        {
            //var _mongoKeyCodeChooseService = serviceProvider.GetService<IMongoKeyCodeAnswerService>();
            //await _mongoKeyCodeChooseService.CreateIndexAsync(x => x.KeyCode);
            //await _mongoKeyCodeChooseService.CreateIndexAsync(x => x.MockTestPartId);
            //await _mongoKeyCodeChooseService.CreateIndexAsync(x => x.MockTestSectionId);
            //await _mongoKeyCodeChooseService.CreateIndexAsync(x => x.QuestionnaireId);
            //await _mongoKeyCodeChooseService.CreateIndexAsync(x => x.QuestionId);
        }
        
        private static async Task EnsureCourseTestChooseIndexs(IServiceProvider serviceProvider)
        {
            //var _mongoCourseTestAnswerService = serviceProvider.GetService<IMongoCourseTestAnswerService>();
            //await _mongoCourseTestAnswerService.CreateIndexAsync(x => x.WebUserId);
            //await _mongoCourseTestAnswerService.CreateIndexAsync(x => x.AnswerId);
            //await _mongoCourseTestAnswerService.CreateIndexAsync(x => x.CourseTestId);
            //await _mongoCourseTestAnswerService.CreateIndexAsync(x => x.MockTestPartId);
            //await _mongoCourseTestAnswerService.CreateIndexAsync(x => x.QuestionId);
        }

        private static async Task EnsureLiveClassTestChooseIndexs(IServiceProvider serviceProvider)
        {
            //var _mongoLiveClassTestAnswerService = serviceProvider.GetService<IMongoLiveClassTestAnswerService>();
            //await _mongoLiveClassTestAnswerService.CreateIndexAsync(x => x.WebUserId);
            //await _mongoLiveClassTestAnswerService.CreateIndexAsync(x => x.AnswerId);
            //await _mongoLiveClassTestAnswerService.CreateIndexAsync(x => x.LiveClassTestId);
            //await _mongoLiveClassTestAnswerService.CreateIndexAsync(x => x.MockTestPartId);
            //await _mongoLiveClassTestAnswerService.CreateIndexAsync(x => x.QuestionId);
        }
    }
}
