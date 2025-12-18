using IIG.Application.Models.MockTestPartQuestionnaire;
using IIG.Application.Services;
using IIG.Core.Base;
using IIG.Core.Common.Enums;
using IIG.Core.Common.Models.Paging;
using IIG.Core.Common.Models.SpeakingAndWriting;
using IIG.Core.Entities;
using IIG.Core.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace IIG.Application.Data;

public class MockTestPartQuestionnaireDA : IMockTestPartQuestionnaireDA
{
    private readonly IAppFactory _appFactory;
    private readonly IFileService _fileService;

    public MockTestPartQuestionnaireDA(IAppFactory appFactory, IFileService fileService)
    {
        _appFactory = appFactory;
        _fileService = fileService;
    }

    public async Task<PaginationSet<MockTestPartQuestionnaireAssignedListModel>> GetQuestionnaireListByMockTestPartIdAsync(Guid mockTestPartId, SearchingMockTestPartQuestionnaireRequest request, DateTime? publishedAt = null)
    {
        var response = new PaginationSet<MockTestPartQuestionnaireAssignedListModel>();

        var mocktestSectionType = await _appFactory.Repository<MocktestPart>().GetAll().Include(x => x.MocktestSection).AsNoTracking()
                                                            .Where(x => x.Id == mockTestPartId).Select(p => p.MocktestSection.Type).FirstOrDefaultAsync();

        //Kiểm tra dạng đề Record-nonstop hay Writing-nonstop
        if ((EMockTestSectionType)mocktestSectionType == EMockTestSectionType.RecordNonstop ||
            (EMockTestSectionType)mocktestSectionType == EMockTestSectionType.WritingNonstop)
        {

            var result = await GetQuestionnaireSWNonStopByMockTestPartId(mockTestPartId, (EMockTestSectionType)mocktestSectionType, request);
            return result;
        }

        var baseQuery =
          from mpq in _appFactory.Repository<MocktestPartQuestionnaire>().GetAll().AsNoTracking()
          join q in _appFactory.Repository<Questionnaire>().GetAll().AsNoTracking()
              on mpq.QuestionnaireId equals q.Id
          join mp in _appFactory.Repository<MocktestPart>().GetAll().AsNoTracking()
              on mpq.MocktestPartId equals mp.Id
          join qqv in _appFactory.Repository<QuestionnaireQuestionView>().GetAll()
              on q.Id equals qqv.QuestionnaireId into qqvJoin
          from qqv in qqvJoin.DefaultIfEmpty()

          where mpq.MocktestPartId == mockTestPartId
          select new
          {
              q.Id,
              q.Identifier,
              q.Name,
              q.Type,
              q.IsActive,
              mpq.SortOrder,
              DelayTime = mp.DelayTimeEachQuestion,
              QuestionCount = qqv.NumberOfQuestions ?? 0,
              AudioDuration = q.LeftSections.Sum(p=>p.AudioDuration) ?? 0
          };

        if (!string.IsNullOrEmpty(request.Keyword))
        {
            baseQuery = baseQuery.Where(x =>
                EF.Functions.Like(
                    EF.Property<string>(x, "FullTextSearch"),
                    request.KeywordFullTextSearch));
        }

     
        var totalRecords = await baseQuery
            .Select(x => x.Id)
            .Distinct()
            .CountAsync();

        var items = await baseQuery
                        .GroupBy(x => new
                        {
                            x.Id,
                            x.Identifier,
                            x.Name,
                            x.Type,
                            x.IsActive,
                            x.SortOrder,
                            x.DelayTime,
                            x.QuestionCount
                        })
                        .OrderBy(g => g.Key.SortOrder)
                        .Skip((request.PageNum - 1) * request.PageSize)
                        .Take(request.PageSize)
                        .Select(g => new MockTestPartQuestionnaireAssignedListModel
                        {
                            Id = g.Key.Id,
                            Identifier = g.Key.Identifier,
                            Name = g.Key.Name,
                            Type = (EQuestionnaireType)g.Key.Type,
                            IsActive = g.Key.IsActive,
                            NumberOfQuestions = g.Key.QuestionCount,
                            NumberOfTime = (int)(g.Key.DelayTime + g.Sum(x => x.AudioDuration))
                        })
                        .ToListAsync();

        return response;
    }


    private async Task<PaginationSet<MockTestPartQuestionnaireAssignedListModel>> GetQuestionnaireSWNonStopByMockTestPartId(Guid mockTestPartId, EMockTestSectionType sectionType, SearchingMockTestPartQuestionnaireRequest request)
    {
        var response = new PaginationSet<MockTestPartQuestionnaireAssignedListModel>();

        var questionnaires = _appFactory.Repository<MocktestPartQuestionnaire>().GetAll()
                                   .Include(x => x.Questionnaire).ThenInclude(x => x.LeftSections)
                                   .Include(x => x.Questionnaire).ThenInclude(x => x.Questions)
                                   .AsNoTracking()
                                   .Where(x => x.MocktestPartId == mockTestPartId)
                                   .Select(x => new
                                   {
                                       x.Questionnaire.Id,
                                       Identifier = x.Questionnaire.Identifier,
                                       x.Questionnaire.Name,
                                       x.Questionnaire.FullTextSearch,
                                       x.Questionnaire.IsActive,
                                       x.Questionnaire.Type,
                                       x.TotalTime,
                                       x.IsMarkByAi,
                                       x.SortOrder,
                                       LeftSections = x.Questionnaire.LeftSections.Select(l => new
                                       {
                                           l.Id,
                                           l.AudioDuration,
                                           l.SpaceTime,
                                           l.AudioFileId,
                                           l.VideoId
                                       }),
                                       Questions = x.Questionnaire.Questions.Select(q => new
                                       {
                                           q.AudioDuration,
                                           q.SampleTemplateJsonObject,
                                           q.SpaceTime,
                                           q.RecordingTime,
                                           q.AudioFileId,
                                       })
                                   });

        if (!string.IsNullOrEmpty(request.Keyword))
        {
            questionnaires = questionnaires.Where(x => x.Identifier.Contains(request.Keyword) || x.FullTextSearch.Contains(request.Keyword));
        }

        //Paginated data
        var paginatedQuestionnaires = questionnaires
                                        .OrderBy(x => x.SortOrder)
                                        .Skip(PaginationExtensions.Skip(request.PageNum, request.PageSize)).Take(request.PageSize)
                                        .ToList();

        //Aggregate fileID related time of questionnaires
        var aggreateFileIds = paginatedQuestionnaires.SelectMany(x =>
        {
            var audioIds = x.LeftSections.Where(x => x.AudioFileId.HasValue).Select(x => x.AudioFileId.Value);
            var videoIds = x.LeftSections.Where(x => x.VideoId.HasValue).Select(x => x.VideoId.Value);

            var questionsIds = x.Questions.Where(x => x.AudioFileId.HasValue).Select(x => x.AudioFileId.Value);
            var questionTemplateIds = x.Questions.SelectMany(x =>
            {
                var sampletemplates = x.SampleTemplateJsonObject.ConvertSampleTemplate<SampleTemplateViewModel>();
                if (sampletemplates?.Any() == true)
                {
                    return sampletemplates.Where(x => x.AudioFileId.HasValue).Select(x => x.AudioFileId.Value);
                }

                return Enumerable.Empty<Guid>();
            });


            return audioIds.Concat(videoIds).Concat(questionsIds).Concat(questionTemplateIds);
        }).ToList();

        //Get file's time by fileIds
        var audioFileDurationDict = await _fileService.GetFilesDuration(aggreateFileIds);

        //Calculate totaltime of questions
        var items = paginatedQuestionnaires.Select(x =>
        {
            MockTestPartQuestionnaireAssignedListModel viewModel = new();
            viewModel.Id = x.Id;
            viewModel.Identifier = x.Identifier;
            viewModel.Name = x.Name;
            viewModel.NumberOfQuestions = x.Questions.Count();
            viewModel.IsActive = x.IsActive;
            viewModel.Type = (EQuestionnaireType)x.Type;
            viewModel.IsMarkByAI = x.IsMarkByAi;
            viewModel.TotalTime = x.TotalTime;
            viewModel.SortOrder = x.SortOrder;

            if ((EMockTestSectionType)sectionType == EMockTestSectionType.RecordNonstop)
            {

                var lefSectionTotalTime = x.LeftSections.Sum(c =>
                {
                    int total = 0;
                    if (c.SpaceTime.HasValue)
                        total += (int)c.SpaceTime.Value.TotalSeconds;

                    if (c.AudioFileId.HasValue)
                    {
                        total += audioFileDurationDict.TryGetValue(c.AudioFileId.Value, out var time) ? time : 0;
                    }

                    return total;
                });

                var questionTotalTime = x.Questions.Sum(c =>
                {
                    int total = 0;
                    if (c.AudioFileId.HasValue)
                    {
                        total += audioFileDurationDict.TryGetValue(c.AudioFileId.Value, out var time) ? time : 0;
                    }

                    if (c.SpaceTime.HasValue)
                        total += (int)c.SpaceTime.Value.TotalSeconds;

                    if (c.RecordingTime.HasValue)
                        total += (int)c.RecordingTime.Value.TotalSeconds;

                    var sampleTemplates = c.SampleTemplateJsonObject.ConvertSampleTemplate<SampleTemplateViewModel>();

                    if (sampleTemplates?.Any() == true)
                    {
                        total += sampleTemplates.Sum(t =>
                        {
                            int total = 0;
                            if (t.AudioFileId.HasValue)
                            {
                                total += audioFileDurationDict.TryGetValue(t.AudioFileId.Value, out var time) ? time : 0;
                            }
                            ;
                            return total;
                        });
                    }

                    return total;
                });

                viewModel.NumberOfTime = lefSectionTotalTime + questionTotalTime;

            }
            else if ((EMockTestSectionType)sectionType == EMockTestSectionType.WritingNonstop)
            {
                if (x.TotalTime.HasValue)
                {
                    viewModel.TotalTime = x.TotalTime;
                }
            }

            return viewModel;
        });

        response.TotalRecords = await questionnaires.CountAsync();
        response.PageNum = request.PageNum;
        response.PageSize = request.PageSize;
        response.Items = items;

        return response;
    }


    public async Task<IEnumerable<MockTestQuestionAnswerIdModel>> GetListQuestionAnswerIdByMockTestPartIdAsync(Guid mockTestPartId, DateTime? publishedAt = null)
    {
        var questionnaire = await (from q in _appFactory.Repository<Questionnaire>().GetAll()
                                   join mtpq in _appFactory.Repository<MocktestPartQuestionnaire>().GetAll() on q.Id equals mtpq.QuestionnaireId
                                   where mtpq.MocktestPartId == mockTestPartId
                                   select new
                                   {
                                       q.Id,
                                       q.Type,
                                   }).ToListAsync();

        var listQuestionnaireTypeNeedToGetQuestion = new List<short>
        {
            (short) EQuestionnaireType.MCQ,
            (short) EQuestionnaireType.MCQImage,
            (short) EQuestionnaireType.TrueFalse,
            (short) EQuestionnaireType.Record,
            (short) EQuestionnaireType.ReadTextALoud,
            (short) EQuestionnaireType.Writing,
        };
        var listQuestionnaireTypeNeedToGetAnswer = new List<short>
        {
            (short) EQuestionnaireType.ImageDragDrop,
            (short) EQuestionnaireType.Droplist,
            (short) EQuestionnaireType.FillInTheBlank,
            (short) EQuestionnaireType.Matching,
            (short) EQuestionnaireType.MatchingImage,
        };

        var listQuestionnaireNeedToGetQuestion = questionnaire.Where(p => listQuestionnaireTypeNeedToGetQuestion.Contains(p.Type)).Select(p => p.Id);
        var queryQuestion = from qs in _appFactory.Repository<Question>().GetAll()
                            where listQuestionnaireNeedToGetQuestion.Contains(qs.QuestionnaireId)
                            select new MockTestQuestionAnswerIdModel
                            {
                                Id = qs.Id,
                                QuestionnaireId = qs.QuestionnaireId,
                                SortOrder = qs.SortOrder ?? 0,
                                MatchingKey = ""
                            };
        var listQuestion = await queryQuestion.ToListAsync();
        var listQuestionnaireNeedToGetAnswer = questionnaire.Where(p => listQuestionnaireTypeNeedToGetAnswer.Contains(p.Type)).Select(p => p.Id);
        var queryAnswer = from answer in _appFactory.Repository<Answer>().GetAll()
                          where listQuestionnaireNeedToGetAnswer.Contains(answer.Question.QuestionnaireId)
                          select new MockTestQuestionAnswerIdModel
                          {
                              Id = answer.Id,
                              QuestionnaireId = answer.Question.QuestionnaireId,
                              SortOrder = answer.SortOrder ?? 0,
                              MatchingKey = answer.MatchingKey
                          };
        var listAnswer = await queryAnswer.ToListAsync();
        var result = listQuestion.Concat(listAnswer).OrderBy(p => p.SortOrder).ToList();
        return result;
    }
}