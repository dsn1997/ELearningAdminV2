using IIG.Application.Models.MockTestPart;
using IIG.Application.Models.Questionnaires;
using IIG.Core.Base;
using IIG.Core.Common.Enums;
using IIG.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace IIG.Application.Data;

public class MockTestPartDA : IMockTestPartDA
{
    private readonly IAppFactory _appFactory;
    private readonly IMockTestSectionDA _mockTestSectionDA;

    public MockTestPartDA(
                          IAppFactory appFactory,
                          IMockTestSectionDA mockTestSectionDA)
    {
        _appFactory = appFactory;
        _mockTestSectionDA = mockTestSectionDA;
    }

    public async Task<IEnumerable<MockTestPartInfoDto>> GetListByMockTestSectionIdAsync(Guid mockTestSectionId, DateTime? publishedAt = null)
    {

        var mocktestSectionTypeDB = await _appFactory.Repository<MocktestSection>().GetAll().Where(x => x.Id == mockTestSectionId).Select(p => p.Type).FirstOrDefaultAsync();
        var mocktestSectionType = (EMockTestSectionType)mocktestSectionTypeDB;

        //default
        if (mocktestSectionType == EMockTestSectionType.RecordNonstop || mocktestSectionType == EMockTestSectionType.WritingNonstop)
        {
            return await _mockTestSectionDA.GetListMocktespartsOfSWType(mockTestSectionId);
        }

        var query = from mockTestPart in _appFactory.Repository<MocktestPart>().GetAll()
                    where mockTestPart.MocktestSectionId == mockTestSectionId
                    select new MockTestPartInfoDto
                    {
                        Id = mockTestPart.Id,
                        Name = mockTestPart.Name,
                        SortOrder = mockTestPart.SortOrder,
                        DelayTimeEachQuestion = mockTestPart.DelayTimeEachQuestion ?? 0,
                        IsMarkByAI = mockTestPart.IsMarkByAi,
                    };
        var parts = await query.AsNoTracking().OrderBy(x => x.SortOrder).ToListAsync();

        if (!parts.Any())
            return parts;

        var partIds = parts.Select(x => x.Id).ToList();

        // tính total part time và num of questions
        var queryPartQuestionnaire = from mtpq in _appFactory.Repository<MocktestPartQuestionnaire>().GetAll()
                                     join r_questionnaire in _appFactory.Repository<QuestionnaireQuestionView>().GetAll()
                                     on mtpq.QuestionnaireId equals r_questionnaire.QuestionnaireId into rqg
                                     from questinnaire in rqg.DefaultIfEmpty()
                                     where partIds.Contains(mtpq.MocktestPartId)
                                     select new
                                     {
                                         MockTestPartId = mtpq.MocktestPartId,
                                         QuestionnaireId = mtpq.QuestionnaireId,
                                         QuestionCount = questinnaire.NumberOfQuestions
                                     };
        var partQuestionnaires = await queryPartQuestionnaire.AsNoTracking().ToListAsync();

        //Group question count theo part

        var questionCountByPart = partQuestionnaires
        .GroupBy(x => x.MockTestPartId)
        .ToDictionary(
            g => g.Key,
            g => g.Sum(x => x.QuestionCount ?? 0)
        );

        foreach (var part in parts)
        {
            part.NumOfQuestions = questionCountByPart.GetValueOrDefault(part.Id);
        }

        if (mocktestSectionType != EMockTestSectionType.NonStop)
        {
            parts.ForEach(p => p.NumOfTime = 0);
            return parts;
        }

        var audioDurationByQuestionnaire = await (from ls in _appFactory.Repository<LeftSection>().GetAll()
                                                  join f in _appFactory.Repository<Core.Entities.File>().GetAll() on ls.AudioFileId equals f.Id
                                                  where partQuestionnaires.Select(x => x.QuestionnaireId).Contains(ls.QuestionnaireId)
                                                  group f by ls.QuestionnaireId into g
                                                  select new
                                                  {
                                                     QuestionnaireId = g.Key,
                                                     TotalDuration = g.Sum(x => x.AudioDuration ?? 0)
                                                  }).ToDictionaryAsync(x => x.QuestionnaireId, x => x.TotalDuration);
        var questionnaireByPart = partQuestionnaires
       .GroupBy(x => x.MockTestPartId)
       .ToDictionary(g => g.Key, g => g.ToList());

        foreach (var part in parts)
        {
            if (!questionnaireByPart.TryGetValue(part.Id, out var qList))
                continue;

            var audioTime = (int)qList.Sum(q => audioDurationByQuestionnaire.GetValueOrDefault(q.QuestionnaireId));

            part.NumOfTime = audioTime +  (part.DelayTimeEachQuestion * qList.Count);
        }

        return parts;
    }

    public async Task<MockTestPartDetailDto> GetDetailById(Guid id, DateTime? publishedAt = null)
    {
        var query = from mtp in _appFactory.Repository<MocktestPart>().GetAll()
                    where mtp.Id == id
                    select new MockTestPartDetailDto
                    {
                        Id = mtp.Id,
                        Name = mtp.Name,
                        SortOrder = mtp.SortOrder,
                        DelayTimeEachQuestion = mtp.DelayTimeEachQuestion ?? 0,
                        Content = mtp.Content,
                        IntroAudioFileId = mtp.IntroAudioFileId,
                        MocktestSectionId= mtp.MocktestSectionId,
                    };
        var result = await query.AsNoTracking().FirstOrDefaultAsync();
        return result;
    }
}