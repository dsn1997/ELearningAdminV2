using AutoMapper;
using IIG.Web.BL.Services.Interfaces.MockTests;
using IIG.Core.Common.Enums;
using IIG.Core.Common.ErrorHandling;
using IIG.Core.Common.MongoDataModels.MockTests;


using Microsoft.Extensions.Logging;
using IIG.Core.Enums;
using IIG.Application.Models.MockTestSection;
using IIG.Application.Data;
using IIG.Application.Models.MockTestPartQuestionnaire;
using IIG.Core.Base;
using IIG.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace IIG.Application.Services;

public class MockTestDumpDataToMongoDbBiz : IMockTestDumpDataToMongoDbBiz
{

    private readonly IAppFactory _appFactory;
    private readonly IMapper _mapper;
    private readonly IMongoMockTestService _mongoMockTestService;
    private readonly IMockTestSectionDA _mockTestSectionDA;
    private readonly IMockTestDA _mockTestDA;
    private readonly IMockTestPartDA _mockTestPartDA;
    private readonly IMockTestPartQuestionnaireDA _mockTestPartQuestionnaireDA;

    private readonly ILogger<MockTestDumpDataToMongoDbBiz> _logger;

    public MockTestDumpDataToMongoDbBiz(
        IAppFactory appFactory,
        IMongoMockTestService mongoMockTestService, 
        IMapper mapper, 
        IMockTestDA mockTestDA,
        IMockTestSectionDA mockTestSectionDA,
        IMockTestPartDA mockTestPartDA, 
        IMockTestPartQuestionnaireDA mockTestPartQuestionnaireDA, 
        ILogger<MockTestDumpDataToMongoDbBiz> logger)
    {
        _appFactory = appFactory;
        _mapper = mapper;
        _mongoMockTestService = mongoMockTestService;
        _mockTestSectionDA = mockTestSectionDA;
        _mockTestDA = mockTestDA;
        _mockTestPartDA = mockTestPartDA;
        _mockTestPartQuestionnaireDA = mockTestPartQuestionnaireDA;
        _logger = logger;
    }

    public async Task DumpAsync(Guid id,bool? isModeToeflChallenge, DateTime? publishedAt = null)
    {
        
        await DumpInternalAsync(id, isModeToeflChallenge, publishedAt);
    }

    public async Task DumpInternalAsync(Guid id, bool? isModeToeflChallenge, DateTime? publishedAt = null)
    {
        var publicInfo = await _mockTestDA
            
            .GetPublicInfoAsync(id);
        if (publicInfo == null)
            throw new ApiValidationException(WebConstants.ValidationMessages.MockTestMessage.MockTestNotFound, nameof(publicInfo));

        if (publicInfo.Status == EMockTestStatus.NotActive)
        {
            throw new ApiValidationException(WebConstants.ValidationMessages.MockTestMessage.MockTestCannotDumpWithStatusNotActive, nameof(publicInfo));
        }

        if (publishedAt == null && publicInfo.PublishedAt == null)
        {
            return;
        }

        publishedAt ??= publicInfo.PublishedAt;

        var mocktest = await _mockTestDA
            
            .GetDetailByIdAsync(id, publishedAt);


        var mockTestMenu = new MgMockTestMenuModel();
        IEnumerable<MockTestSectionDto> mockTestSections;
        if (isModeToeflChallenge == true)
        {
            mockTestSections = await _mockTestSectionDA
            
                .GetListByMockTestChallengeAsync(id, publishedAt);
        }
        else
        {
            mockTestSections = await _mockTestSectionDA

                .GetListByMockTestAsync(id, publishedAt);
        }

        var mgMockTestSection = _mapper.Map<IEnumerable<MgMockTestSectionModel>>(mockTestSections).ToList();
        if (mgMockTestSection == null || !mgMockTestSection.Any()) return;

        int countIndexQuestion = 1;
        foreach (var section in mgMockTestSection)
        {
            section.NumberOfTime *= 60;
            var mgMockTestSectionModel = new MgMenuMockTestSectionModel
            {
                Id = section.Id,
                Name = section.Name,
                SortOrder = section.SortOrder,
                Type = section.Type,
                NumberOfTime = section.NumberOfTime
            };

            var parts = await _mockTestPartDA
                
                .GetListByMockTestSectionIdAsync(section.Id, publishedAt);
            _logger.LogError("parts: {@Model}" + parts);
            if (parts == null) continue;

            if (section.Type == EMockTestSectionType.NonStop ||
                    section.Type == EMockTestSectionType.RecordNonstop ||
                    section.Type == EMockTestSectionType.WritingNonstop
                )
            {
                var sectionRealTime = parts.Sum(x => x.NumOfTime);
                _logger.LogError("parts: " + sectionRealTime);
                section.NumberOfTime = sectionRealTime;
                mgMockTestSectionModel.NumberOfTime = sectionRealTime;
            }
            _logger.LogError("mgMockTestSectionModel: {@Model}" + mgMockTestSectionModel);

            var mgMenuParts = new List<MgMenuMockTestPartModel>();

            var mgParts = parts.Select(x => new MgMockTestPartModel
            {
                Id = x.Id,
                Name = x.Name,
                NumOfQuestions = x.NumOfQuestions,
                NumOfTime = x.NumOfTime,
                SortOrder = x.SortOrder,
                DelayTimeEachQuestion = x.DelayTimeEachQuestion,
                TotalPartTime = x.TotalPartTime
            }).ToList();

            foreach (var part in mgParts)
            {
                var partDetail = await _mockTestPartDA
                    
                    .GetDetailById(part.Id, publishedAt);

                part.Content = partDetail?.Content;
                part.IntroAudioFileId = partDetail?.IntroAudioFileId;
                part.DelayTimeEachQuestion = partDetail?.DelayTimeEachQuestion ?? 0;

                var paginationQuestionnaire = await _mockTestPartQuestionnaireDA
                    
                    .GetQuestionnaireListByMockTestPartIdAsync(part.Id, new SearchingMockTestPartQuestionnaireRequest
                    {
                        PageNum = 1,
                        PageSize = 1000,
                    }, publishedAt);

                part.Questionnaires = _mapper.Map<List<MgMockTestQuestionnaireInfo>>(paginationQuestionnaire.Items);
                var listQuestionAnswerIds = await _mockTestPartQuestionnaireDA
                    .GetListQuestionAnswerIdByMockTestPartIdAsync(part.Id, publishedAt);
                var grpQuestionAnswer = listQuestionAnswerIds?.GroupBy(x => x.QuestionnaireId);
               
                mgMenuParts.Add(new MgMenuMockTestPartModel
                {
                    Id = part.Id,
                    Name = part.Name,
                    SortOrder = part.SortOrder,
                    Questionnaires = part.Questionnaires.Select((x, index) =>
                    {

                        var grpQuestionAnswerGroupByMatchingKey = grpQuestionAnswer.FirstOrDefault(g => g.Key == x.Id).GroupBy(x => x.MatchingKey);
                        var questionList = new List<MgMenuQuestionModel>();
                        foreach (var item in grpQuestionAnswerGroupByMatchingKey)
                        {
                            var itemSort = item.OrderBy(x => x.SortOrder);
                            foreach (var item2 in itemSort)
                            {
                                var menuQuestion = new MgMenuQuestionModel
                                {
                                    Id = item2.Id,
                                    SortOrder = countIndexQuestion,
                                    Status = EAnswerStatus.NotAnswered
                                };
                                countIndexQuestion++;
                                questionList.Add(menuQuestion);
                            }
                        }

                        return new MgMenuQuestionnaireModel
                        {
                            Id = x.Id,
                            SortOrder = index,
                            Questions = questionList,
                            TotalTime = x.TotalTime,
                        };
                    }).ToList(),
                    DelayTimeEachQuestion = part.DelayTimeEachQuestion,
                    IntroAudioFileId = part.IntroAudioFileId,
                    Content = part.Content,
                    TotalPartTime = part.TotalPartTime
                });
            }

            mgMockTestSectionModel.Parts = mgMenuParts;
            mockTestMenu.Sections.Add(mgMockTestSectionModel);

            section.Parts = mgParts;
        }

        var mocktestType = await _appFactory.Repository<MocktestType>().GetAll().FirstOrDefaultAsync(p=>p.Id ==mocktest.MockTestTypeId);
        var mocktestObject = await _appFactory.Repository<MocktestObject>().GetAll().FirstOrDefaultAsync(p=>p.Id == mocktest.MockTestObjectId);

        var dataDump = new MgMockTestModel
        {
            MockTestId = mocktest.Id,
            GeneralInfo = _mapper.Map<MgMockTestInfoModel>(mocktest),
            MockTestSections = mgMockTestSection,
            MockTestMenu = mockTestMenu
        };

        dataDump.GeneralInfo.PublishedAt = publishedAt.Value;

        dataDump.GeneralInfo.MockTestType = new MgMockTestTypeModel
        {
            Id = mocktestType.Id,
            Name = mocktestType.Name
        };

        dataDump.GeneralInfo.MockTestObject = new MgMockTestObjectModel
        {
            Id = mocktestObject.Id,
            Name = mocktestObject.Name
        };

        await _mongoMockTestService.DeleteManyAsync(id);
        await _mongoMockTestService.InsertOneAsync(dataDump);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _mongoMockTestService.DeleteManyAsync(id);
    }

    public async Task<MgMockTestModel> GetOrDumpAsync(Guid id, bool? isTypeToeflChallenge)
    {
        if (isTypeToeflChallenge == null || isTypeToeflChallenge == false)
        {
            var currentVersion = await _mongoMockTestService.FindByIdAsync(id);
            if (currentVersion != null) return currentVersion;
        }

        await DumpAsync(id, isTypeToeflChallenge);
        return await _mongoMockTestService.FindByIdAsync(id);
    }
}