
using Microsoft.EntityFrameworkCore;
using System.Data;
using IIG.Core.Common.Enums;

using IIG.Core.Common.Models.SpeakingAndWriting;
using IIG.Core.Helpers;
using IIG.Core.Base;
using IIG.Application.Models.MockTestSection;
using IIG.Application.Models.MockTestPart;
using IIG.Core.Services;
using IIG.Core.Entities;
using File = IIG.Core.Entities.File;
using IIG.Application.Services;

namespace IIG.Application.Data;

public class MockTestSectionDA : IMockTestSectionDA
{
    private readonly IAppFactory _appFactory;
    private readonly IFileService _fileService;

    private readonly EMockTestSectionType[] _SWTYPES = new EMockTestSectionType[]
                                    {
                                            EMockTestSectionType.RecordNonstop,
                                            EMockTestSectionType.WritingNonstop
                                    };

    public MockTestSectionDA(IAppFactory appFactory,
                            IFileService fileService)
    {
        _appFactory = appFactory;
        _fileService = fileService;
    }

    public async Task<IEnumerable<MockTestSectionDto>> GetListByMockTestAsync(Guid mockTestId, DateTime? publishedAt = null)
    {
        var query = from mts in _appFactory.Repository<MocktestSection>().GetAll()
                    where mts.MocktestId == mockTestId
                    select new MockTestSectionDto
                    {
                        Id = mts.Id,
                        MockTestId = mts.MocktestId,
                        Name = mts.Name,
                        Type = (EMockTestSectionType)mts.Type,
                        SortOrder = mts.SortOrder,
                        NumberOfTime = mts.NumberOfTime ?? 0,
                        RankingScoreId = mts.RankingScoreId,
                        NumberOfQuestions = mts.NumberOfQuestions,     
                    };
        var results = await query.AsNoTracking().ToListAsync();
        return results;
    }

    public async Task<IEnumerable<MockTestSectionDto>> GetListByMockTestChallengeAsync(Guid mockTestId, DateTime? publishedAt = null)
    {
        var mockTestSectionDtoList = await GetListByMockTestAsync(mockTestId, publishedAt);
        foreach (var mockTestSectionDto in mockTestSectionDtoList)
        {
            if (mockTestSectionDto.Type != EMockTestSectionType.NonStop)
            {
                continue;
            }
            var query = from mts in _appFactory.Repository<MocktestSection>().GetAll()
                        join mtp in _appFactory.Repository<MocktestPartQuestionnaire>().GetAll()
                            on mts.MocktestId equals mtp.MocktestId
                        join ls in _appFactory.Repository<LeftSection>().GetAll()
                            on mtp.QuestionnaireId equals ls.QuestionnaireId
                        join f in _appFactory.Repository<File>().GetAll()
                            on ls.AudioFileId equals f.Id
                        where mts.Id == mockTestSectionDto.Id
                        let audioDuration = f.AudioDuration ?? 0
                        select new { AudioDuration = audioDuration };

            var sumOfDurations = await query.SumAsync(item => item.AudioDuration);

            mockTestSectionDto.NumberOfTime = (int)sumOfDurations;
        }
        return mockTestSectionDtoList;
    }

    public async Task<IEnumerable<MockTestPartInfoDto>> GetListMocktespartsOfSWType(Guid mockTestSectionId)
    {
        var mckTestSc = await _appFactory.Repository<MocktestSection>().GetAll()
                                                    .Include(x => x.Mocktest)
                                                    .AsNoTracking()
                                                    .FirstOrDefaultAsync(x => x.Id == mockTestSectionId);

        var mktestSecType = (EMockTestSectionType)mckTestSc.Type;

        //SW speaking, writing type
        //record speaking
        var mktestParts = await _appFactory.Repository<MocktestPart>().GetAll()
                                                    .Include(x => x.MocktestPartQuestionnaires).ThenInclude(x => x.Questionnaire).ThenInclude(x => x.LeftSections)
                                                    .Include(x => x.MocktestPartQuestionnaires).ThenInclude(x => x.Questionnaire).ThenInclude(x => x.Questions)
                                                    .Where(x => x.MocktestSectionId == mockTestSectionId)
                                                    .AsNoTracking()
                                                    .Select(x => new
                                                    {
                                                        x.Id,
                                                        x.Name,
                                                        x.SortOrder,
                                                        IsMarkByAI = x.IsMarkByAi,
                                                        x.DelayTimeEachQuestion,
                                                        x.TotalPartTime,
                                                        MocktestPartQuestionnaires = x.MocktestPartQuestionnaires.Select(q => new
                                                        {
                                                            QuestionnaireType = (EQuestionnaireType)q.Questionnaire.Type,
                                                            TotalTime = q.TotalTime ?? q.Questionnaire.TotalTime,
                                                            LeftSections = q.Questionnaire.LeftSections.Select(c => new
                                                            {
                                                                c.Id,
                                                                c.AudioDuration,
                                                                c.SpaceTime,
                                                                c.AudioFileId,
                                                                c.VideoId,
                                                            }),
                                                            Questions = q.Questionnaire.Questions.Select(c => new
                                                            {
                                                                c.Id,
                                                                c.AudioDuration,
                                                                c.SpaceTime,
                                                                c.RecordingTime,
                                                                c.AudioFileId,
                                                                c.SampleTemplateJsonObject

                                                            })
                                                        })
                                                    }).ToListAsync();

        var aggregateMckTestPartFileIds = mktestParts.SelectMany(x =>
        {
            var leftsectionsFileIds = x.MocktestPartQuestionnaires.SelectMany(c =>
            {
                var audioIds = c.LeftSections.Where(x => x.AudioFileId.HasValue).Select(c1 => c1.AudioFileId.Value);
                var videoIds = c.LeftSections.Where(c1 => c1.VideoId.HasValue).Select(c1 => c1.VideoId.Value);
                return audioIds.Concat(videoIds);
            });

            var questionFileIds = x.MocktestPartQuestionnaires.SelectMany(c =>
            {
                var audioIds = c.Questions.Where(x => x.AudioFileId.HasValue).Select(c1 => c1.AudioFileId.Value);

                return audioIds;
            });

            return leftsectionsFileIds.Concat(questionFileIds);
        }).ToList();

        var fileDurationDict = await _fileService.GetFilesDuration(aggregateMckTestPartFileIds);

        var mocktestPartQuestionnaires = mktestParts.Select(x =>
        {
            int questionsTotalTime = 0;
            int leftSectionTotalTime = 0;
            int totalTime = 0;
            int questionsCount = x.MocktestPartQuestionnaires.Sum(x => x.Questions.Select(x => x.Id).Count());

            if (mktestSecType == EMockTestSectionType.RecordNonstop)
            {

                leftSectionTotalTime = x.MocktestPartQuestionnaires.Sum(x => x.LeftSections.Sum(c =>
            {
                double total = 0;
                if (c.AudioFileId.HasValue)
                {
                    total += fileDurationDict.GetValueOrDefault(c.AudioFileId.Value);
                }
                total += (c.SpaceTime?.TotalSeconds ?? default);

                return (int)total;
            }));

                questionsTotalTime = x.MocktestPartQuestionnaires.Sum(x => x.Questions.Sum(q =>
                {
                    double total = 0;
                    if (q.AudioFileId.HasValue)
                    {
                        total += fileDurationDict.GetValueOrDefault(q.AudioFileId.Value);
                    }
                    total += (q.SpaceTime?.TotalSeconds ?? default);
                    total += (q.RecordingTime?.TotalSeconds ?? default);

                    var sampleTemplates = q.SampleTemplateJsonObject.ConvertSampleTemplate<SampleTemplateViewModel>();

                    if (sampleTemplates?.Any() == true)
                    {
                        total += sampleTemplates.Sum(t =>
                        {
                            int total = 0;
                            if (t.AudioFileId.HasValue)
                            {
                                total += fileDurationDict.TryGetValue(t.AudioFileId.Value, out var time) ? time : 0;
                            };
                            return total;
                        });
                    }

                    return (int)total;
                }));

                int totalDelayBetweenQuestions = (x.DelayTimeEachQuestion ?? default) * questionsCount;
                totalTime = leftSectionTotalTime + totalDelayBetweenQuestions + questionsTotalTime;
            }

            else if (mktestSecType == EMockTestSectionType.WritingNonstop) // WRITING NONSTOP
            {
                if (x.TotalPartTime.HasValue) // Cài tổng thời gian
                {
                    totalTime = (int)x.TotalPartTime?.TotalSeconds + (x.DelayTimeEachQuestion ?? default);
                }
                else   // KHÔNG cài tổng thời gian
                {
                    questionsTotalTime = x.MocktestPartQuestionnaires.Sum(q =>
                    {
                        int total = 0;
                        if (q.TotalTime.HasValue)
                        {
                            total += (int)q.TotalTime?.TotalSeconds * q.Questions.Count();
                        }

                        return total;
                    });

                    totalTime = questionsTotalTime + (x.DelayTimeEachQuestion ?? default);
                }
            }

            return new MockTestPartInfoDto
            {
                Id = x.Id,
                IsMarkByAI = x.IsMarkByAI,
                Name = x.Name,
                NumOfQuestions = questionsCount,
                NumOfTime = totalTime,
                SortOrder = x.SortOrder,
                TotalPartTime = x.TotalPartTime,
            };
        });

        return mocktestPartQuestionnaires.OrderBy(x => x.SortOrder).ToList();
    }
}