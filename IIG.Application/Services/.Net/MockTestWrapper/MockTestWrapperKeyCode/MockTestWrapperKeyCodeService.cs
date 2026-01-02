using AutoMapper;
using Azure;
using IIG.Application.BackgroundJob.Dtos;
using IIG.Application.Data;
using IIG.Application.Models;
using IIG.Application.Models.Keycodes;
using IIG.Application.Models.KeyCodes;
using IIG.Application.Models.Questionnaires;
using IIG.Application.Services.Redis;
using IIG.Core.Base;
using IIG.Core.Common.ConfigureModels;
using IIG.Core.Common.Enums;
using IIG.Core.Common.ErrorHandling;
using IIG.Core.Common.MongoDataModels.Keycodes;
using IIG.Core.Common.MongoDataModels.MockTests;
using IIG.Core.Entities;
using IIG.Core.Enums;
using IIG.Core.Helpers;
using IIG.Core.Providers.Interfaces;
using IIG.Core.Providers.RabbitMQProvider;
using IIG.Core.Services;
using IIG.Core.Services.Interfaces;
using IIG.Web.Data.Models;
using LinqKit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using static IIG.Core.Common.ConfigureModels.Constants;
using static IIG.Core.Common.ConfigureModels.Constants.RabbitMQ;

namespace IIG.Application.Services;

public class MockTestWrapperKeyCodeService : IMockTestWrapperKeyCodeService
{

    private readonly IHttpRequestService _httpRequestService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;

    private readonly ILogger<MockTestWrapperKeyCodeService> _logger;
    private readonly IAppFactory  _appFactory;
    private readonly IFileService _fileService;
    private readonly IFileUploaderService _fileUploaderService;
    private readonly IMockTestService _mockTestBiz;
    private readonly IRedisGenericFactory _redisFactory;
    private readonly IMockTestKeyCodeDA _mockTestKeyCodeDA;
    private readonly IKeyCodeResultDA _keyCodeResultDA;
    private readonly IKeyCodeAnswerService _keyCodeAnswerService;
    private readonly IMockTestWrapperRedisDataService _mockTestWrapperDataService;
    private readonly IMockTestRedisDataService _mockTestDataService;
    private readonly IMockTestKeyCodeRedisDataService _mockTestKeyCodeDataService;
    private readonly IQuestionaireRedisDataService _questionaireDataService;
    private readonly IMockTestWrapperKeyCodeRedisDataService _mockTestWrapperKeyCodeDataService;
    private readonly IMongoMockTestKeyCodeService _mongoMockTestKeyCodeService;
    private readonly IRabbitMQProducer _rabbitMQProducer;

    public MockTestWrapperKeyCodeService(
        IHttpRequestService httpRequestService,
        IHttpContextAccessor httpContextAccessor,
        IMapper mapper,
        ILogger<MockTestWrapperKeyCodeService> logger,
        IAppFactory appFactory,
        IFileService fileService,
        IFileUploaderService fileUploaderService,
        IMockTestService mockTestBiz,
        IRedisGenericFactory redisFactory,
        IMockTestKeyCodeDA mockTestKeyCodeDA,
        IKeyCodeResultDA keyCodeResultDA,
        IKeyCodeAnswerService keyCodeAnswerService,
        IMockTestWrapperRedisDataService mockTestWrapperDataService,
        IMockTestRedisDataService mockTestDataService,
        IMockTestKeyCodeRedisDataService mockTestKeyCodeDataService,
        IQuestionaireRedisDataService questionaireDataService,
        IMockTestWrapperKeyCodeRedisDataService mockTestWrapperKeyCodeDataService,
        IMongoMockTestKeyCodeService mongoMockTestKeyCodeService,
       IRabbitMQProducer rabbitMQProducer

        )
    {

        _httpRequestService = httpRequestService;
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
        _logger = logger;
        _appFactory = appFactory;
        _fileService = fileService;
        _mockTestBiz = mockTestBiz;
        _redisFactory = redisFactory;
        _mockTestKeyCodeDA = mockTestKeyCodeDA;
        _keyCodeResultDA = keyCodeResultDA;
        _keyCodeAnswerService = keyCodeAnswerService;
        _mockTestWrapperDataService = mockTestWrapperDataService;
        _mockTestDataService = mockTestDataService;
        _mockTestKeyCodeDataService = mockTestKeyCodeDataService;
        _questionaireDataService = questionaireDataService;
        _mockTestWrapperKeyCodeDataService = mockTestWrapperKeyCodeDataService;
        _mongoMockTestKeyCodeService = mongoMockTestKeyCodeService;
        _rabbitMQProducer = rabbitMQProducer;
    }

    public async Task<VerifyKeyCodeDto> VerifyMockTestAsync(VerifyMockTestWrapperDto request)
    {
        return new VerifyKeyCodeDto();
    }
    public async Task<StartedDoingAnswerMockTestWrapperResponse> StartDoingAnswerAsync(StartedDoingAnswerMockTestWrapperRequest request)
    {
        //kiểm tra user có đang làm 1 bài test nếu, nếu có trả keycode đó không thì tạo mới
        var userId = _httpContextAccessor.HttpContext.User.FindFirst(Constants.ClaimTypes.UserId)?.Value;
        var response = new StartedDoingAnswerMockTestWrapperResponse();
        var languageCode = await _httpRequestService.GetCurrentLanguageCode();
        var utcNow = DateTime.UtcNow;
        var clientIp = _httpRequestService.GetClientIpAddress();

        _logger.LogInformation($"StartedDoingAnswerMockTestWrapper: {request.MocktestWrapperId} -- {userId} ----- Start");

        var mockTestWrapperData = await _mockTestWrapperDataService.GetDetailById(request.MocktestWrapperId, languageCode);
        var mockTestData = await _mockTestDataService.GetMockTest(mockTestWrapperData.MockTestId);

        var keyCodeInfo = await _mockTestWrapperKeyCodeDataService.GetMockTestKeyCodeInfoByUser(request.MocktestWrapperId, Guid.Parse(userId));
        var keyCode = keyCodeInfo?.KeyCode;


        //get course scoring info if exists

        if (keyCodeInfo == null)
        {
            _logger.LogInformation($"StartedDoingAnswerMockTestWrapper: {request.MocktestWrapperId} -- {userId} -- mockTestKeyCodeInfo null");

            keyCode = StringExtensions.GenerateKeyCode(Constants.KeyCode.KeyCodeLength, 1).First();

            _logger.LogInformation($"StartedDoingAnswerMockTestWrapper: {request.MocktestWrapperId} -- {userId} -- StartedDoingAnswerMockTestWrapper: {keyCode}");

            var mockTestKeyCodeEntity = new MocktestKeyCode
            {
                MocktestId = mockTestData.MockTestId,
                WebUserId = Guid.Parse(userId),
                Code = keyCode,
                Created = utcNow,
                IsAutoGenerate = true,
                StartDate = utcNow,
                EndDate = utcNow.AddSeconds(mockTestData.MockTestSections.Sum(p => p.NumberOfTime)),
            };
            await _appFactory.Repository<MocktestKeyCode>().InsertAsync(mockTestKeyCodeEntity);

            //var mgMockTestModelAdd = await _mockTestDumpDataToMongoDbBiz.GetOrDumpAsync(mockTestKeyCode.MocktestId, mockTestKeyCode.IsAutoGenerate);

            var dumpModelAdd = new MgMockTestKeyCodeModel()
            {
                Browser = request.Browser,
                ClientIp = clientIp,
                KeyCode = keyCode,
            };
            dumpModelAdd.ClientIp = clientIp;
            dumpModelAdd.MockTestId = mockTestData.MockTestId;
            dumpModelAdd.MockTestName = mockTestData.GeneralInfo.Names.Where(p => p.LanguageCode == languageCode).Select(p => p.Name).FirstOrDefault();
            dumpModelAdd.MockTestMenu = mockTestData.MockTestMenu;
            dumpModelAdd.TimeRemaining = mockTestData.MockTestSections.Sum(s => s.NumberOfTime);
            dumpModelAdd.StartedDoingExamDate = DateTime.UtcNow;
            dumpModelAdd.EndedDoingExamDate = dumpModelAdd.StartedDoingExamDate.Value.AddSeconds(dumpModelAdd.TimeRemaining);
            dumpModelAdd.MockTestMenu?.Sections?.ToList()
                                                    .ForEach(section => section?.Parts?
                                                        .ToList()
                                                        .ForEach(part => part?.Questionnaires?
                                                            .ToList()
                                                            .ForEach(qn => qn?.Questions?
                                                                .ToList()
                                                                .ForEach(q => q.Status = EAnswerStatus.NotAnswered))));
            dumpModelAdd.IsAutoGenerate = true;
            //save redis cache
            await _mockTestKeyCodeDataService.InsertOrUpdateKeyCodeAsync(keyCode, dumpModelAdd, dumpModelAdd.TimeRemaining + 30);
            await _mockTestWrapperKeyCodeDataService.InsertMockTestKeyCodeInfoByUser(request.MocktestWrapperId, Guid.Parse(userId), dumpModelAdd);
            //Send to rabbitMQ to execute update mongoDB and SQL
            response = _mapper.Map<StartedDoingAnswerMockTestWrapperResponse>(dumpModelAdd);
            response.KeyCode = keyCode;

            _rabbitMQProducer.SendMessage(new RabbitMQMockTestWrapperKeyCodeActionModel()
            {
                Action = RabbitMQMockTestWrapperKeyCodeAction.StartDoingTest,
                KeyCode = keyCode,
                Data = JsonConvert.SerializeObject(request)
            }, Constants.RabbitMQ.MockTestWrapperKeyCode.ExchangeMockTestRedisToMongo, Constants.RabbitMQ.MockTestWrapperKeyCode.QueueMockTestRedisToMongoRoutingkey);

            _logger.LogInformation($"StartedDoingAnswerMockTestWrapper: {request.MocktestWrapperId} -- {userId} ----- End");

            return response;
        }

        _logger.LogInformation($"StartedDoingAnswerMockTestWrapper: {request.MocktestWrapperId} -- {userId} -- StartedDoingAnswerMockTestWrapper: {keyCode}");
        //get mockTest data from mongodb
        //var mgMockTestModel = await _mockTestDumpDataToMongoDbBiz.GetOrDumpAsync(mockTestKeyCode.MocktestId, mockTestKeyCode.IsAutoGenerate);
        var mgMockTestModel = await _mockTestDataService.GetMockTest(keyCodeInfo.MockTestId);
        var courseScoring = await _mockTestKeyCodeDataService.GetTupleCourseScoring(keyCode);

        var dumpModel = new MgMockTestKeyCodeModel()
        {
            Browser = request.Browser,
            ClientIp = clientIp,
            KeyCode = keyCode,
        };
        dumpModel.ClientIp = clientIp;
        dumpModel.MockTestId = mgMockTestModel.MockTestId;
        dumpModel.MockTestMenu = mgMockTestModel.MockTestMenu;
        dumpModel.StartedDoingExamDate = keyCodeInfo.StartedDoingExamDate;
        dumpModel.EndedDoingExamDate = keyCodeInfo.EndedDoingExamDate;
        dumpModel.TimeRemaining = (int)(dumpModel.EndedDoingExamDate.Value - DateTime.UtcNow).TotalSeconds;
        dumpModel.TimeRemaining = dumpModel.TimeRemaining < 0 ? 0 : dumpModel.TimeRemaining;
        dumpModel.IsAutoGenerate = keyCodeInfo.IsAutoGenerate;
        //get data of this keycode test from mongodb
        dumpModel.Id = keyCodeInfo.Id;

        var areSpeakingQuestionnaires = false;
        dumpModel.CurrentMockTestPartId = keyCodeInfo.CurrentMockTestPartId;
        if (!areSpeakingQuestionnaires)
        {
            dumpModel.CurrentQuestionnaireId = keyCodeInfo.CurrentQuestionnaireId;
        }
        else
        {
            var removeQuestionStatus = keyCodeInfo.MockTestMenu.Sections.Where(x => x.Parts.Any(p => p.Id == keyCodeInfo.CurrentMockTestPartId && p.Questionnaires.Any(q => q.Id == keyCodeInfo.CurrentQuestionnaireId))).SelectMany(x => x.Parts).Where(x => x.Id == keyCodeInfo.CurrentMockTestPartId).SelectMany(x => x.Questionnaires).SelectMany(x => x.Questions).ToList();
            foreach (var item in removeQuestionStatus)
            {
                item.Status = EAnswerStatus.NotAnswered;
            }

        }
        dumpModel.MockTestMenu = keyCodeInfo.MockTestMenu;

        await _mockTestKeyCodeDataService.InsertOrUpdateKeyCodeAsync(keyCode, dumpModel);


        //Send to rabbitMQ to execute update mongoDB and SQL
        _rabbitMQProducer.SendMessage(new RabbitMQMockTestWrapperKeyCodeActionModel()
        {
            Action = RabbitMQMockTestWrapperKeyCodeAction.StartDoingTest,
            KeyCode = keyCode,
            Data = JsonConvert.SerializeObject(request)
        }, Constants.RabbitMQ.MockTestWrapperKeyCode.ExchangeMockTestRedisToMongo, Constants.RabbitMQ.MockTestWrapperKeyCode.QueueMockTestRedisToMongoRoutingkey);


        keyCodeInfo = dumpModel;
        keyCodeInfo.MockTestMenu = mgMockTestModel.MockTestMenu;
        response = _mapper.Map<StartedDoingAnswerMockTestWrapperResponse>(keyCodeInfo);

        response.KeyCode = keyCode;

        _logger.LogInformation($"StartedDoingAnswerMockTestWrapper: {request.MocktestWrapperId} -- {userId} ----- End");

        return response;
    }
    public async Task<MockTestResultDto> ViewMockTestResultAsync(string keyCode)
    {
        var keyCodeExist = await _appFactory.Repository<MocktestKeyCode>().GetAll().Where(x => x.Code == keyCode).Select(p=> new
        {
            p.Code,
            p.MocktestId,
            p.MocktestPublishedAt,
            p.StartedDoingExamDate
        }).FirstOrDefaultAsync();

        if (keyCodeExist == null)
        {
            throw new ApiValidationException(WebConstants.ValidationMessages.MockTestMessage.KeyCodeNotFoundKey, nameof(keyCode));
        }

        var languageCode = await _httpRequestService.GetCurrentLanguageCode();

        var mockTestKeyCodeDto = await _mockTestKeyCodeDA.GetDetailByKeyCode(keyCode, languageCode, keyCodeExist.MocktestPublishedAt);
        if (mockTestKeyCodeDto.SubmittedDate is null)
        {
            throw new ApiValidationException(String.Format(WebConstants.ValidationMessages.MockTestKeyCodeMessage.KeyCodeHaveNotSubmittedYet, keyCode), nameof(mockTestKeyCodeDto.SubmittedDate));
        }

        var resultModel = new MockTestResultDto();
        var tmpKeyCodeResultDto = await _keyCodeResultDA.GetDetailFromKeyCode(keyCode);
        var keyCodeResult = _mapper.Map<KeyCodeResultModel>(tmpKeyCodeResultDto);
        IEnumerable<MockTestSectionResultDto> listMockTestSectionResultDto = null;
        if (keyCodeResult != null)
        {
            listMockTestSectionResultDto = await _mockTestKeyCodeDA.GetMockTestSectionResultV2(keyCodeResult.Sections, mockTestKeyCodeDto.SubmittedDate, keyCodeExist.MocktestPublishedAt);
            listMockTestSectionResultDto = listMockTestSectionResultDto.OrderBy(x => x.SortOrder);

            resultModel = GenerateScoreResult(mockTestKeyCodeDto, listMockTestSectionResultDto);
        }
       
       
        var viewMockTestDetail = await ViewMockTestResultDetailAsync(keyCode, keyCodeExist.MocktestId, mockTestKeyCodeDto.SubmittedDate);

        resultModel.QuestionDetails = viewMockTestDetail.Questions;
        resultModel.SubmittedDate = mockTestKeyCodeDto.SubmittedDate;
        resultModel.StartDoingDate = keyCodeExist.StartedDoingExamDate;
        return resultModel;
    }

    public async Task<MockTestViewResultDetailModel> ViewMockTestResultDetailAsync(string keyCode, Guid mockTestId, DateTime? submitDate)
    {
        submitDate = submitDate ?? DateTime.UtcNow;

        var query = from mtpq in _appFactory.Repository<MocktestPartQuestionnaire>().GetTable().TemporalAsOf(submitDate.Value)
                    join mtp in _appFactory.Repository<MocktestPart>().GetTable().TemporalAsOf(submitDate.Value) on mtpq.MocktestPartId equals mtp.Id
                    join mts in _appFactory.Repository<MocktestSection>().GetTable().TemporalAsOf(submitDate.Value) on mtp.MocktestSectionId equals mts.Id
                    where mts.MocktestId == mockTestId
                    orderby mts.SortOrder, mtp.SortOrder, mtpq.SortOrder
                    select new 
                    {
                        QuestionnaireId = mtpq.QuestionnaireId,
                        PartName = mtp.Name,
                        PartId = mtp.Id,
                        SectionName = mts.Name,
                        SectionId = mts.Id
                    };
        var mockTestQuestionnaireDetails = await query.ToListAsync();

        var listQuestionnaireTypeNeedToGetQuestion = new List<short>
        {
            (short) EQuestionnaireType.MCQ,
            (short) EQuestionnaireType.MCQImage,
            (short) EQuestionnaireType.TrueFalse,
            (short) EQuestionnaireType.Record,
            (short) EQuestionnaireType.ReadTextALoud,
            (short) EQuestionnaireType.Writing,
        };
        var questionnaireIds = mockTestQuestionnaireDetails.Select(p => p.QuestionnaireId).Distinct();
        var mockTestQuestionDetails = await _appFactory.Repository<Question>().GetAll().Where(p => questionnaireIds.Contains(p.QuestionnaireId) && listQuestionnaireTypeNeedToGetQuestion.Contains(p.Questionnaire.Type)).Select(p => new MockTestQuestionDetailModel
        {
            QuestionId = p.Id,
            QuestionnaireId = p.QuestionnaireId,
            Tags = p.QuestionTags.Select(qt => qt.Tag.Name)
        }).ToListAsync();
        var questionIds = mockTestQuestionDetails.Select(p => p.QuestionId).Distinct().ToList();

        var keyCodeChooseQuestionDics = await _appFactory.Repository<KeycodeChoose>().GetAll().Where(p => p.Keycode == keyCode && questionIds.Contains(p.QuestionId) && !p.IsDelete.HasValue).Select(p => new
        {
            p.QuestionId,
            p.MocktestPartId,
            p.CorrectAnswer
        }).ToDictionaryAsync(p => new { p.QuestionId, p.MocktestPartId }, p => p.CorrectAnswer);

        var listQuestionResults = new List<MockTestQuestionDetailModel>();
        short stt = 0;
        foreach (var item in mockTestQuestionnaireDetails)
        {
            var questions = mockTestQuestionDetails.Where(p => p.QuestionnaireId == item.QuestionnaireId).ToList();
            foreach(var question in questions)
            {
                listQuestionResults.Add(new MockTestQuestionDetailModel
                {
                    STT = ++stt,
                    QuestionId = question.QuestionId,
                    QuestionnaireId = item.QuestionnaireId,
                    PartId = item.PartId,
                    PartName = item.PartName,
                    SectionId = item.SectionId,
                    SectionName = item.SectionName,
                    Tags = question.Tags,
                    IsCorrect = keyCodeChooseQuestionDics.TryGetValue(new { question.QuestionId, MocktestPartId = item.PartId }, out var correctAnswer) ? correctAnswer : (bool?)null,
                });
            }    
          
        }
        return new MockTestViewResultDetailModel
        {
            MockTestId = mockTestId,
            Questions = listQuestionResults
        };

    }
    public async Task<MockTestStructureModel> GetMocktestStructureAsync(Guid mockTestWrapperId)
    {
        var languageCode = await _httpRequestService.GetCurrentLanguageCode();
        var mockTestWrapperData = await _mockTestWrapperDataService.GetDetailById(mockTestWrapperId, null);
        //var mockTestKeyCode = await _mockTestKeyCodeDataService.GetMockTestKeyCodeAsync(keyCode);
        if (mockTestWrapperData == null)
        {
            throw new ApiValidationException(WebConstants.ValidationMessages.MockTestWrapper.WrapperNotFound,
                nameof(mockTestWrapperId));
        }
        var mockTestIdByKeyCode = mockTestWrapperData.MockTestId;
        var mockTest = await _mockTestDataService.GetMockTest(mockTestIdByKeyCode);

        return _mapper.Map<MockTestStructureModel>(mockTest);
    }
    public async Task<MgMockTestMenuModel> GetMockTestMenuAsync(MockTestKeyCodeBaseRequest request)
    {
        var mockTestKeyCodeInfo = await VerifyRequestInfoAndGetKeyCodeInfoAsync(request);

        return mockTestKeyCodeInfo.MockTestMenu;
    }
    public async Task<MgMockTestPartModel> GetMockTestPartDetailAsync(GetMockTestPartDetailRequest request)
    {
        _logger.LogInformation($"MockTestWrapperKeyCode ---- GetMockTestPartDetailAsync {request.KeyCode} --- Start");

        var courseScoring = await _mockTestKeyCodeDataService.GetTupleCourseScoring(request.KeyCode);

        var mockTestKeyCodeInfo = await VerifyRequestInfoAndGetKeyCodeInfoAsync(request, courseScoring: courseScoring);
        _logger.LogInformation($"MockTestWrapperKeyCode ---- GetMockTestPartDetailAsync {request.KeyCode} ---- mockTestKeyCodeInfo ID = {mockTestKeyCodeInfo.MockTestId}");

        var mgMockTestModel = await _mockTestDataService.GetMockTest(mockTestKeyCodeInfo.MockTestId);

        var mgMockTestPartDetail = mgMockTestModel.MockTestSections.SelectMany(x => x.Parts)
            .FirstOrDefault(x => x.Id == request.MockTestPartId);

        _logger.LogInformation($"MockTestWrapperKeyCode ---- GetMockTestPartDetailAsync {request.KeyCode} --- End");

        return mgMockTestPartDetail;
    }
    public async Task<QuestionnaireDto> GetQuestionnaireDetailAsync(GetQuestionnaireDetailRequest request)
    {


        var questionnaire = await _questionaireDataService.GetQuestionaire(request.QuestionnaireId);
        var questions = await _questionaireDataService.GetQuestions(request.QuestionnaireId);
        var leftSections = await _questionaireDataService.GetLeftSections(request.QuestionnaireId);

        // Kiểm tra nếu questionnaire không tồn tại
        if (questionnaire is null)
        {
            throw new ApiNotFoundException(WebConstants.ValidationMessages.WebQuestionnaireMessage.QuestionnaireNotFoundKey);
        }

        // Ánh xạ dữ liệu từ questionnaire
        var result = _mapper.Map<QuestionnaireDto>(questionnaire);

        // Xử lý câu hỏi dựa trên loại câu hỏi
        if (questionnaire.Type == EQuestionnaireType.Matching || questionnaire.Type == EQuestionnaireType.MatchingImage)
        {
            var sortbyQuestion = _mapper.Map<List<QuestionDto>>(questions);
            SortRandomAnswers(sortbyQuestion);
            result.Questions = sortbyQuestion;
        }
        else
        {
            result.Questions = _mapper.Map<List<QuestionDto>>(questions).Select(q =>
            {
                q.Answers = q.Answers.OrderBy(a => a.SortOrder).ToList();
                return q;
            }).ToList();
        }

        // Sắp xếp các phần trái (left sections)
        var leftSectionsWithOrder = await _questionaireDataService.GetListIdsByQuestionnaireIdAsync(questionnaire.QuestionnaireId);
        var allIdsExist = leftSectionsWithOrder.All(id => leftSections.Any(item => item.LeftSectionId == id));
        var leftSectionsSorted = allIdsExist
            ? leftSections.OrderBy(ls => leftSectionsWithOrder.IndexOf(ls.LeftSectionId))
            : leftSections;

        result.LeftSections = _mapper.Map<List<LeftSectionDto>>(leftSectionsSorted);

        // Truy vấn trả lời trước khi nộp (pre-submit answer)
        var preSubmitAnswerTask = _mockTestKeyCodeDataService.GetKeyCodeAnswerAsync(request.KeyCode);

        await Task.WhenAll(preSubmitAnswerTask);

        var preSubmitAnswer = await preSubmitAnswerTask;

        // Gán câu trả lời đã nộp vào câu hỏi
        foreach (var question in result.Questions)
        {
            question.SubmittedQuestionModel = new Core.Common.Models.SpeakingAndWriting.SubmittedQuestionModel();

            var matchingPreSubmitAnswer = preSubmitAnswer?.FirstOrDefault(x => x.QuestionnaireId == question.QuestionnaireId &&
                                                                                x.QuestionId == question.QuestionId);


            question.SubmittedQuestionModel.AnsweredText = matchingPreSubmitAnswer?.AnswerText;
            question.SubmittedQuestionModel.AudioFileId = matchingPreSubmitAnswer?.RecordingFileId;
        }

        // Sắp xếp câu hỏi theo thứ tự SortOrder
        result.Questions = result.Questions.OrderBy(f => f.SortOrder).ToList();

        return result;
    }
    public async Task<IEnumerable<AnswerResponse>> GetAnswerAsync(GetAnswerRequest request)
    {
        var dataRedis = await _mockTestKeyCodeDataService.GetKeyCodeAnswerAsync(request.KeyCode);
        var mongoAnswerList = dataRedis.Where(x =>
            x.KeyCode.Equals(request.KeyCode) &&
            x.MockTestPartId.Equals(request.MockTestPartId) &&
            x.QuestionnaireId.Equals(request.QuestionnaireId));

        var response = _mapper.Map<List<AnswerResponse>>(mongoAnswerList);

        return response;
    }
    public async Task MarkQuestionsTask(MarkQuestionRequest request)
    {
        _logger.LogInformation($"MarkQuestionsTask {request.KeyCode}");

        var mockTestKeyCodeInfo = await VerifyRequestInfoAndGetKeyCodeInfoAsync(request);

        var questionList = mockTestKeyCodeInfo.MockTestMenu.Sections.SelectMany(m => m.Parts)
            .SelectMany(p => p.Questionnaires).Where(q => q.Id == request.QuestionnaireId).SelectMany(q => q.Questions);
        foreach (var questionModel in questionList)
        {
            questionModel.Marked = request.Marked;
        }

        await _mockTestKeyCodeDataService.InsertOrUpdateKeyCodeAsync(mockTestKeyCodeInfo.KeyCode, mockTestKeyCodeInfo);

        //Send to rabbitMQ to execute update mongoDB and SQL   
        _rabbitMQProducer.SendMessage(new RabbitMQMockTestWrapperKeyCodeActionModel()
        {
            Action = RabbitMQMockTestWrapperKeyCodeAction.MarkQuestion,
            KeyCode = request.KeyCode,
            Data = JsonConvert.SerializeObject(request)
        }, Constants.RabbitMQ.MockTestWrapperKeyCode.ExchangeMockTestRedisToMongo, Constants.RabbitMQ.MockTestWrapperKeyCode.QueueMockTestRedisToMongoRoutingkey);
    }

    public async Task<MgMockTestMenuModel> SaveAnswerAsync(SaveAnswerRequest request)
    {
        _logger.LogInformation($"MockTestWrapperKeycode --- SaveAnswerAsync {request.KeyCode} -- QuestionnaireId {request.QuestionnaireId} --- Start");

        var mockTestKeyCodeInfo = await VerifyRequestInfoAndGetKeyCodeInfoAsync(request);

        var newestMockTestKeyCodeMenu = await ProcessSaveAnswerAsync(request, mockTestKeyCodeInfo);

        //Send to rabbitMQ to execute update mongoDB and SQL
        _rabbitMQProducer.SendMessage(new RabbitMQMockTestWrapperKeyCodeActionModel()
        {
            Action = RabbitMQMockTestWrapperKeyCodeAction.SaveAnswer,
            KeyCode = request.KeyCode,
            Data = JsonConvert.SerializeObject(request)
        }, Constants.RabbitMQ.MockTestWrapperKeyCode.ExchangeMockTestRedisToMongo, Constants.RabbitMQ.MockTestWrapperKeyCode.QueueMockTestRedisToMongoRoutingkey);

        _logger.LogInformation($"MockTestWrapperKeycode --- SaveAnswerAsync {request.KeyCode} -- QuestionnaireId {request.QuestionnaireId} --- Start");

        return newestMockTestKeyCodeMenu;
    }
    public async Task SubmitMockTestAsync(Guid mockTestWrapperId)
    {
        var userId = _httpContextAccessor.HttpContext.User.FindFirst(Constants.ClaimTypes.UserId)?.Value;
        _logger.LogInformation($"MockTestWrapperKeycode ---  SubmitMockTestAsync {mockTestWrapperId} -- userId: {userId} --- Start");

        var keyCodeInfo = await _mockTestWrapperKeyCodeDataService.GetMockTestKeyCodeInfoByUser(mockTestWrapperId, Guid.Parse(userId));

        await _keyCodeAnswerService.SubmitMockTestAsync(keyCodeInfo.KeyCode);

        await _mockTestWrapperKeyCodeDataService.DeleteMockTestKeyCodeInfoByUser(mockTestWrapperId, Guid.Parse(userId));
        _logger.LogInformation($"MockTestWrapperKeycode ---  SubmitMockTestAsync {mockTestWrapperId} -- userId: {userId} --- End");

    }

    //public async Task AutoDeleteUnSubmittedKeyCode()
    //{
    //    var listKeyCodeNeedDelete = await _mongoMockTestKeyCodeService.FilterAsync(x => x.EndedDoingExamDate < DateTime.UtcNow && x.IsAutoGenerate == true);
    //    var keyCodes = listKeyCodeNeedDelete.Select(p => p.KeyCode).ToList();
    //    _logger.LogInformation($"AutoDeleteUnSubmittedKeyCode ---  Found {listKeyCodeNeedDelete.Count()} ---- ({string.Join(",", keyCodes)})");

    //    // delete from sql 
    //    await _iIGLmsdbContext.MocktestKeyCodes.Where(p => keyCodes.Contains(p.Code)).ExecuteDeleteAsync();
    //    await _iIGLmsdbContext.SaveChangesAsync();

    //    foreach (var item in listKeyCodeNeedDelete)
    //    {
    //        _logger.LogInformation($"AutoDeleteUnSubmittedKeyCode ---  KeyCode {item.KeyCode} --- Start");
    //        await _keyCodeAnswerService.DeleteDataAsync(item.KeyCode);
    //        _logger.LogInformation($"AutoDeleteUnSubmittedKeyCode ---  KeyCode {item.KeyCode} --- End");
    //    }
    //}
    public async Task<FileStreamResult> StreamingPlayAsync(string keyCode, Guid fileId)
    {
        var mockTestKeyCodeInfo = await _mockTestKeyCodeDataService.GetMockTestKeyCodeAsync(keyCode);
        if (mockTestKeyCodeInfo == null)
        {
            throw new ApiValidationException(WebConstants.ValidationMessages.MockTestMessage.KeyCodeNotFoundKey,
                nameof(keyCode));
        }

        return await _fileUploaderService.StreamingPlayAsync(fileId);
    }

    private async Task<MgMockTestKeyCodeModel> VerifyRequestInfoAndGetKeyCodeInfoAsync(MockTestKeyCodeBaseRequest request, string? requestCookie = null, Tuple<CourseScoring, bool> courseScoring = null)
    {
        var mockTestKeyCodeInfo = await _mockTestKeyCodeDataService.GetMockTestKeyCodeAsync(request.KeyCode);
        _logger.LogInformation($"mockTestKeyCodeInfo = {mockTestKeyCodeInfo}");
        if (mockTestKeyCodeInfo == null)
        {
            throw new ApiValidationException(WebConstants.ValidationMessages.MockTestMessage.KeyCodeNotFoundKey,
                nameof(request.KeyCode));
        }

        return mockTestKeyCodeInfo;
    }
    private async Task<MgMockTestKeyCodeModel> UpdateMockTestMenuAsync(MgMockTestKeyCodeModel mgMockTestKeyCode, SaveAnswerRequest request, EAnswerStatus status)
    {
        _logger.LogInformation($"MockTestWrapperKeycode --- SaveAnswerAsync {request.KeyCode} -- QuestionnaireId {request.QuestionnaireId} --- UpdateMockTestMenuAsync - Start");

        var question = mgMockTestKeyCode.MockTestMenu?.Sections?.SelectMany(m => m.Parts)
            .Where(x => x.Id == request.MockTestPartId)
            .SelectMany(p => p.Questionnaires)
            .Where(q => q.Id == request.QuestionnaireId)
            .SelectMany(q => q.Questions)
            .FirstOrDefault(x => x.Id == request.QuestionId || x.Id == request.AnswerId);

        if (question != null) question.Status = status;
        // time remaining = 
        mgMockTestKeyCode.TimeRemaining = (int)(mgMockTestKeyCode.EndedDoingExamDate.Value - DateTime.UtcNow).TotalSeconds;
        mgMockTestKeyCode.CurrentMockTestPartId = request.MockTestPartId;
        mgMockTestKeyCode.CurrentQuestionnaireId = request.QuestionnaireId;

        await _mockTestKeyCodeDataService.InsertOrUpdateKeyCodeAsync(request.KeyCode, mgMockTestKeyCode);

        _logger.LogInformation($"MockTestWrapperKeycode --- SaveAnswerAsync {request.KeyCode} -- QuestionnaireId {request.QuestionnaireId} --- UpdateMockTestMenuAsync - End");

        return mgMockTestKeyCode;
    }
    private void SortRandomAnswers(List<QuestionDto> questions)
    {
        foreach (var question in questions)
        {
            var random = new Random();
            question.Answers = question.Answers.OrderBy(a => random.Next()).ToList();
        }
    }
    private async Task<MgMockTestMenuModel> ProcessSaveAnswerAsync(SaveAnswerRequest request, MgMockTestKeyCodeModel mockTestKeyCodeInfo)
    {
        _logger.LogInformation($"MockTestWrapperKeycode --- SaveAnswerAsync {request.KeyCode} -- QuestionnaireId {request.QuestionnaireId} --- ProcessSaveAnswerAsync - Start");

        EAnswerStatus status = EAnswerStatus.NotAnswered;
        MgKeyCodeAnswerModel mongoKeyCodeAnswer;

        switch (request.QuestionnaireType)
        {
            case EQuestionnaireType.Video:
            case EQuestionnaireType.Slide:
            case EQuestionnaireType.MCQ:
            case EQuestionnaireType.PronunciationRecognition:
            case EQuestionnaireType.FlashCard:
            case EQuestionnaireType.MCQImage:
            case EQuestionnaireType.TrueFalse:
            case EQuestionnaireType.Record:
            case EQuestionnaireType.ReadTextALoud:
            case EQuestionnaireType.Writing:

                await _mockTestKeyCodeDataService.DeleteKeyCodeAnswer(request.KeyCode, x => x.KeyCode == request.KeyCode &&
                                                                      x.MockTestSectionId ==
                                                                      request.MockTestSectionId &&
                                                                      x.MockTestPartId == request.MockTestPartId &&
                                                                      x.QuestionnaireId == request.QuestionnaireId &&
                                                                      x.QuestionId == request.QuestionId);
                mongoKeyCodeAnswer = _mapper.Map<MgKeyCodeAnswerModel>(request);


                mongoKeyCodeAnswer.LastSaveDate = DateTime.UtcNow;

                status = EAnswerStatus.InCorrect;
                if (request.QuestionnaireType == EQuestionnaireType.Writing && string.IsNullOrEmpty(request.AnswerText))
                {
                    status = EAnswerStatus.NotAnswered;
                }
                else if (request.QuestionnaireType == EQuestionnaireType.Writing && !string.IsNullOrEmpty(request.AnswerText))
                {
                    status = EAnswerStatus.Correct;
                }

                //Handle file Speaking
                if (request.QuestionnaireType == EQuestionnaireType.Record ||
                    request.QuestionnaireType == EQuestionnaireType.ReadTextALoud
                    )
                {
                    if (request.FileInfo != null)
                    {
                        request.FileInfo.Id = Guid.NewGuid();
                        request.FileInfo.IsActive = true;
                        request.FileInfo.IsMigrate = true;

                        await _fileService.InsertFileAsync(request.FileInfo);

                        mongoKeyCodeAnswer.RecordingFileId = request.FileInfo.Id;
                        status = EAnswerStatus.Correct;
                    }
                }
                await _mockTestKeyCodeDataService.InsertKeyCodeAnswer(request.KeyCode, mongoKeyCodeAnswer);

                break;

            case EQuestionnaireType.Droplist:

                await _mockTestKeyCodeDataService.DeleteKeyCodeAnswer(request.KeyCode, x => x.KeyCode == request.KeyCode &&
                                                                      x.MockTestSectionId ==
                                                                      request.MockTestSectionId &&
                                                                      x.MockTestPartId == request.MockTestPartId &&
                                                                      x.QuestionnaireId == request.QuestionnaireId &&
                                                                      x.QuestionId == request.QuestionId &&
                                                                      x.AnswerId == request.AnswerId);

                mongoKeyCodeAnswer = _mapper.Map<MgKeyCodeAnswerModel>(request);
                await _mockTestKeyCodeDataService.InsertKeyCodeAnswer(request.KeyCode, mongoKeyCodeAnswer);
                status = EAnswerStatus.InCorrect;
                break;

            case EQuestionnaireType.ImageDragDrop:
            case EQuestionnaireType.FillInTheBlank:

                await _mockTestKeyCodeDataService.DeleteKeyCodeAnswer(request.KeyCode, x => x.KeyCode == request.KeyCode &&
                                                                      x.MockTestSectionId ==
                                                                      request.MockTestSectionId &&
                                                                      x.MockTestPartId == request.MockTestPartId &&
                                                                      x.QuestionnaireId ==
                                                                      request.QuestionnaireId &&
                                                                      x.QuestionId == request.QuestionId &&
                                                                      x.AnswerId == request.AnswerId);

                if (!string.IsNullOrEmpty(request.AnswerText))
                {
                    mongoKeyCodeAnswer = _mapper.Map<MgKeyCodeAnswerModel>(request);
                    await _mockTestKeyCodeDataService.InsertKeyCodeAnswer(request.KeyCode, mongoKeyCodeAnswer);

                    status = EAnswerStatus.InCorrect;
                }
                break;

            case EQuestionnaireType.Matching:
            case EQuestionnaireType.MatchingImage:

                await _mockTestKeyCodeDataService.DeleteKeyCodeAnswer(request.KeyCode, x => x.KeyCode == request.KeyCode &&
                                                                      x.MockTestSectionId ==
                                                                      request.MockTestSectionId &&
                                                                      x.MockTestPartId == request.MockTestPartId &&
                                                                      x.QuestionnaireId ==
                                                                      request.QuestionnaireId &&
                                                                      x.QuestionId == request.QuestionId &&
                                                                      x.AnswerId == request.AnswerId);
                if (request.MatchingQuestionId.HasValue)
                {
                    mongoKeyCodeAnswer = _mapper.Map<MgKeyCodeAnswerModel>(request);
                    await _mockTestKeyCodeDataService.InsertKeyCodeAnswer(request.KeyCode, mongoKeyCodeAnswer);

                    status = EAnswerStatus.InCorrect;
                }

                break;
            default:
                throw new ArgumentOutOfRangeException(WebConstants.ValidationMessages.AssessmentMessage
                    .QuestionnaireTypeIsNotValid);
        }

        //Update MockTest Menu
        var newestMockTestKeyCodeInfo = await UpdateMockTestMenuAsync(mockTestKeyCodeInfo, request, status);

        _logger.LogInformation($"MockTestWrapperKeycode --- SaveAnswerAsync {request.KeyCode} -- QuestionnaireId {request.QuestionnaireId} --- ProcessSaveAnswerAsync - End");

        return newestMockTestKeyCodeInfo.MockTestMenu;
    }
    private MockTestResultDto GenerateScoreResult(MockTestKeyCodeBasicDto mockTestKeyCodeDto, IEnumerable<MockTestSectionResultDto> listMockTestSectionResultDto)
    {
        var scoreResult = new MockTestResultDto()
        {
            MockTestName = mockTestKeyCodeDto.MockTestName,
            MockTestId = mockTestKeyCodeDto.MockTestId,
            ScoreType = mockTestKeyCodeDto.MockTestScoreType,
            LinkUrlViewMore = mockTestKeyCodeDto.LinkUrlViewMore,
            TextViewMore = mockTestKeyCodeDto.TextViewMore,
        };

        scoreResult = mockTestKeyCodeDto.MockTestScoreType switch
        {
            EMockTestScoreType.CorrectScore => GenerateCorrectScoreType(scoreResult, listMockTestSectionResultDto),
            EMockTestScoreType.ScoreRange => GenerateScoreRangeType(scoreResult, listMockTestSectionResultDto),
            EMockTestScoreType.ITPScoreRange => GenerateITPScoreRangeType(scoreResult, listMockTestSectionResultDto),
            _ => throw new ArgumentOutOfRangeException(WebConstants.ValidationMessages.MockTestKeyCodeMessage.MockTestScoreTypeIsNotValid)
        };
        scoreResult.Sections = listMockTestSectionResultDto;

        return scoreResult;
    }
    private MockTestResultDto GenerateCorrectScoreType(MockTestResultDto result, IEnumerable<MockTestSectionResultDto> listMockTestSectionResultDto)
    {
        result.TotalScore = listMockTestSectionResultDto.Sum(x => x.ExactScore).ToString();
        result.MinScore = listMockTestSectionResultDto.Sum(x => x.MinScore);
        result.MaxScore = listMockTestSectionResultDto.Sum(x => x.MaxScore);
        result.ExtractScore = listMockTestSectionResultDto.Sum(x => x.ExactScore).ToString();
        listMockTestSectionResultDto.ForEach(x => x.Score = x.ExactScore.ToString());

        return result;
    }

    private MockTestResultDto GenerateScoreRangeType(MockTestResultDto result, IEnumerable<MockTestSectionResultDto> listMockTestSectionResultDto)
    {
        result.TotalScore = $"{listMockTestSectionResultDto.Sum(x => x.FromScore)}-{listMockTestSectionResultDto.Sum(x => x.ToScore)}";
        result.MinScore = listMockTestSectionResultDto.Sum(x => x.MinScore);
        result.MaxScore = listMockTestSectionResultDto.Sum(x => x.MaxScore);
        result.ExtractScore = listMockTestSectionResultDto.Sum(x => x.ExactScore).ToString();
        listMockTestSectionResultDto.ForEach(x => x.Score = $"{x.FromScore} - {x.ToScore}");

        return result;
    }

    private MockTestResultDto GenerateITPScoreRangeType(MockTestResultDto result, IEnumerable<MockTestSectionResultDto> listMockTestSectionResultDto)
    {
        result.TotalScore = $"{(int)Math.Round(listMockTestSectionResultDto.Average(x => x.FromScore) * 10)}-{(int)Math.Round(listMockTestSectionResultDto.Average(x => x.ToScore) * 10)}";
        result.MinScore = (int)Math.Round(listMockTestSectionResultDto.Average(x => x.MinScore) * 10);
        result.MaxScore = (int)Math.Round(listMockTestSectionResultDto.Average(x => x.MaxScore) * 10);
        result.ExtractScore = (listMockTestSectionResultDto.Sum(x => x.ExactScore) * 10 / listMockTestSectionResultDto.Count()).ToString();
        listMockTestSectionResultDto.ForEach(x => x.Score = $"{x.FromScore} - {x.ToScore}");

        return result;
    }

}
