
using IIG.Application.Models.Questionnaires;
using IIG.Application.Models.UnitTest;
using IIG.Application.Models.Versioning;
using IIG.Core.Base;
using IIG.Core.Common.Enums;
using IIG.Core.Entities;
using IIG.Core.Helper;
using IIG.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace IIG.Application.Data;
public class QuestionnaireDA : IQuestionnaireDA
{
    private readonly IAppFactory _appFactory;
    private readonly IRepository<Questionnaire> _questionnaireRepos;
    private readonly IRepository<Question> _questionRepos;
    private readonly IRepository<Answer> _answerRepos;

    public QuestionnaireDA(IAppFactory appFactory,
                           IRepository<Questionnaire> questionnaireRepos,
                           IRepository<Question> questionRepos,
                           IRepository<Answer> answerRepos
        )
    {
        _appFactory = appFactory;
        _questionnaireRepos = questionnaireRepos;
        _questionRepos = questionRepos;
        _answerRepos = answerRepos;
    }

    public async Task<IEnumerable<UnitTestVersionMenuWithStatusListModel>> GetMenuVersionByListQuestionnaireIdAsync(List<Guid> questionnaireIds, DateTime submittedDate)
    {
        var questionnaireDatas = await _questionnaireRepos.GetAll().Where(p => questionnaireIds.Contains(p.Id)).Select(p => new
        {
            p.Id,
            p.Type,
        }).ToListAsync();
        var questionDataDictionary = questionnaireDatas.ToDictionary(p => p.Id, p => (EQuestionnaireType)p.Type);

        //get question
        var listQuestionnaireTypeQuestion = new List<EQuestionnaireType>
        {
            EQuestionnaireType.MCQ,
            EQuestionnaireType.MCQImage,
            EQuestionnaireType.TrueFalse,
            EQuestionnaireType.Record,
            EQuestionnaireType.ReadTextALoud,
            EQuestionnaireType.Writing,

        };
        var queryQuestion = from q in _questionRepos.GetAll()
                            where questionnaireDatas.Where(p => listQuestionnaireTypeQuestion.Contains((EQuestionnaireType)p.Type)).Select(p => p.Id).Contains(q.QuestionnaireId)
                            select new UnitTestVersionMenuWithStatusListModel
                            {
                                Id = q.Id,
                                QuestionnaireId = q.QuestionnaireId,
                                SortOrder = q.SortOrder ?? 0,
                                QuestionSortOrder = q.SortOrder ?? 0,
                                QuestionId = q.Id,
                            };
        var questions = await queryQuestion.ToListAsync();

        //get answer
        var listQuestionnaireTypeAnswer = new List<EQuestionnaireType>
        {
            EQuestionnaireType.ImageDragDrop,
            EQuestionnaireType.Droplist,
            EQuestionnaireType.FillInTheBlank,
            EQuestionnaireType.Matching,
            EQuestionnaireType.MatchingImage,

        };
        var queryAnswer = from a in _answerRepos.GetAll()
                          where questionnaireDatas.Where(p => listQuestionnaireTypeAnswer.Contains((EQuestionnaireType)p.Type)).Select(p => p.Id).Contains(a.Question.QuestionnaireId)
                          select new UnitTestVersionMenuWithStatusListModel
                          {
                              Id = a.Id,
                              QuestionnaireId = a.Question.QuestionnaireId,
                              SortOrder = a.SortOrder ?? 0,
                              QuestionSortOrder = a.Question.SortOrder ?? 0,
                              QuestionId = a.QuestionId,
                          };


        var answers = await queryAnswer.ToListAsync();

        var output = questions.Union(answers);
        output = output.Select(p =>
        {
            p.QuestionnaireType = questionDataDictionary.ContainsKey(p.QuestionnaireId) ? questionDataDictionary.GetValueOrDefault(p.QuestionnaireId) : EQuestionnaireType.MCQImage;
            return p;
        }).ToList();
        return output;
    }

    public async Task<QuestionnaireVersionInfo> GetVersionInfoBySubmittedDate(Guid questionnaireId, DateTime submittedDate)
    {
        var dto = await _questionnaireRepos.GetAll().Where(p => p.Id == questionnaireId).Select(p => new QuestionnaireVersionInfo
        {
            QuestionnaireId = p.Id,
            Name = p.Name,
            Type = (EQuestionnaireType)p.Type,
            GroupId = p.QuestionnaireGroupId,
            TitleLeftSection = p.TitleLeftSection,
            VideoFileId = p.VideoFileId,
            SubtitleFileId = p.SubtitleFileId,
            SlideFileId = p.SlideFileId,
            IntroContent = p.IntroContent,
            IsActive = p.IsActive,
            Identifier = p.Identifier
        }).FirstOrDefaultAsync();
        return dto;
    }

    public async Task<QuestionnaireModel> GetByIdAsync(Guid id)
    {
        var dto = await _questionnaireRepos.GetAll().Where(p => p.Id == id).Select(p => new QuestionnaireModel
        {
            Id = p.Id,
            Name = p.Name,
            Type = (EQuestionnaireType)p.Type,
            GroupId = p.QuestionnaireGroupId,
            TitleLeftSection = p.TitleLeftSection,
            VideoFileId = p.VideoFileId,
            SubtitleFileId = p.SubtitleFileId,
            SlideFileId = p.SlideFileId,
            IntroContent = p.IntroContent,
            IsActive = p.IsActive,
        }).FirstOrDefaultAsync();
        return dto;
    }
}
