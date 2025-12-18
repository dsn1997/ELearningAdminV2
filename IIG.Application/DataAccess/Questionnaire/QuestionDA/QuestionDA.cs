using IIG.Application.Models.Questionnaires;
using IIG.Application.Models.Versioning;
using IIG.Core.Base;
using IIG.Core.Common.Enums;
using IIG.Core.Common.Models.SpeakingAndWriting;
using IIG.Core.Entities;
using IIG.Core.Helper;
using IIG.Core.Repository;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data;

namespace IIG.Application.Data;
public class QuestionDA : IQuestionDA
{
    private readonly IAppFactory _appFactory;
    private readonly IRepository<Question> _questionRepos;
    private readonly IRepository<QuestionTranslation> _questionTranslationRepos;

    public QuestionDA(IAppFactory appFactory,
                      IRepository<Question> questionRepos,
                      IRepository<QuestionTranslation> questionTranslationRepos
        )
    {
        _appFactory = appFactory;
        _questionRepos = questionRepos;
        _questionTranslationRepos = questionTranslationRepos;
    }

    public async Task<IEnumerable<QuestionVersionInfo>> GetListVersionInfoBySubmittedDate(Guid questionnaireId, DateTime submittedDate)
    {
        var dtos = await _questionRepos.GetAll().Where(p => p.QuestionnaireId == questionnaireId).Select(p => new QuestionVersionInfo
        {
            QuestionId = p.Id,
            Name = p.Name,
            QuestionnaireId = p.QuestionnaireId,
            SortOrder = p.SortOrder ?? 0,
            Explanation = p.Explanation,
            Note = p.Note,
            FakeValues = p.FakeValues,
            DisplayTimestamp = p.DisplayTimestamp,
            ImageFileId = p.ImageFileId,
            WordEnglish = p.WordEnglish,
            TypeOfWord = (EQuestionTypeOfWord?)p.TypeOfWord,
            WordPhonetic = p.WordPhonetic,
            WordFileId = p.WordFileId,
            TextFileId = p.TextFileId,
            TextEnglish = p.TextEnglish,
            MatchingJson = p.MatchingJson,
            SampleTemplateJsonObject = p.SampleTemplateJsonObject,
            AudioFileId = p.AudioFileId,
        }).ToListAsync();
        return dtos;
    }

    public async Task<IEnumerable<QuestionTranslationDto>> GetListTranslationByListQuestionIdAndAndSubmittedDateAsync(IEnumerable<Guid> questionIds, DateTime submittedDate)
    {
        var dtos = await _questionTranslationRepos.GetAll().Where(p => questionIds.Contains(p.QuestionId)).Select(p => new QuestionTranslationDto
        {
            LanguageCode = p.LanguageCode,
            QuestionId = p.QuestionId,
            WordTranslation = p.WordTranslation
        }).ToListAsync();
        return dtos;
    }

    public async Task<FlashCardQuestionModel> GetFlashCardQuestionInfoByIdAsync(Guid id)
    {
        var dto = await _questionRepos.GetAll().Where(p => p.Id == id).Select(p => new FlashCardQuestionModel
        {
            Id = p.Id,
            WordEnglish = p.WordEnglish,
            TypeOfWord = (EQuestionTypeOfWord?)p.TypeOfWord,
            WordPhonetic = p.WordPhonetic,
            WordFileId = p.WordFileId,
            TextEnglish = p.TextEnglish,
            TextFileId = p.TextFileId,
        }).FirstOrDefaultAsync();
        return dto;
    }

    public async Task<IEnumerable<QuestionTranslationDto>> GetListTranslationByQuestionIdAsync(Guid questionId)
    {
        var dtos = await _questionTranslationRepos.GetAll().Where(p => p.QuestionId == questionId).Select(p => new QuestionTranslationDto
        {
            LanguageCode = p.LanguageCode,
            QuestionId = p.QuestionId,
            WordTranslation = p.WordTranslation
        }).ToListAsync();
        return dtos;
    }

    public async Task<IEnumerable<QuestionDto>> GetListQuestionByQuestionnaireIdAsync(Guid questionnaireId)
    {
        var questions = await _questionRepos.GetAll().Where(p => p.QuestionnaireId == questionnaireId).Select(p => new QuestionDto
        {
            Id = p.Id.ToString(),
            Name = p.Name,
            QuestionnaireId = p.QuestionnaireId,
            SortOrder = p.SortOrder,
            Explanation = p.Explanation,
            Note = p.Note,
            FakeValues = p.FakeValues,
            DisplayTimestamp = p.DisplayTimestamp,
            ImageFileId = p.ImageFileId,
            WordEnglish = p.WordEnglish,
            TypeOfWord = (EQuestionTypeOfWord?)p.TypeOfWord,
            WordPhonetic = p.WordPhonetic,
            WordFileId = p.WordFileId,
        }).ToListAsync();
        questions = questions.Select(p =>
        {
            if (!string.IsNullOrEmpty(p.SampleTemplateJsonObject))
                p.SampleTemplateViewModels = JsonConvert.DeserializeObject<List<SampleTemplateViewModel>>(p.SampleTemplateJsonObject);
            return p;
        }).ToList();
        return questions;
    }

    public async Task<IEnumerable<QuestionTranslationDto>> GetListTranslationByListQuestionIdAsync(IEnumerable<Guid> questionIds)
    {
        var dtos = await _questionTranslationRepos.GetAll().Where(p => questionIds.Contains(p.QuestionId)).Select(p => new QuestionTranslationDto
        {
            LanguageCode = p.LanguageCode,
            QuestionId = p.QuestionId,
            WordTranslation = p.WordTranslation
        }).ToListAsync();
        return dtos;
    }
}
