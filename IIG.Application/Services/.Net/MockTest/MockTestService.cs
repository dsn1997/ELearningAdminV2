using AutoMapper;
using IIG.Application.BackgroundJob.Dtos;
using IIG.Application.Data;
using IIG.Application.Models;
using IIG.Application.Models.Keycodes;
using IIG.Application.Models.MockTestKeyCode;
using IIG.Application.Models.Questionnaires;
using IIG.Core.Base;
using IIG.Core.Common.ConfigureModels;
using IIG.Core.Common.Enums;
using IIG.Core.Common.ErrorHandling;
using IIG.Core.Common.MongoDataModels.Keycodes;
using IIG.Core.Common.MongoDataModels.MockTests;
using IIG.Core.Entities;
using IIG.Core.Enums;
using IIG.Core.Providers.RabbitMQProvider;
using IIG.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using static IIG.Core.Common.ConfigureModels.Constants;

namespace IIG.Application.Services;

public class MockTestService : IMockTestService
{
    private readonly IAppFactory _appFactory;
    private readonly IMockTestDA _mockTestDA;
    private readonly IMapper _mapper;
    private readonly IHttpRequestService _httpRequestService;
    private readonly IMockTestKeyCodeDA _mockTestKeyCodeDA;
    //private readonly IFileUploaderService _fileUploaderService;
    private readonly IKeyCodeAnswerService _keyCodeAnswerService;
    private readonly ILogger<MockTestService> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;
    //private readonly AppSettingOptions _appSettingOptions;
    //private readonly ILeftSectionDA _leftSectionDA;
    private readonly IFileService _fileService;
    //private readonly ISWScoringBiz _sWScoringBiz;
    private readonly IMockTestRedisDataService _mockTestDataService;
    private readonly IMockTestKeyCodeRedisDataService _mockTestKeyCodeDataService;
    private readonly IQuestionaireRedisDataService _questionaireDataService;
    //private readonly IMockTestDumpDataToMongoDbBiz _mockTestDumpDataToMongoDbBiz;

    private readonly IRabbitMQProducer _rabbitMQProducer;

    public MockTestService(
                IAppFactory appFactory,
                IMockTestDA mockTestDA,
                IHttpRequestService httpRequestService,
                IMockTestKeyCodeDA mockTestKeyCodeDA,
                //IKeyCodeResultDA keyCodeResultDA,
                IMapper mapper,

                //       IFileUploaderService fileUploaderService, IKeyCodeAnswerService keyCodeAnswerService,
                ILogger<MockTestService> logger,
                IHttpContextAccessor httpContextAccessor,
                       //        IOptions<AppSettingOptions> settingOption,
                       //        ILeftSectionDA leftSectionDA,
                       IFileService fileService,
               //, ISWScoringBiz sWScoringBiz,
               IMockTestRedisDataService mockTestDataService,
               IMockTestKeyCodeRedisDataService mockTestKeyCodeDataService,
               //        IQuestionaireRedisDataService questionaireDataService,
               IRabbitMQProducer rabbitMQProducer
       )
    {
        _appFactory = appFactory;
        _mockTestDA = mockTestDA;
        _httpRequestService = httpRequestService;
        _mockTestKeyCodeDA = mockTestKeyCodeDA;
        //_keyCodeResultDA = keyCodeResultDA;
        _mapper = mapper;
        //_fileUploaderService = fileUploaderService;
        //_keyCodeAnswerService = keyCodeAnswerService;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
        //_appSettingOptions = settingOption.Value;
        //_leftSectionDA = leftSectionDA;
        _fileService = fileService;
        //_sWScoringBiz = sWScoringBiz;
        _mockTestDataService = mockTestDataService;
        _mockTestKeyCodeDataService = mockTestKeyCodeDataService;
        //_questionaireDataService = questionaireDataService;
        _rabbitMQProducer = rabbitMQProducer;
        //_distributedCacheProvider = distributedCacheProvider;
    }

    public async Task<VerifyKeyCodeDto> VerifyKeyCodeAsync(VerifyKeyCodeRequest request)
    {
        var languageCode = await _httpRequestService.GetCurrentLanguageCode();
        var keyCodeInfo = await _mockTestKeyCodeDA
            .GetKeyCodeDetailByKeyCodeAsync(request.Keycode);
        if (keyCodeInfo == null)
        {
            throw new ApiValidationException(WebConstants.ValidationMessages.MockTestMessage.KeyCodeNotFoundKey,
                nameof(request.Keycode));
        }

        var courseScoring = await _mockTestKeyCodeDataService.GetTupleCourseScoring(request.Keycode);
        var isSW = false;

        if (courseScoring == null)
        {
            isSW = await (from mtk in _appFactory.Repository<MocktestKeyCode>().GetAll()
                          where mtk.Code == request.Keycode
                          from ms in _appFactory.Repository<MocktestSection>().GetAll()
                              .Where(x => x.MocktestId == mtk.MocktestId)
                          from mp in _appFactory.Repository<MocktestPart>().GetAll()
                              .Where(x => x.MocktestSectionId == ms.Id)
                          from mtq in _appFactory.Repository<MocktestPartQuestionnaire>().GetAll()
                              .Where(x => x.MocktestPartId == mp.Id)
                          from qn in _appFactory.Repository<Questionnaire>().GetAll()
                              .Where(x => x.Id == mtq.QuestionnaireId &&
                                          (x.Type == (int)EQuestionnaireType.Record ||
                                           x.Type == (int)EQuestionnaireType.ReadTextALoud ||
                                           x.Type == (int)EQuestionnaireType.Writing))
                          select 1
                        ).AnyAsync();
        }
        else
        {
            isSW = courseScoring.Item2;
        }

        if (courseScoring == null || (!courseScoring.Item2 && courseScoring.Item1.Status != ECourseScoringStatus.Scored))
        {
            // Check if test is already submitted
            if (keyCodeInfo.SubmittedDate.HasValue)
                throw new ApiValidationException(WebConstants.ValidationMessages.MockTestKeyCodeMessage.KeyCodeAlreadySubmitted, nameof(request.Keycode), isSW ? "SW" : null);

            // Check if invalid start date
            if (keyCodeInfo.StartDate.HasValue)
            {
                var isValidStartDate = keyCodeInfo.StartDate.Value.Date <= DateTime.UtcNow.Date;
                if (!isValidStartDate)
                    throw new ApiValidationException(WebConstants.ValidationMessages.MockTestMessage.InvalidKeyCodeStartDateKey, nameof(request.Keycode), isSW ? "SW" : null);
            }

            // Check if already pass end date
            var isValidEndDate = keyCodeInfo.EndDate.Date >= DateTime.UtcNow.Date;
            if (!isValidEndDate)
                throw new ApiValidationException(WebConstants.ValidationMessages.MockTestMessage.KeyCodeNotFoundKey,
                    nameof(request.Keycode), isSW ? "SW" : null);

            if (request.MocktestObjectId.HasValue && request.MocktestObjectId != Guid.Empty &&
                request.MocktestTypeId.HasValue && request.MocktestTypeId != Guid.Empty)
            {
                var isValidObjectType = await _mockTestDA.WebVerifyKeycodeObjectType(request);
                if (!isValidObjectType)
                {
                    throw new ApiValidationException(WebConstants.ValidationMessages.MockTestMessage.InvalidKeyCodeKey,
                        nameof(request.Keycode), isSW ? "SW" : null);
                }
            }
        }

        var cookieValue = _httpContextAccessor.HttpContext.Request.Cookies[RequestHeaderKey.KeyCodeCookieKey];
        var mockTestKeyCodeInfo = await _mockTestKeyCodeDataService.GetMockTestKeyCodeAsync(request.Keycode);


        if (courseScoring == null || (!courseScoring.Item2 && courseScoring.Item1.Status != ECourseScoringStatus.Scored))
        {
            // Check if key code is already used in another browser or computer
            if (mockTestKeyCodeInfo != null && cookieValue != null)
            {
                if (mockTestKeyCodeInfo.Cookie.HasValue || !string.IsNullOrEmpty(mockTestKeyCodeInfo.Browser))
                {
                    if (mockTestKeyCodeInfo.Cookie.ToString() != cookieValue
                    || mockTestKeyCodeInfo.Browser?.ToLower() != request.Browser?.ToLower())
                    {
                        _logger.LogError("VerifyKeyCodeAsync mockTestKeyCodeInfo.Cookie{0}-cookieValue{1};mockTestKeyCodeInfo.Browser{2}-request.Browser{3}", mockTestKeyCodeInfo.Cookie, cookieValue, mockTestKeyCodeInfo.Browser, request.Browser);
                        throw new ApiValidationException(WebConstants.ValidationMessages.MockTestKeyCodeMessage.KeyCodeInUseKey, request.Keycode, isSW ? "SW" : null);
                    }
                }

            }

            if (keyCodeInfo.Cookie.HasValue && keyCodeInfo.Cookie.Value.ToString() != cookieValue)
            {
                _logger.LogError("VerifyKeyCodeAsync keyCodeInfo.Cookie.Value{0}-cookieValue{1}", keyCodeInfo.Cookie.Value, cookieValue);
                throw new ApiValidationException(WebConstants.ValidationMessages.MockTestKeyCodeMessage.KeyCodeInUseKey, request.Keycode, isSW ? "SW" : null);
            }

            // Check if there is already a test in browser
            if (!string.IsNullOrEmpty(cookieValue) && !(keyCodeInfo.IsAutoGenerate.HasValue && keyCodeInfo.IsAutoGenerate.Value))
            {
                var existedMocktestKeyCode = await _mockTestKeyCodeDA
                    .GetKeyCodeDetailByCookieAsync(Guid.Parse(cookieValue));
                if (existedMocktestKeyCode != null && !existedMocktestKeyCode.SubmittedDate.HasValue && existedMocktestKeyCode.Code != request.Keycode)
                {
                    throw new ApiValidationException(
                        WebConstants.ValidationMessages.MockTestMessage.UserNeedToFinishCurrentSectionMocktest, null, isSW ? "SW" : null);
                }
            }
        }

        var response = await _mockTestDA.VerifyKeyCodeAsync(request.Keycode, languageCode);
        var mgMockTestModel = await _mockTestDataService.GetMockTest(response.MockTestId);
        var totalTime = mgMockTestModel.MockTestSections.Sum(s => s.NumberOfTime);

        response.TotalSeconds = totalTime;
        response.TotalQuestions = mgMockTestModel.MockTestSections.Sum(s => s.NumberOfQuestions);
        response.MocktestName = mgMockTestModel?.GeneralInfo?.Names?.Where(p=>p.LanguageCode == languageCode).Select(p=>p.Name).FirstOrDefault();

        if (response.StartedDoingExamDate.HasValue && response.StartedDoingExamDate.Value.AddDays(1) <= DateTime.UtcNow && !response.SubmittedDate.HasValue)
        {
            await _keyCodeAnswerService.SubmitMockTestAsync(request.Keycode);
            response.Status = EMockTestKeyCodeStatus.Used;

        }

        if (courseScoring?.Item1 != null)
        {

            response.CourseScoringInfo = new CourseScoringModel
            {
                CourseScoringId = courseScoring.Item1.Id,
                ScoringStatus = (ECourseScoringStatus?)courseScoring.Item1.Status
            };
        }

        return response;
    }

    #region commit

    //public async Task<MockTestResultDto> ViewMockTestResultAsync(string keyCode)
    //{
    //    var isKeyCodeAlreadyExisted = await _mockTestDA
    //        .CheckKeyCodeAlreadyExistedAsync(keyCode);
    //    if (!isKeyCodeAlreadyExisted)
    //    {
    //        throw new ApiValidationException(WebConstants.ValidationMessages.MockTestMessage.KeyCodeNotFoundKey, nameof(keyCode));
    //    }

    //    var languageCode = await _httpRequestService.GetCurrentLanguageCode();

    //    var mocktestPublishedAt = await _mockTestKeyCodeDA.GetMockTestPublishedAtByKeyCode(keyCode);
    //    var mockTestKeyCodeDto = await _mockTestKeyCodeDA.GetDetailByKeyCode(keyCode, languageCode, mocktestPublishedAt);
    //    if (mockTestKeyCodeDto.SubmittedDate is null)
    //    {
    //        throw new ApiValidationException(String.Format(WebConstants.ValidationMessages.MockTestKeyCodeMessage.KeyCodeHaveNotSubmittedYet, keyCode), nameof(mockTestKeyCodeDto.SubmittedDate));
    //    }

    //    var resultModel = new MockTestResultDto();
    //    var tmpKeyCodeResultDto = await _keyCodeResultDA.GetDetailFromKeyCode(keyCode);
    //    var keyCodeResult = _mapper.Map<KeyCodeResultModel>(tmpKeyCodeResultDto);
    //    IEnumerable<MockTestSectionResultDto> listMockTestSectionResultDto = null;
    //    if (keyCodeResult != null)
    //    {
    //        listMockTestSectionResultDto = await _mockTestKeyCodeDA.GetMockTestSectionResult(keyCodeResult.Sections, mockTestKeyCodeDto.SubmittedDate, mocktestPublishedAt);
    //        listMockTestSectionResultDto = listMockTestSectionResultDto.OrderBy(x => x.SortOrder);

    //        resultModel = GenerateScoreResult(mockTestKeyCodeDto, listMockTestSectionResultDto);
    //    }
    //    else
    //    {
    //        var sWSections = await _iIGLmsdbContext.SpeakingWritingScoringResults
    //                 .Include(x => x.MocktestSection)
    //                 .Include(x => x.CourseScoring)
    //                 .AsNoTracking()
    //                 .Where(x => x.MocktestSection != null && x.CourseScoring != null && x.CourseScoring.KeyCode == keyCode)
    //                 .OrderBy(x => x.MocktestSection.SortOrder)
    //                 .Select(x => new
    //                 {
    //                     CourseScoringId = x.CourseScoringId,
    //                     SectionId = x.MocktestSectionId,
    //                     SectionName = x.MocktestSection.Name,
    //                     SectionType = (EMockTestSectionType)x.MocktestSection.Type,
    //                     FromScore = x.FromScore.Value,
    //                     ToScore = x.ToScore.Value,
    //                     MinScore = 0,
    //                     MaxScore = 0
    //                 })
    //                 //
    //                 .ToListAsync();

    //        List<MockTestSectionResultDto> sections = new();
    //        foreach (var item in sWSections.DistinctBy(x => x.SectionId).ToList())
    //        {
    //            var groupScoreIds = await _iIGLmsdbContext.GroupScorings.AsNoTracking().Where(x => x.CourseScoringId == item.CourseScoringId && x.MocktestSectionId == item.SectionId)
    //                    .Select(x => x.GroupScoreId).ToListAsync();
    //            var rankScore = await _iIGLmsdbContext.GroupScores.Include(x => x.RankingScore).Where(x => groupScoreIds.Contains(x.Id)).Select(x => new { x.RankingScore.MinScore, x.RankingScore.MaxScore }).FirstOrDefaultAsync();
    //            var section = new MockTestSectionResultDto
    //            {
    //                SectionId = item.SectionId,
    //                SectionName = item.SectionName,
    //                SectionType = item.SectionType,
    //                FromScore = item.FromScore,
    //                ToScore = item.ToScore,
    //                MinScore = rankScore.MinScore,
    //                MaxScore = rankScore.MaxScore
    //            };
    //            sections.Add(section);
    //        }


    //        resultModel.Sections = sections;
    //        resultModel.MockTestName = mockTestKeyCodeDto.MockTestName;
    //        resultModel.MockTestId = mockTestKeyCodeDto.MockTestId;
    //        resultModel.ScoreType = mockTestKeyCodeDto.MockTestScoreType;
    //        resultModel.LinkUrlViewMore = mockTestKeyCodeDto.LinkUrlViewMore;
    //        resultModel.TextViewMore = mockTestKeyCodeDto.TextViewMore;
    //    }

    //    var SWCourseScoring = await _iIGLmsdbContext.CourseScorings.AsNoTracking()
    //                                                .Where(x => x.KeyCode == mockTestKeyCodeDto.Code)
    //                                                .OrderByDescending(x => x.SubmittedDate)
    //                                                .FirstOrDefaultAsync();
    //    resultModel.ScoringStatus = (ECourseScoringStatus?)SWCourseScoring?.Status;
    //    resultModel.WatchCount = SWCourseScoring?.WatchCount;

    //    //Scoring result
    //    if (SWCourseScoring != null)
    //    {
    //        var listSpeakingWritingScoringResults = await _iIGLmsdbContext.SpeakingWritingScoringResults
    //             .Where(x => x.CourseScoringId == SWCourseScoring.Id && resultModel.Sections.Select(p => p.SectionId).Contains(x.MocktestSectionId))
    //             .ToListAsync();
    //        var listMockTestSections = await _iIGLmsdbContext.MocktestSections
    //            .Where(x => resultModel.Sections.Select(p => p.SectionId).Contains(x.Id))
    //            .ToListAsync();
    //        var listScoreComments = await _iIGLmsdbContext.ScoreComments
    //            .Where(x => listMockTestSections.Select(p => p.RankingScoreId).Contains(x.RankingScoreId))
    //            .ToListAsync();
    //        foreach (var section in resultModel.Sections)
    //        {
    //            if (section.SectionType == EMockTestSectionType.RecordNonstop ||
    //                section.SectionType == EMockTestSectionType.WritingNonstop)
    //            {
    //                var scoringResultOfSection = listSpeakingWritingScoringResults
    //                                     .FirstOrDefault(x => x.CourseScoringId == SWCourseScoring.Id && x.MocktestSectionId == section.SectionId);

    //                //Check result null
    //                if (scoringResultOfSection == null) continue;

    //                if (mockTestKeyCodeDto.MockTestScoreType == EMockTestScoreType.CorrectScore)
    //                {
    //                    section.ExactScore = scoringResultOfSection.ExactScore.HasValue ? scoringResultOfSection.ExactScore.Value : default;
    //                    section.Score = scoringResultOfSection.ExactScore.HasValue ? $"{scoringResultOfSection.ExactScore.Value}" : string.Empty;



    //                    var rankingScoreId = listMockTestSections.FirstOrDefault(x => x.Id == section.SectionId)?.RankingScoreId;
    //                    var scoreComments = listScoreComments.FirstOrDefault(x => x.RankingScoreId == rankingScoreId &&
    //                                                                        section.ExactScore >= x.FromScore &&
    //                                                                        section.ExactScore <= x.ToScore);

    //                    section.Comment = scoreComments?.Comment;

    //                }
    //                else if (mockTestKeyCodeDto.MockTestScoreType == EMockTestScoreType.ScoreRange ||
    //                    mockTestKeyCodeDto.MockTestScoreType == EMockTestScoreType.ITPScoreRange
    //                    )
    //                {
    //                    var exactScore = scoringResultOfSection.ExactScore.HasValue ? scoringResultOfSection.ExactScore.Value : default;
    //                    section.MinScore = resultModel.Sections.FirstOrDefault(x => x.SectionId == section.SectionId).MinScore;
    //                    section.MaxScore = resultModel.Sections.FirstOrDefault(x => x.SectionId == section.SectionId).MaxScore;

    //                    section.Score = $"{(scoringResultOfSection.FromScore.HasValue ? scoringResultOfSection.FromScore.Value : string.Empty)}" +
    //                                    $" - {(scoringResultOfSection.ToScore.HasValue ? scoringResultOfSection.ToScore.Value : string.Empty)}";


    //                    var rankingScoreId = listMockTestSections.FirstOrDefault(x => x.Id == section.SectionId)?.RankingScoreId;

    //                    var scoreComments = listScoreComments.FirstOrDefault(x => x.RankingScoreId == rankingScoreId &&
    //                                                                        exactScore >= x.FromScore &&
    //                                                                        exactScore <= x.ToScore);
    //                    section.Comment = scoreComments?.Comment;
    //                }
    //            }
    //        }

    //        //Update watch count
    //        await _sWScoringBiz.SaveWatchCount(SWCourseScoring.Id, EScoringType.Course);
    //    }

    //    return resultModel;
    //}

    //private MockTestResultDto GenerateScoreResult(MockTestKeyCodeBasicDto mockTestKeyCodeDto, IEnumerable<MockTestSectionResultDto> listMockTestSectionResultDto)
    //{
    //    var scoreResult = new MockTestResultDto()
    //    {
    //        MockTestName = mockTestKeyCodeDto.MockTestName,
    //        MockTestId = mockTestKeyCodeDto.MockTestId,
    //        ScoreType = mockTestKeyCodeDto.MockTestScoreType,
    //        LinkUrlViewMore = mockTestKeyCodeDto.LinkUrlViewMore,
    //        TextViewMore = mockTestKeyCodeDto.TextViewMore,
    //    };

    //    scoreResult = mockTestKeyCodeDto.MockTestScoreType switch
    //    {
    //        EMockTestScoreType.CorrectScore => GenerateCorrectScoreType(scoreResult, listMockTestSectionResultDto),
    //        EMockTestScoreType.ScoreRange => GenerateScoreRangeType(scoreResult, listMockTestSectionResultDto),
    //        EMockTestScoreType.ITPScoreRange => GenerateITPScoreRangeType(scoreResult, listMockTestSectionResultDto),
    //        _ => throw new ArgumentOutOfRangeException(WebConstants.ValidationMessages.MockTestKeyCodeMessage.MockTestScoreTypeIsNotValid)
    //    };
    //    scoreResult.Sections = listMockTestSectionResultDto;

    //    return scoreResult;
    //}

    //private MockTestResultDto GenerateCorrectScoreType(MockTestResultDto result, IEnumerable<MockTestSectionResultDto> listMockTestSectionResultDto)
    //{
    //    result.TotalScore = listMockTestSectionResultDto.Sum(x => x.ExactScore).ToString();
    //    result.MinScore = listMockTestSectionResultDto.Sum(x => x.MinScore);
    //    result.MaxScore = listMockTestSectionResultDto.Sum(x => x.MaxScore);
    //    result.ExtractScore = listMockTestSectionResultDto.Sum(x => x.ExactScore).ToString();
    //    listMockTestSectionResultDto.ForEach(x => x.Score = x.ExactScore.ToString());

    //    return result;
    //}

    //private MockTestResultDto GenerateScoreRangeType(MockTestResultDto result, IEnumerable<MockTestSectionResultDto> listMockTestSectionResultDto)
    //{
    //    result.TotalScore = $"{listMockTestSectionResultDto.Sum(x => x.FromScore)}-{listMockTestSectionResultDto.Sum(x => x.ToScore)}";
    //    result.MinScore = listMockTestSectionResultDto.Sum(x => x.MinScore);
    //    result.MaxScore = listMockTestSectionResultDto.Sum(x => x.MaxScore);
    //    result.ExtractScore = listMockTestSectionResultDto.Sum(x => x.ExactScore).ToString();
    //    listMockTestSectionResultDto.ForEach(x => x.Score = $"{x.FromScore} - {x.ToScore}");

    //    return result;
    //}

    //private MockTestResultDto GenerateITPScoreRangeType(MockTestResultDto result, IEnumerable<MockTestSectionResultDto> listMockTestSectionResultDto)
    //{
    //    result.TotalScore = $"{(int)Math.Round(listMockTestSectionResultDto.Average(x => x.FromScore) * 10)}-{(int)Math.Round(listMockTestSectionResultDto.Average(x => x.ToScore) * 10)}";
    //    result.MinScore = (int)Math.Round(listMockTestSectionResultDto.Average(x => x.MinScore) * 10);
    //    result.MaxScore = (int)Math.Round(listMockTestSectionResultDto.Average(x => x.MaxScore) * 10);
    //    result.ExtractScore = (listMockTestSectionResultDto.Sum(x => x.ExactScore) * 10 / listMockTestSectionResultDto.Count()).ToString();
    //    listMockTestSectionResultDto.ForEach(x => x.Score = $"{x.FromScore} - {x.ToScore}");

    //    return result;
    //}

    #endregion
    public async Task<MockTestStructureModel> GetMocktestStructureAsync(string keyCode)
    {

        var mockTestIdByKeyCode = await _appFactory.Repository<MocktestKeyCode>().GetAll().Where(p => p.Code == keyCode).Select(p => p.MocktestId).FirstOrDefaultAsync();
        if (mockTestIdByKeyCode == null)
        {
            throw new ApiValidationException(WebConstants.ValidationMessages.MockTestMessage.KeyCodeNotFoundKey,
                nameof(keyCode));
        }

        var mockTest = await _mockTestDataService.GetMockTest(mockTestIdByKeyCode);

        return _mapper.Map<MockTestStructureModel>(mockTest);
    }

    public async Task<StartedDoingAnswerResponse> StartDoingAnswerAsync(StartedDoingAnswerRequest request)
    {
        var languageCode = await _httpRequestService.GetCurrentLanguageCode();

        _logger.LogInformation($"StartDoingAnswerAsync: {request.KeyCode}");

        var response = new StartedDoingAnswerResponse();

        var clientIp = _httpRequestService.GetClientIpAddress();

        // get key code detail
        var mockTestKeyCode = await _mockTestKeyCodeDA
            .GetKeyCodeDetailByKeyCodeAsync(request.KeyCode);
        if (mockTestKeyCode == null)
        {
            throw new ApiValidationException(WebConstants.ValidationMessages.MockTestMessage.KeyCodeNotFoundKey,
                nameof(request.KeyCode));
        }

        //get course scoring info if exists


        var courseScoring = await _mockTestKeyCodeDataService.GetTupleCourseScoring(request.KeyCode);

        string cookieValue = string.Empty;
        // validate key code
        if (courseScoring == null || (!courseScoring.Item2 && courseScoring.Item1.Status != ECourseScoringStatus.Scored))
        {
            cookieValue = await ValidateMocktestKeycode(request, mockTestKeyCode);
        }

        var timeCache = (int)TimeSpan.FromHours(2).TotalSeconds;
        var mockTestKeyCodeInfo = await _mockTestKeyCodeDataService.GetMockTestKeyCodeAsync(request.KeyCode);
        if (mockTestKeyCodeInfo == null)
        {
            _logger.LogInformation($"mockTestKeyCodeInfo null");
            //Add cookie to response
            var keyCodeCookieValue = Guid.NewGuid();
            _httpContextAccessor.HttpContext.Response.Cookies
                .Append(Constants.RequestHeaderKey.KeyCodeCookieKey, keyCodeCookieValue.ToString(), new CookieOptions
                {
                    Expires = DateTimeOffset.Now.AddDays(1),
                    Path = "/",
                    SameSite = SameSiteMode.None,
                    Secure = true
                });

            var mgMockTestModelAdd = await _mockTestDataService.GetMockTest(mockTestKeyCode.MocktestId);

            var dumpModelAdd = _mapper.Map<MgMockTestKeyCodeModel>(request);
            dumpModelAdd.ClientIp = clientIp;
            dumpModelAdd.MockTestId = mgMockTestModelAdd.MockTestId;
            dumpModelAdd.MockTestMenu = mgMockTestModelAdd.MockTestMenu;
            dumpModelAdd.TimeRemaining = mgMockTestModelAdd.MockTestSections.Sum(s => s.NumberOfTime);
            dumpModelAdd.StartedDoingExamDate = DateTime.UtcNow;
            dumpModelAdd.EndedDoingExamDate = dumpModelAdd.StartedDoingExamDate.Value.AddSeconds(dumpModelAdd.TimeRemaining);
            dumpModelAdd.Cookie = keyCodeCookieValue;
            dumpModelAdd.MockTestMenu?.Sections?.ToList()
                                                    .ForEach(section => section?.Parts?
                                                        .ToList()
                                                        .ForEach(part => part?.Questionnaires?
                                                            .ToList()
                                                            .ForEach(qn => qn?.Questions?
                                                                .ToList()
                                                                .ForEach(q => q.Status = EAnswerStatus.NotAnswered))));

            //save redis cache
            await _mockTestKeyCodeDataService.InsertOrUpdateKeyCodeAsync(request.KeyCode, dumpModelAdd, timeCache);
            var startedDoingModelAdd = _mapper.Map<StartedDoingAnswerModel>(dumpModelAdd);

            //Send to rabbitMQ to execute update mongoDB and SQL
            //_rabbitMQProducer.SendMessage(new RabbitMQMockTestActionModel()
            //{
            //    Action = RabbitMQMockTestAction.MockTestKeyCode_StartDoingTest,
            //    KeyCode = request.KeyCode,
            //    Data = JsonConvert.SerializeObject(request)
            //}, Constants.RabbitMQ.TopicMockTestRedisToMongo, Constants.RabbitMQ.QueueMockTestRedisToMongoRoutingkey);

            response = _mapper.Map<StartedDoingAnswerResponse>(dumpModelAdd);

            return response;
        }

        //get mockTest data from mongodb
        var mgMockTestModel = await _mockTestDataService.GetMockTest(mockTestKeyCode.MocktestId);

        var dumpModel = _mapper.Map<MgMockTestKeyCodeModel>(request);
        dumpModel.ClientIp = clientIp;
        dumpModel.MockTestId = mgMockTestModel.MockTestId;
        dumpModel.MockTestMenu = mgMockTestModel.MockTestMenu;
        dumpModel.StartedDoingExamDate = mockTestKeyCodeInfo.StartedDoingExamDate;
        dumpModel.EndedDoingExamDate = mockTestKeyCodeInfo.EndedDoingExamDate;
        dumpModel.TimeRemaining = (int)(dumpModel.EndedDoingExamDate.Value - DateTime.UtcNow).TotalSeconds;
        dumpModel.TimeRemaining = dumpModel.TimeRemaining < 0 ? 1 : dumpModel.TimeRemaining;
        var keyCodeCookieValueNew = Guid.NewGuid();
        if (string.IsNullOrEmpty(cookieValue))
        {
            if (mockTestKeyCode.Cookie == null)
            {
                //Add cookie to response
                _httpContextAccessor.HttpContext.Response.Cookies
                .Append(Constants.RequestHeaderKey.KeyCodeCookieKey, keyCodeCookieValueNew.ToString(), new CookieOptions
                {
                    Expires = DateTimeOffset.Now.AddDays(1),
                    Path = "/",
                    SameSite = SameSiteMode.None,
                    Secure = true
                });
                dumpModel.Cookie = keyCodeCookieValueNew;
            }
        }
        else
        {
            dumpModel.Cookie = Guid.Parse(cookieValue);
        }

        //get data of this keycode test from mongodb
        var mockTestUpdate = mockTestKeyCodeInfo;
        dumpModel.Id = mockTestUpdate.Id;

        var areSpeakingQuestionnaires = await _appFactory.Repository<Questionnaire>().GetAll()
            .Include(x => x.Questions)
            .Include(x => x.MocktestPartQuestionnaires)
            .Where(x => (x.Type == (short)EQuestionnaireType.Record || x.Type == (short)EQuestionnaireType.ReadTextALoud) && x.Id == mockTestUpdate.CurrentQuestionnaireId && x.MocktestPartQuestionnaires.Any(p => p.MocktestPartId == mockTestUpdate.CurrentMockTestPartId))
            .AnyAsync();

        dumpModel.CurrentMockTestPartId = mockTestUpdate.CurrentMockTestPartId;
        if (!areSpeakingQuestionnaires)
        {
            dumpModel.CurrentQuestionnaireId = mockTestUpdate.CurrentQuestionnaireId;
        }
        else
        {
            var removeQuestionStatus = mockTestUpdate.MockTestMenu.Sections.Where(x => x.Parts.Any(p => p.Id == mockTestUpdate.CurrentMockTestPartId && p.Questionnaires.Any(q => q.Id == mockTestUpdate.CurrentQuestionnaireId))).SelectMany(x => x.Parts).Where(x => x.Id == mockTestUpdate.CurrentMockTestPartId).SelectMany(x => x.Questionnaires).SelectMany(x => x.Questions).ToList();
            foreach (var item in removeQuestionStatus)
            {
                item.Status = EAnswerStatus.NotAnswered;
            }

        }
        dumpModel.MockTestMenu = mockTestUpdate.MockTestMenu;

        if (string.IsNullOrEmpty(dumpModel.MockTestName))
            dumpModel.MockTestName = await _appFactory.Repository<MocktestTranslation>().GetAll()
                                                                                         .Where(p => p.MocktestId == mockTestKeyCode.MocktestId && p.LanguageCode == languageCode)
                                                                                         .Select(p => p.Name)
                                                                                         .FirstOrDefaultAsync();

        await _mockTestKeyCodeDataService.InsertOrUpdateKeyCodeAsync(request.KeyCode, dumpModel, timeCache);


        //Send to rabbitMQ to execute update mongoDB and SQL
        //_rabbitMQProducer.SendMessage(new RabbitMQMockTestActionModel()
        //{
        //    Action = RabbitMQMockTestAction.MockTestKeyCode_StartDoingTest,
        //    KeyCode = request.KeyCode,
        //    Data = JsonConvert.SerializeObject(request)
        //}, Constants.RabbitMQ.TopicMockTestRedisToMongo, Constants.RabbitMQ.QueueMockTestRedisToMongoRoutingkey);

        //verify Request info
        var requestModel = new MockTestKeyCodeBaseRequest
        {
            KeyCode = request.KeyCode,
            Browser = request.Browser,
            TimeRemaining = mockTestKeyCodeInfo.TimeRemaining
        };

        mockTestKeyCodeInfo = dumpModel;
        if (mockTestKeyCode.IsAutoGenerate == true)
            mockTestKeyCodeInfo.MockTestMenu = mgMockTestModel.MockTestMenu;
        response = _mapper.Map<StartedDoingAnswerResponse>(mockTestKeyCodeInfo);
        response.MockTestName = dumpModel.MockTestName;

        CourseScoring courseScoringInfo = null;
        if (courseScoring != null)
        {
            courseScoringInfo = await _appFactory.Repository<CourseScoring>().GetAll()
                                                   .Include(x => x.SpeakingWritingChooses)
                                                   .Where(x => x.KeyCode == request.KeyCode)
                                                   .OrderByDescending(x => x.SubmittedDate)
                                                   .FirstOrDefaultAsync();
        }

        if (response.MockTestMenu.Sections.Any())
        {
            foreach (var mckSection in response.MockTestMenu.Sections)
            {
                if (!mckSection.Parts.Any())
                    continue;

                foreach (var mckPart in mckSection.Parts)
                {
                    if (!mckPart.Questionnaires.Any())
                        continue;

                    var questionnaires = mckPart.Questionnaires.OrderBy(x => x.SortOrder).ToList();

                    foreach (var questionnaire in questionnaires)
                    {
                        if (!questionnaire.Questions.Any())
                            continue;

                        var questionChooses = courseScoringInfo?.SpeakingWritingChooses
                                                   .Where(x => questionnaire.Id == x.QuestionnaireId &&
                                                               x.MocktestPartId == mckPart.Id
                                                        )
                                                   .Select(x => new { x.QuestionId, x.QuestionnaireId, x.MocktestPartId, x.WritingAnswer, x.RecordingFileId });

                        var questions = questionnaire.Questions.OrderBy(x => x.SortOrder).ToList();
                        var userChoose = await _appFactory.Repository<KeycodeChoose>().GetAll()
                            .Where(x => x.MocktestPartId == mckPart.Id && x.Keycode == request.KeyCode)
                            .Select(x => new { x.QuestionId, x.MocktestPartId })
                            .ToListAsync();


                        foreach (var question in questions)
                        {
                            var questionInfo = questionChooses?.FirstOrDefault(x => x.QuestionId == question.Id &&
                                                                    x.QuestionnaireId == questionnaire.Id &&
                                                                    x.MocktestPartId == mckPart.Id
                                                                );

                            var qInfo = userChoose.FirstOrDefault(x => x.QuestionId == question.Id && x.MocktestPartId == mckPart.Id);

                            var status = EAnswerStatus.NotAnswered;
                            if (qInfo is null)
                            {
                                if (questionInfo is null)
                                {
                                    status = EAnswerStatus.NotAnswered;
                                }
                                else
                                {
                                    status = (!string.IsNullOrEmpty(questionInfo.WritingAnswer) || questionInfo.RecordingFileId.HasValue) ?
                                                EAnswerStatus.Correct :
                                                EAnswerStatus.InCorrect;
                                }
                                question.Status = status;

                            }
                            else
                            {
                                question.Status = EAnswerStatus.InCorrect;
                            }

                            //question.Status = questionInfo == null ? EAnswerStatus.NotAnswered :
                            //                   (!string.IsNullOrEmpty(questionInfo.WritingAnswer) || questionInfo.RecordingFileId.HasValue) ?
                            //                    EAnswerStatus.Correct :
                            //                    EAnswerStatus.InCorrect;
                        }
                    }
                }
            }
        }

        return response;
    }

    private async Task<string> ValidateMocktestKeycode(StartedDoingAnswerRequest request, MockTestKeyCodeDetailDto mockTestKeyCode)
    {
        // Check if test is already submitted
        if (mockTestKeyCode.SubmittedDate != null)
        {
            throw new ApiValidationException(WebConstants.ValidationMessages.MockTestKeyCodeMessage.KeyCodeAlreadySubmitted, nameof(request.KeyCode));
        }

        // Check if invalid start date
        if (mockTestKeyCode.StartDate.HasValue)
        {
            var isValidStartDate = mockTestKeyCode.StartDate.Value.Date <= DateTime.UtcNow.Date;
            if (!isValidStartDate)
                throw new ApiValidationException(WebConstants.ValidationMessages.MockTestMessage.InvalidKeyCodeStartDateKey, nameof(request.KeyCode));
        }

        // Check if already pass end date
        var isValidEndDate = mockTestKeyCode.EndDate.Date >= DateTime.UtcNow.Date;
        if (!isValidEndDate)
            throw new ApiValidationException(WebConstants.ValidationMessages.MockTestMessage.KeyCodeNotFoundKey,
                nameof(request.KeyCode));

        //Check user need to finish current section mocktest
        var cookieValue = _httpContextAccessor.HttpContext.Request.Cookies[Constants.RequestHeaderKey.KeyCodeCookieKey];
        if (!string.IsNullOrEmpty(cookieValue) && !(mockTestKeyCode.IsAutoGenerate.HasValue && mockTestKeyCode.IsAutoGenerate.Value))
        {
            var existedMocktestKeyCode = await _mockTestKeyCodeDataService.GetKeyCodeDetailByCookieAsync(Guid.Parse(cookieValue));
            if (existedMocktestKeyCode != null && !existedMocktestKeyCode.SubmittedDate.HasValue && existedMocktestKeyCode.Code != request.KeyCode)
            {
                throw new ApiValidationException(
                    WebConstants.ValidationMessages.MockTestMessage.UserNeedToFinishCurrentSectionMocktest, null);
            }
        }

        return cookieValue;
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
        _rabbitMQProducer.SendMessage(new RabbitMQMockTestActionModel()
        {
            Action = RabbitMQMockTestAction.MockTestKeyCode_MarkQuestion,
            KeyCode = request.KeyCode,
            Data = JsonConvert.SerializeObject(request)
        }, Constants.RabbitMQ.TopicMockTestRedisToMongo, Constants.RabbitMQ.QueueMockTestRedisToMongoRoutingkey);
    }

    private async Task<MgMockTestMenuModel> ProcessSaveAnswerAsync(SaveAnswerRequest request, MgMockTestKeyCodeModel mockTestKeyCodeInfo)
    {

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

        return newestMockTestKeyCodeInfo.MockTestMenu;
    }

    public async Task<MgMockTestMenuModel> SaveAnswerAsync(SaveAnswerRequest request)
    {
        Console.WriteLine($"VerifyRequestInfoAndGetKeyCodeInfoAsync {DateTime.Now.ToString("hh:mm:ss:fff")}");
        _logger.LogInformation($"SaveAnswerAsync {request.KeyCode}");

        var mockTestKeyCodeInfo = await VerifyRequestInfoAndGetKeyCodeInfoAsync(request);


        Console.WriteLine($"ProcessSaveAnswerAsync {DateTime.Now.ToString("hh:mm:ss:fff")}");

        var newestMockTestKeyCodeMenu = await ProcessSaveAnswerAsync(request, mockTestKeyCodeInfo);

        Console.WriteLine($"RabbitMQMockTestActionModel {DateTime.Now.ToString("hh:mm:ss:fff")}");

        //Send to rabbitMQ to execute update mongoDB and SQL
        _rabbitMQProducer.SendMessage(new RabbitMQMockTestActionModel()
        {
            Action = RabbitMQMockTestAction.MockTestKeyCode_SaveAnswer,
            KeyCode = request.KeyCode,
            Data = JsonConvert.SerializeObject(request)
        }, Constants.RabbitMQ.TopicMockTestRedisToMongo, Constants.RabbitMQ.QueueMockTestRedisToMongoRoutingkey);
        Console.WriteLine($"newestMockTestKeyCodeMenu {DateTime.Now.ToString("hh:mm:ss:fff")}");

        return newestMockTestKeyCodeMenu;
    }


    public async Task<MgMockTestMenuModel> GetMockTestMenuAsync(MockTestKeyCodeBaseRequest request)
    {
        _logger.LogInformation($"GetMockTestMenuAsync {request.KeyCode}");

        var mockTestKeyCodeInfo = await VerifyRequestInfoAndGetKeyCodeInfoAsync(request);

        return mockTestKeyCodeInfo.MockTestMenu;
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


        // Truy vấn trả lời đã nộp từ SQL Server
        var submittedAnswerTask = _iIGLmsdbContext.SpeakingWritingChooses
                                                  .Include(x => x.CourseScoring)
                                                  .Include(x => x.LiveClassScoring)
                                                  .Include(x => x.SpeakingWritingResult)
                                                  .AsNoTracking()
                                                  .Where(x => x.CourseScoring != null && x.CourseScoring.KeyCode == request.KeyCode)
                                                  .Select(x => new
                                                  {
                                                      x.ScoringType,
                                                      Status = x.CourseScoring != null ? x.CourseScoring.Status : x.LiveClassScoring.Status,
                                                      x.WritingAnswer,
                                                      x.RecordingFileId,
                                                      x.QuestionId,
                                                      x.QuestionnaireId,
                                                      x.MocktestPartId,
                                                      ResultModel = _mapper.Map<SpeakingWritingResultViewModel>(x.SpeakingWritingResult),
                                                  })
                                                  .ToListAsync();

        await Task.WhenAll(preSubmitAnswerTask, submittedAnswerTask);

        var preSubmitAnswer = await preSubmitAnswerTask;
        var submittedAnswer = await submittedAnswerTask;

        // Gán câu trả lời đã nộp vào câu hỏi
        foreach (var question in result.Questions)
        {
            question.SubmittedQuestionModel = new Core.Common.Models.SpeakingAndWriting.SubmittedQuestionModel();

            var matchingPreSubmitAnswer = preSubmitAnswer?.FirstOrDefault(x => x.QuestionnaireId == question.QuestionnaireId &&
                                                                                x.QuestionId == question.QuestionId);

            var matchingSubmittedAnswer = submittedAnswer?.OrderByDescending(p => p.Status).FirstOrDefault(x => x.QuestionnaireId == question.QuestionnaireId &&
                                                                               x.QuestionId == question.QuestionId);

            question.SubmittedQuestionModel.AnsweredText = matchingPreSubmitAnswer?.AnswerText ?? matchingSubmittedAnswer?.WritingAnswer;
            question.SubmittedQuestionModel.AudioFileId = matchingPreSubmitAnswer?.RecordingFileId ?? matchingSubmittedAnswer?.RecordingFileId;
            question.SubmittedQuestionModel.Status = (ECourseScoringStatus?)matchingSubmittedAnswer?.Status;
            question.SubmittedQuestionModel.ResultModel = matchingSubmittedAnswer?.ResultModel;
        }

        // Sắp xếp câu hỏi theo thứ tự SortOrder
        result.Questions = result.Questions.OrderBy(f => f.SortOrder).ToList();

        return result;
    }

    private void SortRandomAnswers(List<QuestionDto> questions)
    {
        foreach (var question in questions)
        {
            var random = new Random();
            question.Answers = question.Answers.OrderBy(a => random.Next()).ToList();
        }
    }

    public async Task<MgMockTestPartModel> GetMockTestPartDetailAsync(GetMockTestPartDetailRequest request)
    {
        _logger.LogInformation($"GetMockTestPartDetailAsync {request.KeyCode}");

        //var courseScoring = _iIGLmsdbContext.CourseScorings.Include(x => x.MocktestKeyCode)
        //                           .ThenInclude(x => x.Mocktest).ThenInclude(x => x.MocktestSections)
        //                           .ThenInclude(x => x.MocktestParts).ThenInclude(x => x.MocktestPartQuestionnaires)
        //                           .ThenInclude(x => x.Questionnaire)
        //                           .AsNoTracking()
        //                           .Where(x => x.KeyCode == request.KeyCode)
        //                           .AsEnumerable()
        //                           .Select(x => new Tuple<CourseScoring, bool>
        //                           (
        //                               x,
        //                                x.MocktestKeyCode.Mocktest.MocktestSections.Any(x =>
        //                               x.MocktestParts.Any(p => p.MocktestPartQuestionnaires.Any(q =>
        //                               q.Questionnaire.Type == (short)EQuestionnaireType.Record
        //                               || q.Questionnaire.Type == (short)EQuestionnaireType.ReadTextALoud
        //                               || q.Questionnaire.Type == (short)EQuestionnaireType.Writing

        //                               )))
        //                           ))
        //                           .OrderByDescending(x => x.Item1.SubmittedDate)
        //                           .FirstOrDefault();
        var courseScoring = await _mockTestKeyCodeDataService.GetTupleCourseScoring(request.KeyCode);

        var mockTestKeyCodeInfo = await VerifyRequestInfoAndGetKeyCodeInfoAsync(request, courseScoring: courseScoring);
        _logger.LogInformation($"mockTestKeyCodeInfo ID = {mockTestKeyCodeInfo.MockTestId}");




        var mgMockTestModel = await _mockTestDataService.GetMockTest(mockTestKeyCodeInfo.MockTestId);
        _logger.LogInformation($"mgMockTestModel = {mgMockTestModel}");


        var mgMockTestPartDetail = mgMockTestModel.MockTestSections.SelectMany(x => x.Parts)
            .FirstOrDefault(x => x.Id == request.MockTestPartId);


        return mgMockTestPartDetail;
    }

    //public async Task AutoSubmitMockTestKeyCodeAsync()
    //{

    //    try
    //    {
    //        _logger.LogInformation($"Start AutoSubmitMockTestKeyCodeAsync at utc = {DateTime.UtcNow}, now = {DateTime.Now}");

    //        var listKeyCodeNeedToAutoSubmit = (await _mockTestKeyCodeDA.GetListMockTestKeyCodeNeedToAutoSubmit()).ToList();
    //        _logger.LogInformation($"Start AutoSubmitMockTestKeyCodeAsync data = {JsonConvert.SerializeObject(listKeyCodeNeedToAutoSubmit)}");

    //        var batchSize = 100;
    //        var splitList = listKeyCodeNeedToAutoSubmit.SplitList(batchSize);

    //        foreach (var list in splitList)
    //        {
    //            await _keyCodeAnswerService.SubmitMultipleMockTestAsync(list);
    //        }
    //    }
    //    catch (Exception e)
    //    {
    //        _logger.LogError($"There's error in AutoSubmitMockTestKeyCodeAsync, Exception = {e.Message}, InnerException ={e.InnerException?.Message}");
    //    }
    //}

    //public async Task AutoDeleteMockTestKeyCodeAsync()
    //{
    //    try
    //    {
    //        _logger.LogInformation($"Start AutoDeleteMockTestKeyCodeAsync at utc = {DateTime.UtcNow}, now = {DateTime.Now}");
    //        var listKeyCodeNeedToAutoDelete = (await _mockTestKeyCodeDA.GetListMockTestKeyCodeNeedToAutoDelete()).ToList();
    //        _logger.LogInformation($"Start AutoDeleteMockTestKeyCodeAsync data = {JsonConvert.SerializeObject(listKeyCodeNeedToAutoDelete)}");

    //        var batchSize = 100;
    //        var splitList = listKeyCodeNeedToAutoDelete.SplitList(batchSize);
    //        foreach (var list in splitList)
    //        {
    //            await _keyCodeAnswerService.DeleteMultipleMockTestAsync(list);
    //        }

    //    }
    //    catch (Exception e)
    //    {
    //        _logger.LogError($"There's error in AutoDeleteMockTestKeyCodeAsync, Exception = {e.Message}, InnerException ={e.InnerException?.Message}");
    //    }
    //}

    //public async Task<OverloadCodeTest> CheckOverloadMockTestKeyCodeAsync()
    //{
    //    //var count = await _mockTestKeyCodeDA.CountMockTestKeyCodeExamining();
    //    var count = await _mockTestKeyCodeDataService.CountMockTestKeyCodeExamining();
    //    return new OverloadCodeTest
    //    {
    //        UserLive = count,
    //        UserOverload = _appSettingOptions.NumberUserOverload
    //    };
    //}

    private async Task<MgMockTestKeyCodeModel> UpdateMockTestMenuAsync(MgMockTestKeyCodeModel mgMockTestKeyCode, SaveAnswerRequest request, EAnswerStatus status)
    {
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
        return mgMockTestKeyCode;
    }

    private async Task<MgMockTestKeyCodeModel> VerifyRequestInfoAndGetKeyCodeInfoAsync(MockTestKeyCodeBaseRequest request, string? requestCookie = null, Tuple<CourseScoring, bool> courseScoring = null)
    {
        var cookieValue = string.IsNullOrEmpty(requestCookie) ? _httpContextAccessor.HttpContext.Request.Cookies[RequestHeaderKey.KeyCodeCookieKey] : requestCookie;
        var mockTestKeyCodeInfo = await _mockTestKeyCodeDataService.GetMockTestKeyCodeAsync(request.KeyCode);
        _logger.LogInformation($"mockTestKeyCodeInfo = {mockTestKeyCodeInfo}");
        if (mockTestKeyCodeInfo == null)
        {
            throw new ApiValidationException(WebConstants.ValidationMessages.MockTestMessage.KeyCodeNotFoundKey,
                nameof(request.KeyCode));
        }

        if (courseScoring == null)
        {
            courseScoring = await _mockTestKeyCodeDataService.GetTupleCourseScoring(request.KeyCode);
        }

        if (courseScoring == null || (!courseScoring.Item2 && courseScoring.Item1.Status != ECourseScoringStatus.Scored))
        {
            if (mockTestKeyCodeInfo.Cookie.HasValue || !string.IsNullOrEmpty(mockTestKeyCodeInfo.Browser))
            {
                if (mockTestKeyCodeInfo.Cookie?.ToString() != cookieValue
                || mockTestKeyCodeInfo.Browser?.ToLower() != request.Browser?.ToLower())
                {

                    _logger.LogError("VerifyRequestInfoAndGetKeyCodeInfoAsync mockTestKeyCodeInfo.Cookie{0}-cookieValue{1};mockTestKeyCodeInfo.Browser{2}-request.Browser{3}", mockTestKeyCodeInfo.Cookie, cookieValue, mockTestKeyCodeInfo.Browser, request.Browser);
                    throw new ApiValidationException(
                    WebConstants.ValidationMessages.MockTestKeyCodeMessage.KeyCodeInUseKey, request.KeyCode);
                }
            }

        }

        return mockTestKeyCodeInfo;
    }

    //public async Task<FileStreamResult> StreamingPlayAsync(string keyCode, Guid fileId)
    //{
    //    var mockTestKeyCodeInfo = await _mockTestKeyCodeDataService.GetMockTestKeyCodeAsync(keyCode);
    //    if (mockTestKeyCodeInfo == null)
    //    {
    //        throw new ApiValidationException(WebConstants.ValidationMessages.MockTestMessage.KeyCodeNotFoundKey,
    //            nameof(keyCode));
    //    }

    //    return await _fileUploaderService.StreamingPlayAsync(fileId);
    //}

    //public async Task<MgMockTestKeyCodeModel> GetMocktestResultStructure(string keyCode)
    //{
    //    var mockTestKeyCodeInfo = await _mockTestKeyCodeDataService.GetMockTestKeyCodeAsync(keyCode);
    //    return mockTestKeyCodeInfo;
    //}


}
