using AutoMapper;
using IIG.Application.Data;
using IIG.Application.Models;
using IIG.Application.Models.Assessment;
using IIG.Application.Services;
using IIG.Core.Common.ConfigureModels;
using IIG.Core.Common.Enums;
using IIG.Core.Common.ErrorHandling;
using IIG.Core.Common.Models.Files;
using IIG.Core.Common.MongoDataModels;
using IIG.Core.Enums;
using IIG.Core.Helpers;
using IIG.Core.Providers.MongoDbProvider.Infrastructure;
using IIG.Core.Services.Interfaces;
using IIG.Web.BL.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Data;

namespace IIG.Web.BL.Services.Impls.Practices;

public class AssessmentService : IAssessmentService
{
    private readonly IMongoGenericRepository<MgQuestionModel> _mongoQuestionRepos;
    private readonly IMongoGenericRepository<MgQuestionnaireModel> _mongoQuestionnaireRepos;
    //private readonly IMyCourseBiz _myCourseBiz;
    private readonly IStepQuestionnaireDA _stepQuestionnaireDA;

    //private readonly IWebUserChooseDA _webUserChooseDA;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;
    //private readonly IStepDA _stepDA;
    //private readonly IElsaApi _elsaApi;
    //private readonly IFileUploaderService _fileUploaderService;
    //private readonly IHttpRequestService _httpRequestService;
    //private readonly IFileService _fileService;
    //private readonly IWebUserPracticeHistoryDA _webUserPracticeHistoryDA;
    private readonly AppSettingOptions _settingOption;
    //private readonly IStorageService _storageService;
    //private readonly ISwiftStorageService _swiftStorageService;
    //private readonly IIGLmsdbContext _iIGLmsdbContext;
    //private readonly IMicrosoftPronunciationAssessmentService _microsoftPronunciationAssessmentService;
    public AssessmentService(IMongoGenericRepository<MgQuestionModel> mongoQuestionRepos,
       IMongoGenericRepository<MgQuestionnaireModel> mongoQuestionnaireRepos,
        //IMyCourseBiz myCourseBiz,

        IStepQuestionnaireDA stepQuestionnaireDA,
        //IWebUserChooseDA webUserChooseDA,
        IHttpContextAccessor httpContextAccessor,
        IMapper mapper,
        //IStepDA stepDa,
        //IElsaApi elsaApi,
        //IFileUploaderService fileUploaderService,
        //IHttpRequestService httpRequestService,
        //IFileService fileService,
        //IWebUserPracticeHistoryDA webUserPracticeHistoryDA,
        IOptions<AppSettingOptions> settingOption
        //IStorageService storageService,
        //ISwiftStorageService swiftStorageService,
        //IIGLmsdbContext iIGLmsdbContext,
        //IMicrosoftPronunciationAssessmentService microsoftPronunciationAssessmentService
        )
    {
        _mongoQuestionRepos = mongoQuestionRepos;
        _mongoQuestionnaireRepos = mongoQuestionnaireRepos;
        //_myCourseBiz = myCourseBiz;

        _stepQuestionnaireDA = stepQuestionnaireDA;
        //_webUserChooseDA = webUserChooseDA;
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
        //_stepDA = stepDa;
        //_elsaApi = elsaApi;
        //_fileUploaderService = fileUploaderService;
        //_httpRequestService = httpRequestService;
        //_fileService = fileService;
        //_webUserPracticeHistoryDA = webUserPracticeHistoryDA;
        _settingOption = settingOption.Value;
        //_storageService = storageService;
        //_swiftStorageService = swiftStorageService;
        //_iIGLmsdbContext = iIGLmsdbContext;
        //_microsoftPronunciationAssessmentService = microsoftPronunciationAssessmentService;

    }

    //public async Task<AssessmentResponse> CheckAnswerAsync(AssessmentRequest request)
    //{

    //    var stepQuestionnaire = await _stepQuestionnaireDA

    //        .GetInfo(request.StepId, request.QuestionnaireId);

    //    if (stepQuestionnaire == null)
    //    {
    //        throw new ApiNotFoundException(WebConstants.ValidationMessages.WebQuestionnaireMessage.StepQuestionnaireNotFoundKey);
    //    }

    //    var checkAccess = await _myCourseBiz.CurrentUserAccessCourseValidationAsync(stepQuestionnaire.CourseId);
    //    if (!checkAccess)
    //    {
    //        await _myCourseBiz.StepFirstOrderValidationAsync(stepQuestionnaire.CourseId, request.StepId);
    //    }
    //    var questionnaire = await _mongoQuestionnaireRepos.FindByIdAsync(x => x.QuestionnaireId == request.QuestionnaireId);
    //    if (questionnaire == null)
    //    {
    //        throw new ApiNotFoundException(WebConstants.ValidationMessages.WebQuestionnaireMessage.QuestionnaireNotFoundKey);
    //    }

    //    ValidateAssessment(questionnaire.Type, request.AnswerChooseRequest);

    //    var listQuestionId = request.AnswerChooseRequest.Select(y => y.QuestionId);
    //    var questions = await _mongoQuestionRepos.FilterAsync(x => x.QuestionnaireId == request.QuestionnaireId && listQuestionId.Contains(x.QuestionId));
    //    questions = questions.ToList();
    //    var stepBasicInfo = await _stepDA.GetStepBaseInfoByStepIdTask(request.StepId);

    //    var result = new AssessmentResponse()
    //    {
    //        QuestionnaireId = request.QuestionnaireId,
    //        StepId = request.StepId
    //    };

    //    var currentUserId = _httpContextAccessor.HttpContext.User.FindFirst(Constants.ClaimTypes.UserId).Value.ToGuid();

    //    // Delete web_user_choose => then insert
    //    var listAnswerResponse = GenerateAnswerResponse(questionnaire.Type, questions, request.AnswerChooseRequest);
    //    foreach (var item in listAnswerResponse)
    //    {
    //        var deleteRequest = new WebUserChooseDelete
    //        {
    //            WebUserId = currentUserId,
    //            StepId = request.StepId,
    //            QuestionId = item.QuestionId
    //        };

    //        switch (questionnaire.Type)
    //        {
    //            case EQuestionnaireType.Video:
    //            case EQuestionnaireType.MCQ:
    //            case EQuestionnaireType.MCQImage:
    //            case EQuestionnaireType.TrueFalse:
    //                // delete by questionId
    //                break;

    //            case EQuestionnaireType.ImageDragDrop:
    //            case EQuestionnaireType.Droplist:
    //            case EQuestionnaireType.FillInTheBlank:
    //                // delete by questionId && answerId
    //                deleteRequest.AnswerId = item.AnswerId;
    //                break;
    //            case EQuestionnaireType.Matching:
    //            case EQuestionnaireType.MatchingImage:
    //                // delete by questionId && answerId && MatchingQuestionId
    //                deleteRequest.AnswerId = item.AnswerId;
    //                deleteRequest.MatchingQuestionId = item.MatchingQuestionId;
    //                break;
    //            default:
    //                throw new ArgumentOutOfRangeException(WebConstants.ValidationMessages.AssessmentMessage.QuestionnaireTypeIsNotValid);
    //        }

    //        await _webUserChooseDA.DeleteAsync(deleteRequest);

    //        await _webUserChooseDA.InsertAsync(new WebUserChooseInsert()
    //        {
    //            WebUserId = currentUserId,
    //            QuestionId = item.QuestionId,
    //            AnswerId = item.AnswerId,
    //            StepId = request.StepId,
    //            AnswerText = item.AnswerText,
    //            CorrectAnswer = item.IsCorrect,
    //            MatchingQuestionId = item.MatchingQuestionId,
    //            LessonId = stepBasicInfo.LessonId,
    //            UnitId = stepBasicInfo.UnitId,
    //            CourseId = stepBasicInfo.CourseId
    //        });

    //        result.AnswerChooseResponse.Add(item);
    //    }



    //    return result;
    //}

    //public async Task<AssessmentResponse> CheckAnswerV2Async(AssessmentRequest request)
    //{
    //    var stepQuestionnaire = await _stepQuestionnaireDA
    //    .GetInfo(request.StepId, request.QuestionnaireId);
    //    if (stepQuestionnaire == null)
    //    {
    //        throw new ApiNotFoundException(WebConstants.ValidationMessages.WebQuestionnaireMessage.StepQuestionnaireNotFoundKey);
    //    }

    //    var checkAccess = await _myCourseBiz.CurrentUserAccessCourseValidationAsync(stepQuestionnaire.CourseId);
    //    if (!checkAccess)
    //    {
    //        await _myCourseBiz.StepFirstOrderValidationAsync(stepQuestionnaire.CourseId, request.StepId);
    //    }
    //    var questionnaire = await _mongoQuestionnaireRepos.FindByIdAsync(x => x.QuestionnaireId == request.QuestionnaireId);
    //    if (questionnaire == null)
    //    {
    //        throw new ApiNotFoundException(WebConstants.ValidationMessages.WebQuestionnaireMessage.QuestionnaireNotFoundKey);
    //    }

    //    ValidateAssessment(questionnaire.Type, request.AnswerChooseRequest);

    //    var listQuestionId = request.AnswerChooseRequest.Select(y => y.QuestionId);
    //    var questions = await _mongoQuestionRepos.FilterAsync(x => x.QuestionnaireId == request.QuestionnaireId && listQuestionId.Contains(x.QuestionId));
    //    questions = questions.ToList();
    //    var stepBasicInfo = await _stepDA.GetStepBaseInfoByStepIdTask(request.StepId);

    //    var result = new AssessmentResponse()
    //    {
    //        QuestionnaireId = request.QuestionnaireId,
    //        StepId = request.StepId
    //    };

    //    var currentUserId = _httpContextAccessor.HttpContext.User.FindFirst(Constants.ClaimTypes.UserId).Value.ToGuid();

    //    var executionStrategy = _iIGLmsdbContext.Database.CreateExecutionStrategy();

    //    var queryDelete = "";

    //    // Delete web_user_choose => then insert
    //    var listAnswerResponse = GenerateAnswerResponse(questionnaire.Type, questions, request.AnswerChooseRequest);
    //    foreach (var item in listAnswerResponse)
    //    {
    //        var deleteRequest = new WebUserChooseDelete
    //        {
    //            WebUserId = currentUserId,
    //            StepId = request.StepId,
    //            QuestionId = item.QuestionId
    //        };

    //        switch (questionnaire.Type)
    //        {
    //            case EQuestionnaireType.Video:
    //            case EQuestionnaireType.MCQ:
    //            case EQuestionnaireType.MCQImage:
    //            case EQuestionnaireType.TrueFalse:
    //                // delete by questionId
    //                break;

    //            case EQuestionnaireType.ImageDragDrop:
    //            case EQuestionnaireType.Droplist:
    //            case EQuestionnaireType.FillInTheBlank:
    //                // delete by questionId && answerId
    //                deleteRequest.AnswerId = item.AnswerId;
    //                break;
    //            case EQuestionnaireType.Matching:
    //            case EQuestionnaireType.MatchingImage:
    //                // delete by questionId && answerId && MatchingQuestionId
    //                deleteRequest.AnswerId = item.AnswerId;
    //                deleteRequest.MatchingQuestionId = item.MatchingQuestionId;
    //                break;
    //            default:
    //                throw new ArgumentOutOfRangeException(WebConstants.ValidationMessages.AssessmentMessage.QuestionnaireTypeIsNotValid);
    //        }

    //        //queryDelete += @"DELETE FROM [dbo].[web_user_choose]
    //        //             WHERE [web_user_id] = '" + deleteRequest.WebUserId + @"'
    //        //              AND [question_id] =  '" + deleteRequest.QuestionId + @"'
    //        //              AND [step_id] = '" + deleteRequest.StepId + "'" +
    //        //                    (deleteRequest.AnswerId == null ? "" : " AND [answer_id] = '" + deleteRequest.AnswerId + "'") +
    //        //                    (deleteRequest.MatchingQuestionId == null ? "" : " AND [matching_question_id] = '" + deleteRequest.MatchingQuestionId + "'") + ";";

    //        var existed = await _iIGLmsdbContext.WebUserChooses.FirstOrDefaultAsync(x => x.WebUserId == currentUserId && x.QuestionId == item.QuestionId && x.StepId == request.StepId && x.AnswerId == item.AnswerId);
    //        if (existed != null)
    //        {
    //            existed.AnswerText = item.AnswerText;
    //            existed.CorrectAnswer = item.IsCorrect;
    //            existed.MatchingQuestionId = item.MatchingQuestionId;
    //            existed.LessonId = stepBasicInfo.LessonId;
    //            existed.UnitId = stepBasicInfo.UnitId;
    //            existed.CourseId = stepBasicInfo.CourseId;

    //            _iIGLmsdbContext.WebUserChooses.Update(existed);
    //        }
    //        else
    //        {
    //            var webUserChoose = new WebUserChoose()
    //            {
    //                WebUserId = currentUserId,
    //                QuestionId = item.QuestionId,
    //                AnswerId = item.AnswerId,
    //                StepId = request.StepId,
    //                AnswerText = item.AnswerText,
    //                CorrectAnswer = item.IsCorrect,
    //                MatchingQuestionId = item.MatchingQuestionId,
    //                LessonId = stepBasicInfo.LessonId,
    //                UnitId = stepBasicInfo.UnitId,
    //                CourseId = stepBasicInfo.CourseId
    //            };
    //            _iIGLmsdbContext.WebUserChooses.AddRange(webUserChoose);
    //        }
    //        await _iIGLmsdbContext.SaveChangesAsync();
    //        result.AnswerChooseResponse.Add(item);
    //    }


    //    //await executionStrategy.ExecuteAsync(async () =>
    //    //{
    //    //    using (var transaction = _iIGLmsdbContext.Database.BeginTransaction(IsolationLevel.Serializable))
    //    //    {
    //    //        await _iIGLmsdbContext.Database.ExecuteSqlRawAsync(queryDelete);
    //    //        await transaction.CommitAsync();
    //    //        await _iIGLmsdbContext.SaveChangesAsync();
    //    //    }
    //    //});

    //    return result;
    //}
    //public async Task<List<AssessmentSeeAnswerResponse>> SeeAnswerAsync(AssessmentSeeAnswerRequest request)
    //{



    //    var stepQuestionnaire = await _stepQuestionnaireDA.GetInfo(request.StepId, request.QuestionnaireId);

    //    if (stepQuestionnaire == null)
    //    {
    //        throw new ApiNotFoundException(WebConstants.ValidationMessages.WebQuestionnaireMessage.StepQuestionnaireNotFoundKey);
    //    }

    //    await _myCourseBiz.CurrentUserAccessCourseValidationAsync(stepQuestionnaire.CourseId);

    //    var questionnaire = await _mongoQuestionnaireRepos.FindByIdAsync(x => x.QuestionnaireId == request.QuestionnaireId);
    //    if (questionnaire == null)
    //    {
    //        throw new ApiNotFoundException(WebConstants.ValidationMessages.WebQuestionnaireMessage.QuestionnaireNotFoundKey);
    //    }

    //    var questions = await _mongoQuestionRepos.FilterAsync(x => x.QuestionnaireId == request.QuestionnaireId);

    //    return GenerateCorrectAnswers(questionnaire.Type, questions);
    //}

    //public async Task<MicrosoftPronunciationAssessmentResultDto> EvaluatePronunciationAsync(PronunciationPostRequest request)
    //{

    //    var currentUserId = _httpContextAccessor.HttpContext.User.FindFirst(Constants.ClaimTypes.UserId).Value.ToGuid();

    //    // validate data
    //    var stepQuestionnaire = await _stepQuestionnaireDA

    //        .GetInfo(request.StepId, request.QuestionnaireId);
    //    if (stepQuestionnaire == null)
    //        throw new ApiNotFoundException(WebConstants.ValidationMessages.WebQuestionnaireMessage.StepQuestionnaireNotFoundKey);

    //    var isAccessed = await _myCourseBiz.CurrentUserAccessCourseValidationAsync(stepQuestionnaire.CourseId);
    //    if (!isAccessed)
    //        throw new ApiValidationException(WebConstants.ValidationMessages.MyCourseMessage.UserCannotAccessCourseKey, null);

    //    var questionnaire = await _mongoQuestionnaireRepos.FindByIdAsync(x => x.QuestionnaireId == request.QuestionnaireId);
    //    if (questionnaire == null)
    //        throw new ApiNotFoundException(WebConstants.ValidationMessages.WebQuestionnaireMessage.QuestionnaireNotFoundKey);

    //    var question = await _mongoQuestionRepos.FindByIdAsync(x => x.QuestionId == request.QuestionId);
    //    if (question == null)
    //        throw new ApiNotFoundException(WebConstants.ValidationMessages.WebQuestionnaireMessage.QuestionNotFoundKey);

    //    //// validate public file path is null or empty
    //    //var publicFilePath = _settingOption.PublicFileBaseUrl;
    //    //if (publicFilePath.IsNullOrEmpty())
    //    //    throw new ApiValidationException(WebConstants.ValidationMessages.AssessmentMessage.PublicFilePathNotFound, null);

    //    //// upload physical audio file
    //    //var newRecordFileId = Guid.NewGuid();
    //    //var uploadedFile = await _fileUploaderService
    //    //    .FileUploadAsync(request.File, newRecordFileId, EFileTypeIdentifier.PracticeAudioFiles, true);

    //    //// insert file data
    //    //uploadedFile.Id = newRecordFileId;
    //    //uploadedFile.IsMigrate = true;
    //    //await _fileService.InsertFileAsync(uploadedFile);

    //    // get question sentence by question id
    //    var questionSentence = question.WordEnglish;

    //    // get current language
    //    var languageCode = await _httpRequestService.GetCurrentLanguageCode();

    //    //var deletedFileName = "";

    //    //// get record file id
    //    //var oldRecordFileId = await _webUserPracticeHistoryDA

    //    //    .GetRecordFileIdAsync(currentUserId, new WebUserPracticeHistoryRequest()
    //    //    {
    //    //        QuestionId = request.QuestionId,
    //    //        QuestionnaireId = request.QuestionnaireId,
    //    //        StepId = request.StepId
    //    //    });
    //    //if (oldRecordFileId.HasValue)
    //    //{
    //    //    // if exist -> update record file id only
    //    //    await _webUserPracticeHistoryDA

    //    //        .UpdateRecordFileAsync(currentUserId, new WebUserPracticeHistoryRequest()
    //    //        {
    //    //            QuestionId = request.QuestionId,
    //    //            QuestionnaireId = request.QuestionnaireId,
    //    //            StepId = request.StepId
    //    //        }, newRecordFileId);

    //    //    // get file dto
    //    //    var fileDto = await _fileService.GetDtoByIdAsync(oldRecordFileId.Value);

    //    //    deletedFileName = fileDto.FileName;

    //    //    // delete old data file
    //    //    await _fileService.DeleteFileAsync(oldRecordFileId.Value);
    //    //}
    //    //else
    //    //{
    //    //    // if not -> insert new
    //    //    await _webUserPracticeHistoryDA

    //    //        .InsertAsync(Guid.NewGuid(), currentUserId, questionnaire.Type, new WebUserPracticeHistoryRequest()
    //    //        {
    //    //            QuestionId = request.QuestionId,
    //    //            QuestionnaireId = request.QuestionnaireId,
    //    //            StepId = request.StepId
    //    //        }, newRecordFileId);
    //    //}
    //    //var tempUrl = await _swiftStorageService.GetTempUrlAsync();
    //    //if (tempUrl == null)
    //    //{
    //    //    throw new ApiNotFoundException(WebConstants.VngStorage.ErrorVngCloud);
    //    //}
    //    //// get audio path
    //    //var audioPath = Path.Combine(tempUrl.TempUrl, Constants.FileSettings.SubFolderPublic.EnsureEndWithSlash(), uploadedFile.FileName);

    //    //// get result

    //    var result = await _microsoftPronunciationAssessmentService.EvaluatePronuciationAssessmentAsync(request.File, questionSentence);

    //    //if (!deletedFileName.IsNullOrEmpty())
    //    //{
    //    //    // delete old physical file
    //    //    await _storageService.DeleteFile(deletedFileName, true);
    //    //}

    //    return result;
    //}

    //private List<AssessmentSeeAnswerResponse> GenerateCorrectAnswers(EQuestionnaireType type, IEnumerable<MgQuestionModel> questions)
    //{
    //    var listResult = new List<AssessmentSeeAnswerResponse>();

    //    foreach (var mgQuestion in questions)
    //    {
    //        var correctAnswer = new AssessmentSeeAnswerResponse();
    //        switch (type)
    //        {
    //            case EQuestionnaireType.Video:
    //            case EQuestionnaireType.MCQ:
    //            case EQuestionnaireType.MCQImage:
    //            case EQuestionnaireType.TrueFalse:
    //                correctAnswer.QuestionId = mgQuestion.QuestionId;
    //                correctAnswer.CorrectAnswerId = mgQuestion.Answers
    //                    .FirstOrDefault(x => x.CorrectMCQ.HasValue && x.CorrectMCQ.Value)?.Id;
    //                listResult.Add(correctAnswer);
    //                break;
    //            case EQuestionnaireType.ImageDragDrop:
    //            case EQuestionnaireType.Droplist:
    //            case EQuestionnaireType.FillInTheBlank:
    //                var listAnswer = mgQuestion
    //                    .Answers
    //                    .Select(x => new AssessmentSeeAnswerResponse()
    //                    {
    //                        AnswerId = x.Id,
    //                        CorrectAnswerText = x.CorrectMatchingValues.Split(';', StringSplitOptions.TrimEntries).ToList(),
    //                        QuestionId = x.QuestionId
    //                    });
    //                listResult.AddRange(listAnswer);
    //                break;
    //            case EQuestionnaireType.Matching:
    //            case EQuestionnaireType.MatchingImage:
    //                correctAnswer.QuestionId = mgQuestion.QuestionId;
    //                correctAnswer.CorrectAnswerQuestionMatchings = mgQuestion.CorrectAnswerQuestionMatchings;
    //                listResult.Add(correctAnswer);
    //                break;
    //            default:
    //                throw new ArgumentOutOfRangeException(WebConstants.ValidationMessages.AssessmentMessage.QuestionnaireTypeIsNotValid);
    //        }
    //    }
    //    return listResult;
    //}

    //private IEnumerable<AnswerChooseResponse> GenerateAnswerResponse(EQuestionnaireType type, IEnumerable<MgQuestionModel> questions, List<AnswerChooseRequest> answerChooseRequests)
    //{
    //    foreach (var submitAnswer in answerChooseRequests)
    //    {
    //        var itemRes = new AnswerChooseResponse()
    //        {
    //            AnswerId = submitAnswer.AnswerId,
    //            QuestionId = submitAnswer.QuestionId,
    //            AnswerText = submitAnswer.AnswerText,
    //            MatchingQuestionId = submitAnswer.MatchingQuestionId
    //        };

    //        switch (type)
    //        {
    //            case EQuestionnaireType.Video:
    //            case EQuestionnaireType.MCQ:
    //            case EQuestionnaireType.MCQImage:
    //            case EQuestionnaireType.TrueFalse:
    //                itemRes.CorrectAnswerId = questions
    //                    .FirstOrDefault(x => x.QuestionId.Equals(submitAnswer.QuestionId))
    //                    ?.Answers.FirstOrDefault(x => x.CorrectMCQ.HasValue && x.CorrectMCQ.Value)?.Id;
    //                itemRes.IsCorrect = submitAnswer.AnswerId == itemRes.CorrectAnswerId;

    //                break;

    //            case EQuestionnaireType.ImageDragDrop:
    //            case EQuestionnaireType.Droplist:
    //            case EQuestionnaireType.FillInTheBlank:
    //                string correctMatchingValues = questions
    //                    .FirstOrDefault(x => x.QuestionId.Equals(submitAnswer.QuestionId))
    //                    ?.Answers.FirstOrDefault(x => x.Id.Equals(submitAnswer.AnswerId))?.CorrectMatchingValues;

    //                itemRes.CorrectAnswerText = correctMatchingValues?.Split(';', StringSplitOptions.TrimEntries)?.ToList();
    //                itemRes.IsCorrect = itemRes.CorrectAnswerText != null
    //                    && itemRes.CorrectAnswerText
    //                        .Any(x => !string.IsNullOrWhiteSpace(x) && !string.IsNullOrWhiteSpace(submitAnswer.AnswerText) && x.ToLower().Equals(submitAnswer.AnswerText.ToLower()));

    //                break;
    //            case EQuestionnaireType.Matching:
    //            case EQuestionnaireType.MatchingImage:
    //                itemRes.CorrectAnswerId = questions
    //                    .FirstOrDefault(x => x.QuestionId.Equals(submitAnswer.QuestionId))
    //                    ?.CorrectAnswerQuestionMatchings.FirstOrDefault(x => x.MatchingQuestionId == submitAnswer.MatchingQuestionId)?.AnswerId;
    //                itemRes.IsCorrect = submitAnswer.AnswerId == itemRes.CorrectAnswerId;
    //                break;
    //            default:
    //                throw new ArgumentOutOfRangeException(WebConstants.ValidationMessages.AssessmentMessage.QuestionnaireTypeIsNotValid);
    //        }

    //        yield return itemRes;
    //    }
    //}

    //private void ValidateAssessment(EQuestionnaireType type, List<AnswerChooseRequest> answerChooseRequests)
    //{
    //    switch (type)
    //    {
    //        case EQuestionnaireType.Video:
    //        case EQuestionnaireType.MCQ:
    //        case EQuestionnaireType.MCQImage:
    //        case EQuestionnaireType.TrueFalse:
    //        case EQuestionnaireType.Matching:
    //        case EQuestionnaireType.MatchingImage:
    //            break;

    //        case EQuestionnaireType.ImageDragDrop:
    //        case EQuestionnaireType.Droplist:
    //        case EQuestionnaireType.FillInTheBlank:
    //            ValidateAssessmentDropTypeForCheck(answerChooseRequests);
    //            break;

    //        default:
    //            throw new ArgumentOutOfRangeException(WebConstants.ValidationMessages.AssessmentMessage.QuestionnaireTypeIsNotValid);
    //    }
    //}

    //private void ValidateAssessmentDropTypeForCheck(List<AnswerChooseRequest> answerChooseRequests)
    //{
    //    if (answerChooseRequests.Any(x => string.IsNullOrEmpty(x.AnswerText)))
    //    {
    //        throw new ApiValidationException(WebConstants.ValidationMessages.AssessmentMessage.AnswerTextRequiredKey, nameof(answerChooseRequests));
    //    }
    //}

    //public async Task RedoAnswerV2Async(AssessmentRedoRequest request)
    //{
    //    var stepQuestionnaire = await _stepQuestionnaireDA.GetInfo(request.StepId, request.QuestionnaireId);

    //    if (stepQuestionnaire == null)
    //    {
    //        throw new ApiNotFoundException(WebConstants.ValidationMessages.WebQuestionnaireMessage.StepQuestionnaireNotFoundKey);
    //    }

    //    var checkAccess = await _myCourseBiz.CurrentUserAccessCourseValidationAsync(stepQuestionnaire.CourseId);
    //    if (!checkAccess)
    //    {
    //        await _myCourseBiz.StepFirstOrderValidationAsync(stepQuestionnaire.CourseId, request.StepId);
    //    }
    //    var questionnaire = await _mongoQuestionnaireRepos.FindByIdAsync(x => x.QuestionnaireId == request.QuestionnaireId);
    //    if (questionnaire == null)
    //    {
    //        throw new ApiNotFoundException(WebConstants.ValidationMessages.WebQuestionnaireMessage.QuestionnaireNotFoundKey);
    //    }

    //    var questions = await _mongoQuestionRepos.FilterAsync(x => x.QuestionnaireId == request.QuestionnaireId);
    //    var listQuestionId = questions?.Select(f => f.QuestionId)?.Distinct()?.ToList();
    //    var currentUserId = _httpContextAccessor.HttpContext.User.FindFirst(Constants.ClaimTypes.UserId).Value.ToGuid();
    //    var listWebUserChooses = await _iIGLmsdbContext.WebUserChooses.Where(f => f.WebUserId == currentUserId && listQuestionId.Contains(f.QuestionId) && f.StepId == request.StepId).ToListAsync();
    //    //var listSWChoose = await _iIGLmsdbContext.SpeakingWritingChooses.Include(x => x.CourseScoring).Where(x => x.CourseScoring != null && x.CourseScoring.WebUserId == currentUserId && listQuestionId.Contains(x.QuestionId.Value) && x.CourseScoring.StepId == request.StepId).ToListAsync();
    //    //var listSWResult = await _iIGLmsdbContext.SpeakingWritingResults.Where(x => listSWChoose.Select(c => c.Id).ToList().Contains(x.ChooseId.Value)).ToListAsync();

    //    var redoCount = await _iIGLmsdbContext.Steps.Where(x => x.Id == request.StepId).ExecuteUpdateAsync(x => x.SetProperty(x => x.RedoNumber, x => !x.RedoNumber.HasValue ? null : x.RedoNumber > 0 ? x.RedoNumber - 1 : 0).SetProperty(x => x.Modified, DateTime.UtcNow));

    //    _iIGLmsdbContext.WebUserChooses.RemoveRange(listWebUserChooses);
    //    //_iIGLmsdbContext.SpeakingWritingResults.RemoveRange(listSWResult);
    //    //_iIGLmsdbContext.SpeakingWritingChooses.RemoveRange(listSWChoose);

    //    var courseScoring = await _iIGLmsdbContext.CourseScorings.FirstOrDefaultAsync(x => x.WebUserId == currentUserId && x.StepId == request.StepId);
    //    if (courseScoring != null)
    //    {
    //        courseScoring.IsLatest = false;
    //    }

    //    await _iIGLmsdbContext.SaveChangesAsync();

    //}
    //public async Task RedoAnswerAsync(AssessmentRedoRequest request)
    //{




    //    var stepQuestionnaire = await _stepQuestionnaireDA

    //        .GetInfo(request.StepId, request.QuestionnaireId);

    //    if (stepQuestionnaire == null)
    //    {
    //        throw new ApiNotFoundException(WebConstants.ValidationMessages.WebQuestionnaireMessage.StepQuestionnaireNotFoundKey);
    //    }

    //    var checkAccess = await _myCourseBiz.CurrentUserAccessCourseValidationAsync(stepQuestionnaire.CourseId);
    //    if (!checkAccess)
    //    {
    //        await _myCourseBiz.StepFirstOrderValidationAsync(stepQuestionnaire.CourseId, request.StepId);
    //    }
    //    var questionnaire = await _mongoQuestionnaireRepos.FindByIdAsync(x => x.QuestionnaireId == request.QuestionnaireId);
    //    if (questionnaire == null)
    //    {
    //        throw new ApiNotFoundException(WebConstants.ValidationMessages.WebQuestionnaireMessage.QuestionnaireNotFoundKey);
    //    }

    //    var questions = await _mongoQuestionRepos.FilterAsync(x => x.QuestionnaireId == request.QuestionnaireId);

    //    var currentUserId = _httpContextAccessor.HttpContext.User.FindFirst(Constants.ClaimTypes.UserId).Value.ToGuid();
    //    foreach (var item in questions)
    //    {
    //        await _webUserChooseDA.DeleteAsync(new WebUserChooseDelete()
    //        {
    //            WebUserId = currentUserId,
    //            QuestionId = item.QuestionId,
    //            StepId = request.StepId,
    //        });
    //    }


    //}

    public IEnumerable<ChooseBaseModelResponse> VerifyAnswerAsync(IEnumerable<MgQuestionModel> questions, IEnumerable<ChooseBaseModelRequest> listChooseModel)
    {
        var listResult = new List<ChooseBaseModelResponse>();

        foreach (var itemChoose in listChooseModel)
        {
            var itemRes = _mapper.Map<ChooseBaseModelResponse>(itemChoose);

            switch (itemChoose.QuestionnaireType)
            {
                case EQuestionnaireType.MCQ:
                case EQuestionnaireType.MCQImage:
                case EQuestionnaireType.TrueFalse:
                    itemRes.CorrectAnswerId = questions
                        .FirstOrDefault(x => x.QuestionId.Equals(itemChoose.QuestionId))
                        ?.Answers.FirstOrDefault(x => x.CorrectMCQ.HasValue && x.CorrectMCQ.Value)?.Id;
                    itemRes.CorrectAnswer = itemChoose.AnswerId == itemRes.CorrectAnswerId;

                    break;

                case EQuestionnaireType.ImageDragDrop:
                case EQuestionnaireType.Droplist:
                case EQuestionnaireType.FillInTheBlank:
                    string correctMatchingValues = questions
                        .FirstOrDefault(x => x.QuestionId.Equals(itemChoose.QuestionId))
                        ?.Answers.FirstOrDefault(x => x.Id.Equals(itemChoose.AnswerId))?.CorrectMatchingValues;

                    itemRes.CorrectAnswerText = correctMatchingValues?.Split(';', StringSplitOptions.TrimEntries)?.ToList();
                    itemRes.CorrectAnswer = itemRes.CorrectAnswerText != null
                        && itemRes.CorrectAnswerText
                            .Any(x => !string.IsNullOrWhiteSpace(x) && !string.IsNullOrWhiteSpace(itemChoose.AnswerText) && x.ToLower().Equals(itemChoose.AnswerText.ToLower()));

                    break;
                case EQuestionnaireType.Matching:
                case EQuestionnaireType.MatchingImage:
                    itemRes.CorrectAnswerId = questions
                        .FirstOrDefault(x => x.QuestionId.Equals(itemChoose.QuestionId))
                        ?.CorrectAnswerQuestionMatchings.FirstOrDefault(x => x.MatchingQuestionId == itemChoose.MatchingQuestionId)?.AnswerId;
                    itemRes.CorrectAnswer = itemChoose.AnswerId == itemRes.CorrectAnswerId;
                    break;

                case EQuestionnaireType.Video:
                case EQuestionnaireType.Slide:
                case EQuestionnaireType.Writing:
                case EQuestionnaireType.Record:
                case EQuestionnaireType.ReadTextALoud:
                    itemRes.CorrectAnswer = true;
                    itemRes.AnswerId = Guid.NewGuid(); // fake value
                    break;

                case EQuestionnaireType.PronunciationRecognition:
                case EQuestionnaireType.FlashCard:
                    itemRes.CorrectAnswer = questions.Any(x => x.QuestionId.Equals(itemChoose.QuestionId));
                    itemRes.AnswerId = Guid.NewGuid(); // fake value
                    break;
                default:
                    throw new ArgumentOutOfRangeException(WebConstants.ValidationMessages.AssessmentMessage.QuestionnaireTypeIsNotValid);
            }

            listResult.Add(itemRes);
        }

        return listResult;
    }
}