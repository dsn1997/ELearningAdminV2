using IIG.Core.Common.MongoDataModels;


namespace IIG.Application.Services.Redis;

public interface IQuestionaireRedisDataService
{
    Task<MgQuestionnaireModel> GetQuestionaire(Guid id);
    Task<IEnumerable<MgQuestionModel>> GetQuestions(Guid questionaireId);
    Task<IEnumerable<MgLeftSectionModel>> GetLeftSections(Guid questionaireId);
    Task<List<Guid>> GetListIdsByQuestionnaireIdAsync(Guid questionaireId);
}