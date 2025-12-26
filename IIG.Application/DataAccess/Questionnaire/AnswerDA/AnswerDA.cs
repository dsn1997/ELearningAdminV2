using IIG.Application.Models.Questionnaires;
using IIG.Application.Models.Versioning;
using IIG.Core.Base;
using IIG.Core.Entities;
using IIG.Core.Helper;
using IIG.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace IIG.Application.Data;
public class AnswerDA : IAnswerDA
{
    private readonly IAppFactory _appFactory;
    private readonly IRepository<Answer> _answerRepos;

    public AnswerDA(IAppFactory appFactory,
                    IRepository<Answer> answerRepos
        )
    {
        _appFactory = appFactory;
        _answerRepos = answerRepos;
    }

    public async Task<IEnumerable<AnswerVersionInfo>> GetListVersionInfoBySubmittedDate(List<Guid> questionIds, DateTime submittedDate)
    {
        var query = from a in _answerRepos.GetAll()
                    where questionIds.Contains(a.QuestionId)
                    select new AnswerVersionInfo
                    {
                        Id = a.Id,
                        Name= a.Name,
                        QuestionId = a.QuestionId,
                        ImageFileId = a.ImageFileId,
                        CorrectMCQ = a.CorrectMcq,
                        MatchingKey = a.MatchingKey,
                        CorrectMatchingValues = a.CorrectMatchingValues,
                        FakeSelectValues = a.FakeSelectValues,
                        SortOrder = a.SortOrder,
                    };
        var results = await query.ToListAsync();
        return results;
    }


    public async Task<IEnumerable<AnswerDto>> GetListAnswerByListQuestionIdAsync(IEnumerable<Guid> questionIds)
    {
        var query = from a in _answerRepos.GetAll()
                    where questionIds.Contains(a.QuestionId)
                    select new AnswerDto
                    {
                        Id = a.Id,
                        Name = a.Name,
                        QuestionId = a.QuestionId,
                        ImageFileId = a.ImageFileId,
                        CorrectMCQ = a.CorrectMcq,
                        MatchingKey = a.MatchingKey,
                        CorrectMatchingValues = a.CorrectMatchingValues,
                        FakeSelectValues = a.FakeSelectValues,
                        SortOrder = a.SortOrder,
                        Created = a.Created,
                        Modified = a.Modified,
                    };
        var results = await query.ToListAsync();
        return results;
    }
}
