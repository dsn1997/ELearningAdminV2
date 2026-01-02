using AutoMapper;
using IIG.Application.Data;
using IIG.Application.Models;
using IIG.Application.Models.KeycodeChooses;
using IIG.Application.Models.KeyCodes;
using IIG.Application.Models.MockTestSection;
using IIG.Application.Services.Redis;
using IIG.Core.Base;
using IIG.Core.Common.ConfigureModels;
using IIG.Core.Common.Enums;
using IIG.Core.Common.ErrorHandling;
using IIG.Core.Common.MongoDataModels;
using IIG.Core.Common.MongoDataModels.Keycodes;
using IIG.Core.Common.MongoDataModels.MockTests;
using IIG.Core.Entities;
using IIG.Core.Enums;
using IIG.Core.Helpers;
using IIG.Core.Providers.Caching;
using IIG.Core.Providers.Interfaces;
using IIG.Core.Providers.MongoDbProvider.Infrastructure;
using IIG.Core.Services;
using LinqKit;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StackExchange.Redis;
using static IIG.Core.Common.ConfigureModels.Constants;

namespace IIG.Application.Services;

public class KeyCodeAnswerService : IKeyCodeAnswerService
{
    private readonly IMapper _mapper;
    private readonly IHttpRequestService _httpRequestService;
    private readonly IAppFactory _appFactory;
    private readonly IMongoGenericRepository<MgQuestionModel> _mongoQuestionRepos;
    private readonly IAssessmentService _assessmentService;
    private readonly IKeyCodeAnswerDA _keyCodeAnswerDA;
    private readonly IMockTestKeyCodeDA _mockTestKeyCodeDA;
    //private readonly IMockTestSectionDA _mockTestSectionDA;
    private readonly IMockTestDA _mockTestDA;
    private readonly IMongoGenericRepository<MgMockTestKeyCodeModel> _mongoMockTestKeyCodeRepos;
    private readonly ILogger<KeyCodeAnswerService> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;
    //private readonly INotificationBiz _notificationBiz;
    //private readonly IMockTestKeyCodeDA _iMockTestKeyCodeDA;
    //private readonly IEmailProvider _emailService;
    //private readonly IHttpClientFactory _httpClientFactory;
    //private readonly IFileUploaderService _fileUploaderService;
    private readonly IMongoGenericRepository<MgKeyCodeAnswerModel> _mongoKeyCodeAnswerService;
    private readonly IMockTestKeyCodeRedisDataService _mockTestKeyCodeRedisDataService;
    private readonly IMockTestRedisDataService _mockTestRedisDataService;
    private readonly IRedisGenericFactory _redisGenericFactory;

    //private readonly IRabitMQProducer _rabbitMQProducer;
    private readonly IDistributedCacheProvider _distributedCacheProvider;

    //private readonly string _tfcAPI = Utils.GetConfig("TFC:URL");

    public KeyCodeAnswerService(IMapper mapper,
        IHttpRequestService httpRequestService,
        IAppFactory appFactory,
IMongoGenericRepository<MgQuestionModel> mongoQuestionRepos ,
        IAssessmentService assessmentService,
        IKeyCodeAnswerDA keyCodeAnswerDA,
        IMockTestKeyCodeDA mockTestKeyCodeDA,
        //IMockTestSectionDA mockTestSectionDA,
        IMockTestDA mockTestDA,
       IMongoGenericRepository<MgMockTestKeyCodeModel> mongoMockTestKeyCodeRepos,
        ILogger<KeyCodeAnswerService> logger,
        IHttpContextAccessor httpContextAccessor,
         //        INotificationBiz notificationBiz,
         //        IMockTestKeyCodeDA iMockTestKeyCodeDA,
         //        IEmailProvider emailService
         //,
         //        IHttpClientFactory httpClientFactory,
         //        IFileUploaderService fileUploaderService,
         IMongoGenericRepository<MgKeyCodeAnswerModel> mongoKeyCodeAnswerService,
         IMockTestKeyCodeRedisDataService mockTestKeyCodeRedisDataService,
        IMockTestRedisDataService mockTestRedisDataService,
        IRedisGenericFactory redisGenericFactory,
        //IRabitMQProducer rabbitMQProducer,
        IDistributedCacheProvider distributedCacheProvider)
    {
        _mongoKeyCodeAnswerService = mongoKeyCodeAnswerService;
        //_mongoQuestionService = mongoQuestionService;
        _mapper = mapper;
        _httpRequestService = httpRequestService;
        _appFactory = appFactory;
        _assessmentService = assessmentService;
        _keyCodeAnswerDA = keyCodeAnswerDA;
        _mockTestKeyCodeDA = mockTestKeyCodeDA;
        //_mockTestSectionDA = mockTestSectionDA;
        _mockTestDA = mockTestDA;
        _mongoMockTestKeyCodeRepos = mongoMockTestKeyCodeRepos;
        _mongoQuestionRepos = mongoQuestionRepos;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
        //_notificationBiz = notificationBiz;
        //_iMockTestKeyCodeDA = iMockTestKeyCodeDA;
        //_emailService = emailService;
        //_httpClientFactory = httpClientFactory;
        //_fileUploaderService = fileUploaderService;
        _mockTestKeyCodeRedisDataService = mockTestKeyCodeRedisDataService;
        _mockTestRedisDataService = mockTestRedisDataService;
        _redisGenericFactory = redisGenericFactory;
        //_rabbitMQProducer = rabbitMQProducer;
        _distributedCacheProvider = distributedCacheProvider;
    }

    public async Task SubmitMockTestAsync(string keyCode)
    {
        _logger.LogInformation($"Start SubmitMockTestAsync for keyCode: {keyCode}");
        await SubmitMockTestIternalAsync(keyCode);

        // create and send notification
        // await CreateAndSendNotificationAsync(keyCode);
        var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(Constants.ClaimTypes.UserId)?.Value?.ToGuid();
        //await CreateAndSendNotificationAsync(keyCode, userId);

        // delete cookies
        if (_httpContextAccessor.HttpContext != null && _httpContextAccessor.HttpContext.Request.Cookies[Constants.RequestHeaderKey.KeyCodeCookieKey] != null)
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Delete(Constants.RequestHeaderKey.KeyCodeCookieKey);
        }
    }

    private async Task SubmitForSpeakingWriting(string keyCode)
    {
        var qnType = await (from qn in _appFactory.Repository<Questionnaire>().GetAll()
                            join mtq in _appFactory.Repository<MocktestPartQuestionnaire>().GetAll() on qn.Id equals mtq.QuestionnaireId
                            join mp in _appFactory.Repository<MocktestPart>().GetAll() on mtq.MocktestPartId equals mp.Id
                            join ms in _appFactory.Repository<MocktestSection>().GetAll() on mp.MocktestSectionId equals ms.Id
                            join mtk in _appFactory.Repository<MocktestKeyCode>().GetAll() on ms.MocktestId equals mtk.MocktestId
                            //join cs in _iIGLmsdbContext.CourseScorings.AsNoTracking() on mtk.Code equals cs.KeyCode
                            where mtk.Code == keyCode && ((EQuestionnaireType)qn.Type == EQuestionnaireType.Record || (EQuestionnaireType)qn.Type == EQuestionnaireType.ReadTextALoud || (EQuestionnaireType)qn.Type == EQuestionnaireType.Writing)
                            select new { qn.Type, SectionAI = ms.IsMarkByAi, PartAI = mp.IsMarkByAi, QuestionnaireAI = mtq.IsMarkByAi }
                          ).ToListAsync();
        if (!qnType.Any())
        {
            return;
        }
        var existed = await _appFactory.Repository<CourseScoring>().GetAll().AsNoTracking().AnyAsync(x => x.KeyCode == keyCode);
        if (existed)
        {

            _logger.LogError($"CourseScorings already exist keycode = {keyCode}");
            return;
        }
        var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(Constants.ClaimTypes.UserId)?.Value?.ToGuid();

        //get userId from db insted of context for cronjob able to run
        var responseCheckData = await GetResponseCheckMocktest(keyCode, true);

        var currentKeyCodeWebUser = await _appFactory.Repository<MocktestKeyCode>().GetAll().AsNoTracking().Where(x => x.Code == keyCode).Select(x => x.WebUserId)
                                              .FirstOrDefaultAsync();

        var mockTestKeyCodeDto = await GetMockTestKeyCodeAsync(keyCode);
        string mockTestName = mockTestKeyCodeDto?.MockTestName;

        //Clear old data
        await _appFactory.Repository<CourseScoring>().DeleteAsync(x => x.KeyCode == keyCode);
        await _appFactory.UnitOfWorkManager.Current.SaveChangesAsync();

        if (responseCheckData?.Any() == true)
        {
            List<SpeakingWritingChoose> userChooses = new();
            //var coureScoring = await _iIGLmsdbContext.CourseScorings.IgnoreQueryFilters().Where(x => x.KeyCode == keyCode).ExecuteUpdateAsync(x => x.SetProperty(c => c.IsLatest, false).SetProperty(c => c.UpdatedAt, DateTime.UtcNow));

            var questionnaires = responseCheckData.SelectMany(x => x.Questions).Select(x => x.QuestionnaireId).Distinct().ToList();
            CourseScoring latestCourseScoring = new()
            {
                Id = Guid.NewGuid(),
                CourseScoringType = ECourseScoringType.MockTest,
                Status = ECourseScoringStatus.WaitingScoring,
                KeyCode = keyCode,
                SubmittedDate = DateTime.UtcNow,
                ScoringDeadline = DateTime.UtcNow.AddDays(7),
                Name = $"{keyCode} - {mockTestName}",
                IsLatest = true,
            };

            if (userId != null)
            {
                latestCourseScoring.WebUserId = userId;
            }
            else if (currentKeyCodeWebUser != null)
            {
                latestCourseScoring.WebUserId = currentKeyCodeWebUser;
            }

            await _appFactory.Repository<CourseScoring>().InsertAsync(latestCourseScoring);

            foreach (var section in responseCheckData)
            {
                var no = 1;
                for (int i = 0; i < section.Questions.Count; i++)
                {
                    section.Questions[i].No = no++;
                }
            }

            foreach (var item in responseCheckData.SelectMany(x => x.Questions))
            {
                var entity = new SpeakingWritingChoose
                {
                    CourseScoringId = latestCourseScoring.Id,

                    Id = Guid.NewGuid(),
                    QuestionId = item.Id,
                    QuestionnaireId = item.QuestionnaireId,
                    RecordingFileId = item.RecordingFileId,
                    MocktestPartId = item.MocktestPartId,
                    WritingAnswer = item.AnswerText,
                    ScoringType = EScoringAssigneeType.Course,
                    No = item.No,
                    //RecordFileUrl = item.RecordingFileUrl

                };

                userChooses.Add(entity);
            }

            await _appFactory.Repository<SpeakingWritingChoose>().InsertRangeAsync(userChooses);
            await _appFactory.UnitOfWorkManager.Current.SaveChangesAsync();

            //assign for AI if marked & change scoring status
            if (qnType.Any(x => (x.SectionAI.HasValue && x.SectionAI.Value) || (x.PartAI.HasValue && x.PartAI.Value) || (x.QuestionnaireAI.HasValue && x.QuestionnaireAI.Value)))
            {

                ScoringAssignee scoringAssignee = new()
                {
                    Id = Guid.NewGuid(),
                    CourseScoringId = latestCourseScoring.Id,
                    ScoringAssigneeType = EScoringAssigneeType.Course,
                    AssigneeId = Guid.Empty,//AI
                };

                await _appFactory.Repository<ScoringAssignee>().InsertAsync(scoringAssignee);

                latestCourseScoring.Status = ECourseScoringStatus.Scoring;
                latestCourseScoring.ActiveScoringAssignId = Guid.Empty;

                _appFactory.Repository<CourseScoring>().Update(latestCourseScoring);
            }


            //if (transactionResult)
            //{
            //    BackgroundJob.Enqueue<CourseTestBiz>(x => x.BackgroundJobUpdateSWChooseFileUrl(userChooses));
            //}
        }
    }


    public async Task DeleteDataAsync(string keyCode)
    {
        try
        {
            await _mockTestKeyCodeRedisDataService.DeleteKeyCodeAsync(keyCode);

            //Send to rabbitMQ to execute update mongoDB and SQL

            //_rabbitMQProducer.SendMessage(new RabbitMQMockTestActionModel()
            //{
            //    Action = RabbitMQMockTestAction.MockTestKeyCode_Delete,
            //    KeyCode = keyCode,
            //}, Constants.RabbitMQ.TopicMockTestRedisToMongo, Constants.RabbitMQ.PushMessageMockTestRedisToMongoRoutingkey);
        }
        catch (Exception e)
        {
            _logger.LogError($"DeleteDataAsync - Exception = {e.Message}, InnerException = {e.InnerException?.Message}");

        }
    }

    public async Task SubmitMockTestIternalAsync(string keyCode)
    {
        try
        {
            MockTestKeyCodeBasicDto mockTestKeyCodeBasicDto = await ValidateBeforeSubmitMockTestAsync(keyCode);

            IEnumerable<MgKeyCodeAnswerModel> listMgAnsweredData = await _mockTestKeyCodeRedisDataService.GetKeyCodeAnswerAsync(keyCode);
            if (listMgAnsweredData == null)
                listMgAnsweredData = await _mongoKeyCodeAnswerService.FilterAsync(x => x.KeyCode == keyCode);


            //For new S&W
            await SubmitForSpeakingWriting(keyCode);
            if (listMgAnsweredData == null || !listMgAnsweredData.Any())
            {

                _logger.LogError($"----------There's no answered data for keycode {keyCode}--------");

                await SubmitMockTestWhenUserHaveNotAnswered(keyCode, mockTestKeyCodeBasicDto);
            }
            else
            {
                _logger.LogError($"SUBMIT Mocktest KeyCode {keyCode} with keyCodeData:{System.Text.Json.JsonSerializer.Serialize(mockTestKeyCodeBasicDto)} and answerData: {System.Text.Json.JsonSerializer.Serialize(listMgAnsweredData)}");
                await SubmitMockTestWhenUserAnswered(keyCode, listMgAnsweredData, mockTestKeyCodeBasicDto);

                //if (listMgAnsweredData.Any(x => x.QuestionnaireType == EQuestionnaireType.Record || x.QuestionnaireType == EQuestionnaireType.ReadTextALoud || x.QuestionnaireType == EQuestionnaireType.Writing))
                //{

                //}

            }

            // call tfc
            //string apiUrl = $"{_tfcAPI}/registrationscore/sync?keyCode={keyCode}";
            //using var client = new HttpClient();
            //await client.GetAsync(apiUrl);


            // send email
            //_rabbitMQProducer.SendMessage(keyCode, Constants.RabbitMQ.KeyCodeAnswerService.SendResultExamEmail.TopicName, Constants.RabbitMQ.KeyCodeAnswerService.SendResultExamEmail.RoutingKey);
            //BackgroundJob.Enqueue<KeyCodeAnswerService>((x) => x.SendResultExamEmail(keyCode));

            //delete data
            await DeleteDataAsync(keyCode);

            //BackgroundJob.Enqueue<KeyCodeAnswerService>((x) => x.DeleteDataAsync(keyCode));
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error occured while execute SubmitMockTestIternalAsync");
            throw;
        }

    }

    //public async Task SubmitMockTestIternaForOldDatalAsync()
    //{
    //    var listKeyCode = await _mockTestKeyCodeDA.GetListMockTestKeyCodeHaveNotTotalCorrectAnswer();
    //    //var listKeyCode = new List<string> { "ZCBB8T" };
    //    foreach (var keyCode in listKeyCode)
    //    {
    //        // Validate keycode exist
    //        var isKeyCodeAlreadyExisted = await _mockTestDA.CheckKeyCodeAlreadyExistedAsync(keyCode);
    //        if (!isKeyCodeAlreadyExisted)
    //        {
    //            continue;
    //        }

    //        var languageCode = await _httpRequestService.GetCurrentLanguageCode();
    //        var mockTestPublishedAt = await _mockTestKeyCodeDA.GetMockTestPublishedAtByKeyCode(keyCode);
    //        var mockTestKeyCodeDto = await _mockTestKeyCodeDA.GetDetailByKeyCode(keyCode, languageCode, mockTestPublishedAt);
    //        var listMgAnsweredData = await _mockTestKeyCodeDA.GetMockTestKeyCodeChoose(keyCode);
    //        if (listMgAnsweredData == null || !listMgAnsweredData.Any())
    //        {
    //            var keyCodeResultDto = await GenerateKeyCodeResult(mockTestKeyCodeDto, new List<KeyCodeChooseResponse>());

    //            try
    //            {
    //                await _keyCodeAnswerDA.UpdateKeyCodeResult(keyCodeResultDto);
    //            }
    //            catch (Exception e)
    //            {
    //                _logger.LogError($"There's error in SubmitMockTestIternaForOldDatalAsync, Exception = {e.Message}, InnerException ={e.InnerException?.Message}");
    //            }
    //        }
    //        else
    //        {
    //            var mgQuestionnaireIds = listMgAnsweredData.Select(x => x.QuestionnaireId).Distinct().ToList();
    //            var mgQuestionIds = listMgAnsweredData.Select(y => y.QuestionId).Distinct().ToList();

    //            var mgQuestions = await _mongoQuestionService.FilterAsync(x => mgQuestionnaireIds.Contains(x.QuestionnaireId) && mgQuestionIds.Contains(x.QuestionId));

    //            var mgAnsweredGrp = listMgAnsweredData.GroupBy(x => new
    //            {
    //                Keycode = x.KeyCode,
    //                x.MockTestSectionId,
    //                x.MockTestPartId
    //            });

    //            var listKeyCodeChose = new List<KeyCodeChooseResponse>();
    //            foreach (var itemAnsweredGrp in mgAnsweredGrp)
    //            {
    //                var listChooseBaseModelRequest = _mapper.Map<IEnumerable<ChooseBaseModelRequest>>(itemAnsweredGrp.ToList());

    //                var listChooseBaseModelResponse = _assessmentBiz.VerifyAnswerAsync(mgQuestions, listChooseBaseModelRequest);
    //                var verifyAnswerRes = _mapper.Map<IEnumerable<KeyCodeChooseResponse>>(listChooseBaseModelResponse);
    //                if (!verifyAnswerRes.Any()) continue;

    //                verifyAnswerRes.ForEach(x =>
    //                {
    //                    x.KeyCode = itemAnsweredGrp.Key.Keycode;
    //                    x.MockTestPartId = itemAnsweredGrp.Key.MockTestPartId;
    //                    x.MockTestSectionId = itemAnsweredGrp.Key.MockTestSectionId;
    //                });

    //                listKeyCodeChose.AddRange(verifyAnswerRes);
    //            }

    //            var keyCodeResultDto = await GenerateKeyCodeResult(mockTestKeyCodeDto, listKeyCodeChose);
    //            try
    //            {
    //                await _keyCodeAnswerDA.UpdateKeyCodeResult(keyCodeResultDto);
    //            }
    //            catch (Exception e)
    //            {
    //                _logger.LogError($"There's error in SubmitMockTestIternaForOldDatalAsync, Exception = {e.Message}, InnerException ={e.InnerException?.Message}");
    //            }
    //        }
    //    }

    //}

    public async Task SubmitMultipleMockTestAsync(List<string> keyCodes)
    {
        foreach (var keyCode in keyCodes)
        {
            try
            {
                await SubmitMockTestIternalAsync(keyCode);
            }
            catch (Exception ex)
            {
                _logger.LogError($"There's error in SubmitMultipleMockTestAsync keycode: {keyCode}, Exception = {ex.Message}, InnerException ={ex.InnerException?.Message}");

            }

        }
    }

    public async Task DeleteMultipleMockTestAsync(List<string> keyCodes)
    {
        try
        {
            foreach (var keyCode in keyCodes)
            {
                await DeleteDataAsync(keyCode);
            }

            // delete in sql
            await _keyCodeAnswerDA.DeleteManyAsync(keyCodes);
        }
        catch (Exception e)
        {
            _logger.LogError($"DeleteMultipleMockTestAsync - Exception = {e.Message}, InnerException = {e.InnerException?.Message}");
        }
    }

    //public async Task SendResultExamEmail(string code)
    //{
    //    try
    //    {
    //        var bodyEmail = await _keyCodeAnswerDA.SendResultExamEmail(code);
    //        if (bodyEmail == null) return;
    //        await _emailService.SendEmailAsync(bodyEmail);
    //    }
    //    catch (Exception e)
    //    {
    //        _logger.LogError($"There's error in SendResultExamEmail, Exception = {e.Message}, InnerException ={e.InnerException?.Message}");
    //    }
    //}

    private async Task SubmitMockTestWhenUserAnswered(string keyCode, IEnumerable<MgKeyCodeAnswerModel> listMgAnsweredData, MockTestKeyCodeBasicDto mockTestKeyCodeBasicDto)
    {
        if (IsGuid(keyCode) == true)
        {
            if (mockTestKeyCodeBasicDto.SubmittedDate != null)
            {
                throw new InvalidOperationException("Bài thi đã được nộp trước đó.");
            }
        }
        var mgQuestionnaireIds = listMgAnsweredData.Select(x => x.QuestionnaireId);
        var mgQuestionIds = listMgAnsweredData.Select(y => y.QuestionId);
        var mgQuestions = await _mongoQuestionRepos.FilterAsync(x => mgQuestionnaireIds.Contains(x.QuestionnaireId) && mgQuestionIds.Contains(x.QuestionId));
        _logger.LogError($"SUBMIT Mocktest KeyCode {keyCode} mgQuestions: {System.Text.Json.JsonSerializer.Serialize(mgQuestions)}");

        var mgAnsweredGrp = listMgAnsweredData.GroupBy(x => new
        {
            Keycode = x.KeyCode,
            x.MockTestSectionId,
            x.MockTestPartId
        });

        var listKeyCodeChose = new List<KeyCodeChooseResponse>();
        foreach (var itemAnsweredGrp in mgAnsweredGrp)
        {
            var listChooseBaseModelRequest = _mapper.Map<IEnumerable<ChooseBaseModelRequest>>(itemAnsweredGrp.ToList());

            var listChooseBaseModelResponse = _assessmentService.VerifyAnswerAsync(mgQuestions, listChooseBaseModelRequest);
            var verifyAnswerRes = _mapper.Map<IEnumerable<KeyCodeChooseResponse>>(listChooseBaseModelResponse);
            if (!verifyAnswerRes.Any()) continue;

            verifyAnswerRes.ForEach(x =>
            {
                x.KeyCode = itemAnsweredGrp.Key.Keycode;
                x.MockTestPartId = itemAnsweredGrp.Key.MockTestPartId;
                x.MockTestSectionId = itemAnsweredGrp.Key.MockTestSectionId;
            });

            listKeyCodeChose.AddRange(verifyAnswerRes);
        }

        _logger.LogError($"SUBMIT Mocktest KeyCode {keyCode} before GenerateKeyCodeResult with keyCodeData:{System.Text.Json.JsonSerializer.Serialize(mockTestKeyCodeBasicDto)} and answerData: {System.Text.Json.JsonSerializer.Serialize(listKeyCodeChose)}");

        var keyCodeResultDto = await GenerateKeyCodeResult(mockTestKeyCodeBasicDto, listKeyCodeChose);

        _logger.LogError($"SUBMIT Mocktest KeyCode {keyCode} after GenerateKeyCodeResult with data:{System.Text.Json.JsonSerializer.Serialize(keyCodeResultDto)}");
        try
        {
            if (!listMgAnsweredData.Any(x => x.QuestionnaireType == EQuestionnaireType.Record || x.QuestionnaireType == EQuestionnaireType.ReadTextALoud || x.QuestionnaireType == EQuestionnaireType.Writing))
            {

                await _keyCodeAnswerDA.BatchInsertKeyCodeChose(listKeyCodeChose);
                await _keyCodeAnswerDA.InsertKeyCodeResult(keyCodeResultDto);
            }
            await _keyCodeAnswerDA.UpdateSubmittedDateKeyCode(keyCode);
        }
        catch (Exception e)
        {
            _logger.LogError($"There's error in SubmitMockTestWhenUserAnswered, Exception = {e.Message}, InnerException = {e.InnerException?.Message} ");
        }
    }




    private async Task SubmitMockTestWhenUserHaveNotAnswered(string keyCode, MockTestKeyCodeBasicDto mockTestKeyCodeBasicDto)
    {
        var keyCodeResultDto = await GenerateKeyCodeResult(mockTestKeyCodeBasicDto, new List<KeyCodeChooseResponse>());
        try
        {
            await _keyCodeAnswerDA.InsertKeyCodeResult(keyCodeResultDto);
        }
        catch (Exception e)
        {
            _logger.LogError($"There's error in InsertKeyCodeResult, Exception = {e.Message}, InnerException ={e.InnerException?.Message}");
            if (!e.Message.Contains("Violation of PRIMARY KEY constraint 'PK_keycode_result'"))
            {
                throw new Exception("Cannot insert keycode result");
            }
        }

        await _keyCodeAnswerDA.UpdateSubmittedDateKeyCode(keyCode);
    }

    public async Task<MockTestKeyCodeBasicDto> ValidateBeforeSubmitMockTestAsync(string keyCode, bool isSubmitted = false)
    {
        // Validate keycode exist
        var isKeyCodeAlreadyExisted = await _mockTestDA

            .CheckKeyCodeAlreadyExistedAsync(keyCode);
        if (!isKeyCodeAlreadyExisted)
        {
            throw new ApiValidationException(WebConstants.ValidationMessages.MockTestMessage.KeyCodeNotFoundKey, nameof(keyCode));
        }

        var languageCode = await _httpRequestService.GetCurrentLanguageCode();
        var mockTestPublishedAt = await _mockTestKeyCodeDA.GetMockTestPublishedAtByKeyCode(keyCode);
        var mockTestKeyCodeDto = await _mockTestKeyCodeDA.GetDetailByKeyCode(keyCode, languageCode, mockTestPublishedAt);
        if (!isSubmitted)
        {
            // Validate keycode is already finished
            if (mockTestKeyCodeDto.SubmittedDate is not null)
            {
                // delete cookies
                if (_httpContextAccessor.HttpContext.Request.Cookies[Constants.RequestHeaderKey.KeyCodeCookieKey] != null)
                {
                    _httpContextAccessor.HttpContext.Response.Cookies.Delete(Constants.RequestHeaderKey.KeyCodeCookieKey);
                }
                throw new ApiValidationException(String.Format(WebConstants.ValidationMessages.MockTestKeyCodeMessage.KeyCodeAlreadySubmitted, keyCode), nameof(mockTestKeyCodeDto.SubmittedDate));
            }

        }

        return mockTestKeyCodeDto;
    }

    static bool IsGuid(string input)
    {
        return Guid.TryParse(input, out _);
    }

    private async Task<KeyCodeResultDto> GenerateKeyCodeResult(MockTestKeyCodeBasicDto mockTestKeyCodeBasicDto, IEnumerable<KeyCodeChooseResponse> listKeyCodeChose)
    {
        var sectionGrp = listKeyCodeChose.GroupBy(x => x.MockTestSectionId);
        var sectionCorrectAnswerBasic = new List<SectionCorrectAnswerBasic>();
        foreach (var item in sectionGrp)
        {
            sectionCorrectAnswerBasic.Add(new SectionCorrectAnswerBasic()
            {
                MockTestSectionId = item.Key,
                NumberOfCorrectAnswer = item.Count(x => x.CorrectAnswer)
            });
        }


        var publishDate = DateTime.UtcNow;
        //var mgMockTest = await _mockTestDumpDataToMongoDbBiz.GetOrDumpAsync(mockTestKeyCodeBasicDto.MockTestId);
        var mgMockTest = await GetMockTest(mockTestKeyCodeBasicDto.MockTestId);
        if (!IsGuid(mockTestKeyCodeBasicDto.Code))
        {
            publishDate = mgMockTest.GeneralInfo.PublishedAt;
        }

        _logger.LogError($"GenerateKeyCodeResult - mgMockTest: {System.Text.Json.JsonSerializer.Serialize(mgMockTest)}");

        IEnumerable<MockTestSectionCorrectAnswerDto> mockTestSectionCorrectAnswerDtos =
        await _mockTestKeyCodeDA.GetMockTestSectionCorrectAnswers(mgMockTest.MockTestId, sectionCorrectAnswerBasic, DateTime.UtcNow.ToLocalTime(), publishDate.ToLocalTime());
        var keyCodeResult = new KeyCodeResultDto()
        {
            KeyCode = mockTestKeyCodeBasicDto.Code,
            TotalQuestion = mgMockTest.MockTestSections.Sum(x => x.NumberOfQuestions),
            TotalCorrectAnswer = listKeyCodeChose.Count(x => x.CorrectAnswer),
            ComponentsDetails = JsonConvert.SerializeObject(mockTestSectionCorrectAnswerDtos
                .Select(x => new SectionScoreBasicDto()
                {
                    SectionId = x.SectionId,
                    Score = x.ExactScore,
                    NumberOfCorrectAnswer = sectionCorrectAnswerBasic.FirstOrDefault(y => y.MockTestSectionId == x.SectionId)?.NumberOfCorrectAnswer ?? 0
                }))
        };

        keyCodeResult.RankingScore = mockTestKeyCodeBasicDto.MockTestScoreType switch
        {
            EMockTestScoreType.CorrectScore => $"{mockTestSectionCorrectAnswerDtos.Sum(x => x.ExactScore)}",

            EMockTestScoreType.ScoreRange => $"{mockTestSectionCorrectAnswerDtos.Sum(x => x.FromScore)}-{mockTestSectionCorrectAnswerDtos.Sum(x => x.ToScore)}",

            EMockTestScoreType.ITPScoreRange => $"{(int)Math.Round(mockTestSectionCorrectAnswerDtos.Average(x => x.FromScore) * 10)}-{(int)Math.Round(mockTestSectionCorrectAnswerDtos.Average(x => x.ToScore) * 10)}",

            _ => throw new ArgumentOutOfRangeException(WebConstants.ValidationMessages.MockTestKeyCodeMessage.MockTestScoreTypeIsNotValid)
        };

        return keyCodeResult;
    }

    //public async Task CreateAndSendNotificationAsync(string keyCode, Guid? userId)
    //{

    //    if (userId == null) return;

    //    // get language code
    //    var languageCode = await _httpRequestService.GetCurrentLanguageCode();

    //    // get mock test published date + name + course id
    //    var mockTestKeyCodeInfo = await _mockTestKeyCodeDA.GetKeyCodeDetailByKeyCodeAsync(keyCode);
    //    if (mockTestKeyCodeInfo == null)
    //    {
    //        throw new Exception(WebConstants.ValidationMessages.MockTestMessage.KeyCodeNotFoundKey);
    //    }
    //    var mockTestPublishedAt = mockTestKeyCodeInfo.MocktestPublishedAt;
    //    var mockTestDetail = await _mockTestKeyCodeDA.GetDetailByKeyCode(keyCode, languageCode, mockTestPublishedAt);
    //    var mockTestName = mockTestDetail.MockTestName;
    //    var mockTestId = mockTestDetail.MockTestId;
    //    bool isSWType = await _iIGLmsdbContext.MocktestSections
    //    .AsNoTracking()
    //    .Where(x => x.MocktestId == mockTestId)
    //    .AllAsync(x => (EMockTestSectionType)x.Type == EMockTestSectionType.RecordNonstop || (EMockTestSectionType)x.Type == EMockTestSectionType.WritingNonstop);

    //    var courseId = mockTestKeyCodeInfo.CourseId;
    //    var url = $"/thi-thu-online/exam/{keyCode}/result{(isSWType ? $"?isSuccessful=true&keyCode={keyCode}&notiType={Constants.FirebaseNotification.NavigateType5}" : $"?keyCode={keyCode}&notiType={Constants.FirebaseNotification.NavigateType5}")}";
    //    var message = $"<p>Chúc mừng bạn đã hoàn thành bài thi <b>{mockTestName}</b>. Vui lòng truy cập <b class='underline decoration-solid'>tại đây</b> để theo dõi và cập nhật ngay thông tin về kết quả bài thi nhé.</p>";

    //    var notificationModel = new NotificationInsertOrUpdateModel
    //    {
    //        Name = "Thông báo khi người dùng submit key-code",
    //        DatetimeTrigger = DateTime.UtcNow,
    //        Translation = new List<NotificationTranslationDto>
    //        {
    //            new()
    //            {
    //                Title = "Thông báo mới",
    //                LanguageCode = Constants.LanguageTags.TiengViet,
    //                Description = message,
    //                LinkUrl = url
    //            }
    //        }
    //    };

    //    await _notificationBiz.CreateAndSendNotificationToWebUserAsync(notificationModel, userId.Value);
    //}

    public async Task<List<ResponseCheckMocktestModel>> GetResponseCheckMocktest(string keyCode, bool isSubmitted = false)
    {
        //var mcktestKeyCodeInfo = await ValidateBeforeSubmitMockTestAsync(keyCode, isSubmitted);
        var listMgAnsweredData = await GetMockTestKeyCodeAnswer(keyCode);
        //var mocktestInfo = await _iIGLmsdbContext.MocktestKeyCodes
        //                                        .Include(x => x.Mocktest)
        //                                            .ThenInclude(x => x.MocktestTranslations)
        //                                        .FirstOrDefaultAsync(x => x.Code == keyCode);

        var mocktestInfo = await GetMockTestKeyCodeAsync(keyCode);
        if (mocktestInfo == null)
        {
            throw new ApiNotFoundException(Constants.ValidationMessages.WebUserMessage.MocktestKeyCodeNotFound);
        }

        //var mockTestInfo = mocktestInfo.Mocktest.MocktestTranslations.FirstOrDefault(x => x.LanguageCode == Constants.LanguageTags.TiengViet);

        var mockTestSection = await GetMockTestSections(mocktestInfo.MockTestId);


        var result = mockTestSection
                    .Select(x => new ResponseCheckMocktestModel
                    {
                        Id = x.Id,
                        MocktestName = x.Name,
                        TestName = mocktestInfo?.MockTestName,
                        MocktestType = (EMockTestSectionType)x.Type,
                        MocktestSectionId = x.Id,
                        SortOrder = x.SortOrder,
                        Questions = x.MocktestParts
                                .OrderBy(mP => mP.SortOrder)
                                .SelectMany(p => p.MocktestPartQuestionnaires
                                                .OrderBy(mQ => mQ.SortOrder)
                                                .SelectMany(pQ => pQ.Questionnaire.Questions
                                                            .OrderBy(q => q.SortOrder)
                                                            .Select(q =>
                                                            {
                                                                var mgItem = listMgAnsweredData.FirstOrDefault(mg =>
                                                                    mg.MockTestSectionId == x.Id &&
                                                                    mg.MockTestPartId == p.Id &&
                                                                    mg.QuestionnaireId == q.QuestionnaireId &&
                                                                    mg.QuestionId == q.Id
                                                                );

                                                                return new ResponseCheckQuestionModel
                                                                {
                                                                    Id = q.Id,
                                                                    MocktestPartId = p.Id,
                                                                    MocktestSectionId = x.Id,
                                                                    QuestionnaireId = q.QuestionnaireId,
                                                                    SortOrder = q.SortOrder,
                                                                    AnswerText = mgItem?.AnswerText,
                                                                    RecordingFileId = mgItem?.RecordingFileId,
                                                                    RecordingTime = q.RecordingTime,
                                                                };
                                                            }))).ToList()
                    });

        if (result.Any(x => x.MocktestType == EMockTestSectionType.RecordNonstop || x.MocktestType == EMockTestSectionType.WritingNonstop))
        {
            return result.Where(x => x.MocktestType == EMockTestSectionType.RecordNonstop || x.MocktestType == EMockTestSectionType.WritingNonstop).ToList();
        }

        return result.ToList();
    }


    private async Task<MgMockTestKeyCodeModel> GetMockTestKeyCodeAsync(string keyCode)
    {
        var mockTestKeyCodeInfo = await _mockTestKeyCodeRedisDataService.GetMockTestKeyCodeAsync(keyCode);
        if (mockTestKeyCodeInfo == null)
            mockTestKeyCodeInfo = await _mongoMockTestKeyCodeRepos.FindByIdAsync(x => x.KeyCode == keyCode);
        return mockTestKeyCodeInfo;
    }

    private async Task<IEnumerable<MgKeyCodeAnswerModel>> GetMockTestKeyCodeAnswer(string keyCode)
    {
        var listMgAnsweredData = await _mockTestKeyCodeRedisDataService.GetKeyCodeAnswerAsync(keyCode);
        if (listMgAnsweredData == null)
            listMgAnsweredData = await _mongoKeyCodeAnswerService.FilterAsync(x => x.KeyCode == keyCode);
        return listMgAnsweredData;
    }

    private async Task<IEnumerable<MockTestSectionForSubmitDto>> GetMockTestSections(Guid mockTestId)
    {
        var redisService = _redisGenericFactory.CreateCollection<MockTestSectionForSubmitDto>();
        var listData = await redisService.Get(mockTestId.ToString());
        if (listData == null)
        {
            listData = _appFactory.Repository<MocktestSection>().GetAll()
                       .Include(x => x.MocktestParts)
                           .ThenInclude(x => x.MocktestPartQuestionnaires)
                               .ThenInclude(x => x.Questionnaire)
                                   .ThenInclude(x => x.Questions)
                   .Where(x => x.MocktestId == mockTestId)
                   .OrderBy(x => x.SortOrder)
                   .Select(p => new MockTestSectionForSubmitDto
                   {
                       Id = p.Id,
                       MockTestId = p.MocktestId,
                       RankingScoreId = p.RankingScoreId,
                       Name = p.Name,
                       SortOrder = p.SortOrder,
                       Type = (EMockTestSectionType)p.Type,
                       NumberOfQuestions = p.NumberOfQuestions,
                       NumberOfTime = p.NumberOfTime ?? 0,
                       MocktestParts = p.MocktestParts.OrderBy(x => x.SortOrder).Select(mp => new MocktestPartForSubmitDto
                       {
                           Id = mp.Id,
                           Name = mp.Name,
                           SortOrder = mp.SortOrder,
                           MocktestPartQuestionnaires = mp.MocktestPartQuestionnaires.OrderBy(x => x.SortOrder).Select(mpq => new MocktestPartQuestionnaireForSubmitDto
                           {
                               SortOrder = mpq.SortOrder,
                               Questionnaire = new QuestionnaireForSubmitDto
                               {
                                   Id = mpq.Questionnaire.Id,
                                   QuestionnaireId = mpq.Questionnaire.Id,
                                   Questions = mpq.Questionnaire.Questions.OrderBy(x => x.SortOrder).Select(q => new QuestionForSubmitDto
                                   {
                                       Id = q.Id,
                                       QuestionnaireId = q.QuestionnaireId,
                                       SortOrder = q.SortOrder,
                                       RecordingTime = q.RecordingTime
                                   }).ToList(),
                               }
                           }).ToList()
                       }).ToList()
                   })
                   .AsEnumerable();
            await redisService.Add(mockTestId.ToString(), listData, (int)(TimeSpan.FromMinutes(1).TotalSeconds));
        }

        return listData;

    }

    private async Task<MgMockTestModel> GetMockTest(Guid id, int expiredInSeconds = 60)
    {
        var mgMockTestModel = await _mockTestRedisDataService.GetMockTest(id);
        return mgMockTestModel;

    }
}