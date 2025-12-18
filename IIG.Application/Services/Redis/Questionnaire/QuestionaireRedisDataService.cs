using AutoMapper;
using IIG.Application.Data;
using IIG.Core.Common.ConfigureModels;
using IIG.Core.Common.Models.SpeakingAndWriting;
using IIG.Core.Common.MongoDataModels;
using IIG.Core.Providers.Interfaces;
using IIG.Core.Providers.MongoDbProvider.Infrastructure;
using LinqKit;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;



namespace IIG.Application.Services;

public partial class QuestionaireRedisDataService : IQuestionaireRedisDataService
{
    private readonly ILogger<QuestionaireRedisDataService> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMongoGenericRepository<MgQuestionnaireModel> _mongoQuestionnaireRepos;
    private readonly IMongoGenericRepository<MgLeftSectionModel> _mongoLeftSectionRepos;
    private readonly IMongoGenericRepository<MgQuestionModel> _mongoQuestionRepos;
    private readonly IMapper _mapper;
    private readonly IFileService _fileService;
    private readonly IQuestionnaireDA _questionnaireDA;
    private readonly IQuestionDA _questionDA;
    private readonly ILeftSectionDA _leftSectionDA;
    private readonly IAnswerDA _answerDA;
    private const string StringSplitCharacter = ";";

    private readonly IRedisGenericFactory _redisGenericFactory;

    public QuestionaireRedisDataService(
        ILogger<QuestionaireRedisDataService> logger,
        IHttpContextAccessor httpContextAccessor,
        IMongoGenericRepository<MgQuestionnaireModel> mongoQuestionnaireRepos,
        IMongoGenericRepository<MgLeftSectionModel> mongoLeftSectionRepos,
        IMongoGenericRepository<MgQuestionModel> mongoQuestionRepos,
        IMapper mapper,
        IFileService fileService,
        IQuestionnaireDA questionnaireDA,
        IQuestionDA questionDA,
        ILeftSectionDA leftSectionDA,
        IAnswerDA answerDA,
        IRedisGenericFactory redisGenericFactory
        )
    {
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
        _mongoQuestionnaireRepos = mongoQuestionnaireService;
        _mongoLeftSectionRepos = mongoLeftSectionService;
        _mongoQuestionRepos = mongoQuestionService;
        _mapper = mapper;
        _fileService = fileService;
        _questionnaireDA = questionnaireDA;
        _questionDA = questionDA;
        _leftSectionDA = leftSectionDA;
        _answerDA = answerDA;
        _redisGenericFactory = redisGenericFactory;
    }

    public async Task<MgQuestionnaireModel> GetQuestionaire(Guid id)
    {
        var redisService = _redisGenericFactory.Create<MgQuestionnaireModel>();
        var redisKey = $"{id}";
        var data = await redisService.GetOrCreateWithDistributedLockAsync(
             $"{redisKey}",
             async () =>
             {
                 await DumpAsync(id);
                 var questionnaire = await _mongoQuestionnaireRepos.FindByIdAsync(x => x.QuestionnaireId == id);
                 return questionnaire;
             },
             3,
             0,
             TimeSpan.FromSeconds(10),
             (int)TimeSpan.FromMinutes(15).TotalSeconds
         );
        return data;
    }

    public async Task<IEnumerable<MgQuestionModel>> GetQuestions(Guid questionaireId)
    {
        var redisService = _redisGenericFactory.CreateCollection<MgQuestionModel>();
        var listData = await redisService.Get(questionaireId.ToString());

        if (listData == null)
        {
            listData = await _mongoQuestionRepos.FilterAsync(x => x.QuestionnaireId == questionaireId);
            await redisService.Add(questionaireId.ToString(), listData, (int)TimeSpan.FromMinutes(5).TotalSeconds);
        }

        return listData;
    }

    public async Task<IEnumerable<MgLeftSectionModel>> GetLeftSections(Guid questionaireId)
    {
        var redisService = _redisGenericFactory.CreateCollection<MgLeftSectionModel>();
        var listData = await redisService.Get(questionaireId.ToString());

        if (listData == null)
        {
            listData = await _mongoLeftSectionRepos.FilterAsync(x => x.QuestionnaireId == questionaireId);
            await redisService.Add(questionaireId.ToString(), listData, (int)TimeSpan.FromMinutes(5).TotalSeconds);
        }

        return listData;
    }

    public async Task<List<Guid>> GetListIdsByQuestionnaireIdAsync(Guid questionaireId)
    {
        var redisService = _redisGenericFactory.CreateCollection<RedisDto<Guid>>();
        redisService.ChangeKeyPrefix("GetListIdsByQuestionnaireId");
        List<Guid> listData = new List<Guid>();
        var redisData = await redisService.Get(questionaireId.ToString());

        if (redisData == null)
        {
            listData = await _leftSectionDA.GetListIdsByQuestionnaireIdAsync(questionaireId);
            await redisService.Add(questionaireId.ToString(), listData.Select(p => new RedisDto<Guid> { Data = p }), (int)TimeSpan.FromMinutes(5).TotalSeconds);
        }
        else
        {
            listData = redisData.Select(x => x.Data).ToList();
        }

        return listData;
    }


    public async Task DumpAsync(Guid id)
    {
        await HandleQuestionnaireAsync(id);
    }

    private async Task HandleQuestionnaireAsync(Guid questionnaireId)
    {
        var quetionnaire = await _questionnaireDA

            .GetByIdAsync(questionnaireId);
        if (quetionnaire == null || !quetionnaire.IsActive) return;

        var result = _mapper.Map<MgQuestionnaireModel>(quetionnaire);
        if (result.SubtitleFileId.HasValue)
        {
            result.SubtitleInfo = await _fileService.GetDtoByIdAsync(result.SubtitleFileId.Value);
        }

        if (result.SlideFileId.HasValue)
        {
            result.SlideInfo = await _fileService.GetDtoByIdAsync(result.SlideFileId.Value);
        }

        await _mongoQuestionnaireRepos.DeleteManyAsync(x => x.QuestionnaireId, questionnaireId);

        await _mongoQuestionnaireRepos.InsertOneAsync(result);


        await HandleLeftSectionAsync(questionnaireId);

        await HandleQuestionAsync(questionnaireId);
    }

    public async Task HandleLeftSectionAsync(Guid questionnaireId)
    {
        var quetionnaire = await _questionnaireDA

            .GetByIdAsync(questionnaireId);
        if (quetionnaire == null || !quetionnaire.IsActive) return;

        var leftSections = await _leftSectionDA

            .GetListByQuestionnaierIdAsync(questionnaireId);

        var result = _mapper.Map<IEnumerable<MgLeftSectionModel>>(leftSections);
        foreach (var item in result)
        {
            if (item.ImageFileId.HasValue)
            {
                item.ImageInfo = await _fileService.GetDtoByIdAsync(item.ImageFileId.Value);
            }
        }
        ;

        await _mongoLeftSectionRepos.DeleteManyAsync(x => x.QuestionnaireId, questionnaireId);
        await _mongoLeftSectionRepos.InsertManyAsync(result);
    }

    public async Task HandleQuestionAsync(Guid questionnaireId)
    {
        var questionnaire = await _questionnaireDA

            .GetByIdAsync(questionnaireId);
        if (questionnaire == null || !questionnaire.IsActive) return;

        var questions = await _questionDA

            .GetListQuestionByQuestionnaireIdAsync(questionnaireId);
        if (questions == null || !questions.Any()) return;

        var questionTranslations = await _questionDA

            .GetListTranslationByListQuestionIdAsync(questions.Select(x => x.QuestionId));
        var answers = await _answerDA

           .GetListAnswerByListQuestionIdAsync(questions.Select(x => x.QuestionId));

        var questionOrdered = questions.OrderBy(x => x.SortOrder).ToList();

        var questionMaps = _mapper.Map<IEnumerable<MgQuestionModel>>(questionOrdered);
        var answerMaps = _mapper.Map<IEnumerable<MgAnswerDto>>(answers);
        var questionTranslationsMaps = _mapper.Map<IEnumerable<MgQuestionTranslationDto>>(questionTranslations);

        var grpAnswers = answerMaps?.GroupBy(x => x.QuestionId);
        var grpQuestions = questionTranslationsMaps?.GroupBy(x => x.QuestionId);

        foreach (var question in questionMaps)
        {
            var questionMatchingHasImageFile = question?.AnswerQuestionMatching?.Questions?
                .Where(x => x.ImageFileId.HasValue);

            if (questionMatchingHasImageFile != null && questionMatchingHasImageFile.Any())
            {
                foreach (var item in questionMatchingHasImageFile)
                {
                    item.ImageFileInfo = await _fileService.GetDtoByIdAsync(item.ImageFileId.Value);
                }
            }


            if (question.ImageFileId.HasValue)
            {
                question.ImageFileInfo = await _fileService.GetDtoByIdAsync(question.ImageFileId.Value);
            }

            question.Answers = grpAnswers?.FirstOrDefault(x => x.Key == question.QuestionId)?
                 .OrderBy(x => x.SortOrder).ToList();

            if (question.Answers != null && question.Answers.Any())
            {
                foreach (var answer in question.Answers)
                {
                    if (questionnaire.Type == Core.Common.Enums.EQuestionnaireType.Droplist)
                    {
                        answer.DropListValue = answer.FakeSelectValues?.Split(StringSplitCharacter, StringSplitOptions.TrimEntries);
                    }

                    if (answer.ImageFileId.HasValue)
                    {
                        answer.ImageFileInfo = await _fileService.GetDtoByIdAsync(answer.ImageFileId.Value);
                    }
                }
            }

            if (questionnaire.Type == Core.Common.Enums.EQuestionnaireType.ImageDragDrop)
            {
                var lstFakeValue = question.FakeValues?.Split(StringSplitCharacter, StringSplitOptions.TrimEntries)?.ToList() ?? new();
                foreach (var data in GetCorrectValues(question.Answers))
                {
                    if (data is null || !data.Any())
                        continue;

                    lstFakeValue.AddRange(data);
                }

                Random randSort = new();
                question.DragDropValues = lstFakeValue.OrderBy(c => randSort.Next()).ToArray();
            }

            question.Translations = grpQuestions?
                .FirstOrDefault(x => x.Key == question.QuestionId)?.ToList() ?? new();
        }

        await _mongoQuestionRepos.DeleteManyAsync(x => x.QuestionnaireId, questionnaireId);
        await _mongoQuestionRepos.InsertManyAsync(questionMaps);

    }

    private IEnumerable<List<string>> GetCorrectValues(List<MgAnswerDto> answers)
    {
        var correctValues = answers.Select(x => x.CorrectMatchingValues);

        foreach (var item in correctValues)
        {
            var result = item?.Split(StringSplitCharacter, StringSplitOptions.TrimEntries)?.ToList() ?? new();
            yield return result;
        }
    }

}
