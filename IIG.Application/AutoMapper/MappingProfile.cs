using IIG.Core.Common.Models.Paging;
using IIG.Core.Common.MongoDataModels;
using IIG.Core.Common.MongoDataModels.CourseTests;
using IIG.Core.Common.MongoDataModels.Keycodes;
using IIG.Core.Common.MongoDataModels.MockTests;
using System.Text.Json;
using IIG.Core.Common.MongoDataModels.LiveClassTest;
using static IIG.Core.Common.ConfigureModels.Constants;
using IIG.Core.Common.Models.SpeakingAndWriting;
using IIG.Core.Helpers;
using IIG.Core.Common.Models.Files;
using IIG.Application.Models;
using IIG.Application.Models.Questionnaires;
using IIG.Application.Models.Versioning;
using IIG.Core.Entities;
using IIG.Application.Models.Lesson;
using IIG.Application.Models.UnitTest;
using IIG.Application.Models.TransferExamToolCategoryHistory;
using IIG.Application.Models.AccountBank;
using IIG.Application.Models.KeyCodes;
using IIG.Application.Models.MockTestSection;
using IIG.Application.Models.MockTest;
using IIG.Application.Models.MockTestPartQuestionnaire;
using IIG.Application.Models.Keycodes;
using IIG.Application.Models.KeycodeChooses;
using IIG.Application.Models.Unit;
using IIG.Web.Data.Models;
using IIG.Application.Models.LeftSection;
using File = IIG.Core.Entities.File;

namespace IIG.Application.AutoMapper;

public class MappingProfile : BaseMappingProfile
{
    public MappingProfile()
    {
        // mapping other models here
        CreateMap<NewsDto, NewsHomeModel>();

        CreateMap<MgQuestionnaireModel, QuestionnaireDto>();
        CreateMap<MgLeftSectionModel, LeftSectionDto>();
        //Speaking and writing

        CreateMap<MgMatchingQuestionJson, MatchingQuestionJson>();
        CreateMap<MgMatchingAnswerJson, MatchingAnswerJson>();
        CreateMap<MgAnswerQuestionMatching, AnswerQuestionMatching>();

        CreateMap<MatchingVersionQuestionJson, MatchingQuestionJson>();
        CreateMap<MatchingVersionAnswerJson, MatchingAnswerJson>();
        CreateMap<VersionAnswerQuestionMatching, AnswerQuestionMatching>();

        //Speaking and writing
        CreateMap<MgQuestionModel, QuestionDto>()
            .ForMember(x => x.SampleTemplateViewModels, opt => opt.MapFrom((src, dest) =>
            {
                if (string.IsNullOrEmpty(src.SampleTemplateJsonObject)) return null;

                var samples = src.SampleTemplateJsonObject.ConvertSampleTemplate<SampleTemplateViewModel>()?.OrderBy(x => x?.SortOrder ?? -1)?.ToList();
                return samples;
            }));

        CreateMap<MgUserSubmitLesson, SubmitLessonCourseRequestModel>().ReverseMap();
        CreateMap<UserCourseExcersiceChoose, SubmitLessonCourseRequestModel>().ReverseMap();
        CreateMap<MgUserSubmitLessonQuestionRequestModel, MgUserSubmitLessonQuestion>().ReverseMap();


        CreateMap<MgQuestionTranslationDto, QuestionTranslationDto>();
        CreateMap<MgAnswerDto, AnswerDto>();
        CreateMap<LessonDto, LessonListModel>();
        CreateMap<UnitTestDropDownListModel, UnitTestListModel>();
        CreateMap<ReviewInsertRequest, ReviewInsertModel>();
        CreateMap<AccountBankListModel, TransferExamToolCategoryHistoryModel>()
            .ForMember(x => x.CoursePriceId, opt => opt.Ignore())
            .ForMember(x => x.OrderId, opt => opt.Ignore())
            .ForMember(dest => dest.AccountBankId, act => act.MapFrom(src => src.Id));

        CreateMap<AccountBank, TransferExamToolCategoryHistoryModel>()
            .ForMember(x => x.CoursePriceId, opt => opt.Ignore())
            .ForMember(x => x.OrderId, opt => opt.Ignore())
            .ForMember(dest => dest.AccountBankId, act => act.MapFrom(src => src.Id));

        CreateMap<KeyCodeResultDto, KeyCodeResultModel>()
            .ForMember(x => x.Sections, opt => opt.MapFrom((src, dest) => string.IsNullOrEmpty(src.ComponentsDetails) ? null : JsonSerializer.Deserialize<List<SectionScoreBasicDto>>(src.ComponentsDetails)));

        // Mocktest
        CreateMap<MgMockTestModel, MockTestStructureModel>()
            .ForMember(x => x.MockTestName,
                act => act.MapFrom((src, _) =>
                {
                    if (src.GeneralInfo == null || src.GeneralInfo.Names.Count <= 0) return null;
                    var mockTestNameVn = src.GeneralInfo.Names.FirstOrDefault(n => n.LanguageCode == LanguageTags.TiengViet);
                    if (mockTestNameVn != null) return mockTestNameVn.Name;
                    return src.GeneralInfo.Names.FirstOrDefault()?.Name;
                }));
        CreateMap<MgMockTestModel, CourseTestMockTestStructureModel>();
        CreateMap<MgMockTestSectionModel, MockTestSectionStructureModel>();
        CreateMap<MgMockTestPartModel, MockTestPartStructureModel>();
        CreateMap<MgMockTestModel, LiveClassTestMockTestStructureModel>();

        CreateMap<MockTestSectionDto, MockTestSectionStructureModel>()
            .ForMember(x => x.Parts, opt => opt.Ignore());

        CreateMap<MockTestStructureModel, MockTestStructureWithQuestionTagModel>().ReverseMap();
        CreateMap<MockTestSectionStructureModel, MockTestSectionStructureWithQuestionTagModel>().ReverseMap();
        CreateMap<MockTestPartStructureModel, MockTestPartStructureWithQuestionTagModel>().ReverseMap();
        // mocktest
        CreateMap<MockTestTranslationDto, MgNameTranslationModel>();
        CreateMap<MockTestDetailModel, MgMockTestModel>();
        CreateMap<MockTestDetailModel, MgMockTestInfoModel>()
            .ForMember(dest => dest.Names, act => act.MapFrom(src => src.Translations))
            .ForMember(x => x.Tags, opt => opt.MapFrom((src, dest) =>
            {
                if (string.IsNullOrEmpty(src.Tags) || src.Tags.ToLower() == "null") return null;
                var matchingJsons = JsonSerializer.Deserialize<string[]>(src.Tags);

                return matchingJsons;
            }));
        CreateMap<MockTestSectionDto, MgMockTestSectionModel>()
            .ForMember(x => x.Parts, opt => opt.Ignore());
        CreateMap<MockTestPartQuestionnaireAssignedListModel, MgMockTestQuestionnaireInfo>();

        CreateMap(typeof(PaginationSet<>), typeof(PaginationSet<>));

        CreateMap<StartedDoingAnswerRequest, MgMockTestKeyCodeModel>();
        CreateMap<MgMockTestKeyCodeModel, StartedDoingAnswerModel>();
        CreateMap<MgMockTestKeyCodeModel, StartedDoingAnswerResponse>();

        // AnswerChoose of MockTest
        CreateMap<MgKeyCodeAnswerModel, ChooseBaseModelRequest>();
        CreateMap<ChooseBaseModelRequest, ChooseBaseModelResponse>();

        CreateMap<ChooseBaseModelResponse, KeyCodeChooseResponse>();
        CreateMap<SaveAnswerRequest, MgKeyCodeAnswerModel>().ForMember(x => x.Id, opt => opt.Ignore());
        CreateMap<MgKeyCodeAnswerModel, AnswerResponse>();

        // unit test
        CreateMap<MgQuestionModel, UnitTestMenuListModel>()
            .ForMember(dest => dest.Id, act => act.MapFrom(src => src.QuestionId));
        CreateMap<MgAnswerDto, UnitTestMenuListModel>();

        CreateMap<SaveUnitTestAnswerRequest, MgUnitTestAnswer>().ReverseMap();

        // AnswerChoose of CourseTest
        CreateMap<SaveCourseTestAnswerRequest, MgCourseTestAnswerModel>()
            .ForMember(x => x.Id, opt => opt.Ignore());
        CreateMap<MgCourseTestAnswerModel, ChooseBaseModelRequest>();
        CreateMap<ChooseBaseModelResponse, CourseTestChooseResponse>();
        CreateMap<MgMockTestModel, StartedDoingCourseTestResponse>();


        //CreateMap<MgCourseTestAnswerModel, ResponseQuestionModel>();

        CreateMap<WebUserCourseTestResultDto, WebUserCourseTestResultModel>()
            .ForMember(x => x.Sections, opt => opt.MapFrom((src, dest) => string.IsNullOrEmpty(src.ComponentsDetails) ? null : JsonSerializer.Deserialize<List<SectionCourseTestResultDto>>(src.ComponentsDetails)))
            .ForMember(x => x.Menus, opt => opt.MapFrom((src, dest) => string.IsNullOrEmpty(src.MockTestMenu) ? null : JsonSerializer.Deserialize<MgMockTestMenuModel>(src.MockTestMenu)));

        //Course Statistic
        CreateMap<CourseTestResultComponent, SectionStatisticDetailModel>();

        CreateMap<CategoryListByAreaModel, CategoryListModel>();

        CreateMap<UnitModel, UnitDto>();

        //LiveClassTest
        CreateMap<ChooseBaseModelResponse, LiveClassTestChooseResponse>();
        CreateMap<MgMockTestModel, StartedDoingLiveClassTestResponse>();
        CreateMap<MgLiveClassTestAnswerModel, ChooseBaseModelRequest>();
        CreateMap<SaveLiveClassTestAnswerRequest, MgLiveClassTestAnswerModel>()
            .ForMember(x => x.Id, opt => opt.Ignore());
        CreateMap<LiveClassTestResultDto, LiveClassTestResultModel>()
            .ForMember(x => x.Sections, opt => opt.MapFrom((src, dest) => string.IsNullOrEmpty(src.ComponentsDetails) ? null : JsonSerializer.Deserialize<List<LiveClassTestSectionScoreDto>>(src.ComponentsDetails)));

        CreateMap<SpeakingWritingResult, SpeakingWritingResultViewModel>();

        CreateMap<WebUserRegister, WebUserRegisterDto>().ReverseMap().ForMember(p=>p.RegisterType, opt=>opt.Ignore());


        MongoMappingModel();
    }

    private void MongoMappingModel()
    {
        CreateMap<LeftSectionModel, MgLeftSectionModel>()
           .ForMember(dest => dest.LeftSectionId, act => act.MapFrom(src => src.Id))
           .ForMember(x => x.Id, opt => opt.Ignore())
           .ForMember(x => x.ImageInfo, opt => opt.Ignore());

        CreateMap<QuestionnaireModel, MgQuestionnaireModel>()
           .ForMember(dest => dest.QuestionnaireId, act => act.MapFrom(src => src.Id))
           .ForMember(x => x.SubtitleInfo, opt => opt.Ignore())
           .ForMember(x => x.Id, opt => opt.Ignore())
           .ForMember(x => x.SlideInfo, opt => opt.Ignore());

        CreateMap<AnswerDto, MgAnswerDto>()
            .ForMember(x => x.ImageFileInfo, opt => opt.Ignore())
            .ForMember(x => x.DropListValue, opt => opt.Ignore());
        CreateMap<QuestionTranslationDto, MgQuestionTranslationDto>();
        CreateMap<QuestionDto, MgQuestionModel>()
          .ForMember(dest => dest.QuestionnaireId, act => act.MapFrom(src => src.QuestionnaireId))
          .ForMember(dest => dest.QuestionId, act => act.MapFrom(src => src.QuestionId))
          .ForMember(x => x.ImageFileInfo, opt => opt.Ignore())
          .ForMember(x => x.Id, opt => opt.Ignore())
          .ForMember(x => x.DragDropValues, opt => opt.Ignore())
          .ForMember(x => x.CorrectAnswerQuestionMatchings, opt => opt.MapFrom((src, dest) =>
          {
              if (string.IsNullOrEmpty(src.MatchingJson) || src.MatchingJson.ToLower() == "null") return null;
              var matchingJsons = JsonSerializer.Deserialize<List<MgMatchingJsonModel>>(src.MatchingJson);

              return matchingJsons.Select(x => new MgCorrectAnswerQuestionMatching
              {
                  MatchingQuestionId = x.MatchingQuestion.Id,
                  AnswerId = x.Answer.Id
              });
          }))
          .ForMember(x => x.AnswerQuestionMatching, opt => opt.MapFrom((src, dest) =>
          {
              if (string.IsNullOrEmpty(src.MatchingJson) || src.MatchingJson.ToLower() == "null") return null;
              var matchingJsons = JsonSerializer.Deserialize<List<MgMatchingJsonModel>>(src.MatchingJson);
              Random randSort = new();

              var question = matchingJsons.Select(m => m.MatchingQuestion).OrderBy(c => randSort.Next()).ToList();
              var answer = matchingJsons.Select(m => m.Answer).OrderBy(c => randSort.Next()).ToList();

              return new MgAnswerQuestionMatching
              {
                  Answers = answer,
                  Questions = question
              };
          }));

        //CreateMap<MgKeyCodeAnswerModel, ResponseQuestionModel>();

        CreateMap<FileInsertModel, File>().ReverseMap();

    }
}