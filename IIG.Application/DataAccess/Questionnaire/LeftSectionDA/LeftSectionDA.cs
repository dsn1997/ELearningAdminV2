using IIG.Application.Models.LeftSection;
using IIG.Application.Models.Versioning;
using IIG.Core.Base;
using IIG.Core.Entities;
using IIG.Core.Helper;
using IIG.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace IIG.Application.Data;
public class LeftSectionDA : ILeftSectionDA
{
    private readonly IAppFactory _appFactory;
    private readonly IRepository<LeftSection> _leftSectionRepos;

    public LeftSectionDA(IAppFactory appFactory,
                        IRepository<LeftSection> leftSectionRepos)
    {
        _appFactory = appFactory;
        _leftSectionRepos = leftSectionRepos;
    }
   
    public async Task<IEnumerable<LeftSectionVersionInfo>> GetListVersionInfoBySubmittedDate(Guid questionnaireId, DateTime submittedDate)
    {
        var dtos = await _leftSectionRepos.GetAll().Where(p => p.QuestionnaireId == questionnaireId).OrderBy(p=>p.SortOrder).Select(p => new LeftSectionVersionInfo
        {
            Id = p.Id.ToString(),
            QuestionnaireId = p.QuestionnaireId,
            Title = p.Title,
            AudioFileId = p.AudioFileId,
            VideoId = p.VideoId,
            ImageFileId = p.ImageFileId,
            TextScript = p.TextScript,
            TextContent = p.TextContent,
        }).ToListAsync();
        return dtos;
    }

    public async Task<IEnumerable<LeftSectionModel>> GetListByQuestionnaierIdAsync(Guid questionnaireId)
    {
        var dtos = await _leftSectionRepos.GetAll().Where(p => p.QuestionnaireId == questionnaireId).OrderBy(p=>p.SortOrder).Select(p => new LeftSectionModel
        {
            Id = p.Id,
            QuestionnaireId = p.QuestionnaireId,
            SortOrder = p.SortOrder,
            Title = p.Title,
            AudioFileId = p.AudioFileId,
            ImageFileId = p.ImageFileId,
            TextScript = p.TextScript,
            TextContent = p.TextContent,
           SpaceTime = p.SpaceTime,
           VideoId = p.VideoId,       
        }).ToListAsync();
        return dtos;
    }

    public async Task<List<Guid>> GetListIdsByQuestionnaireIdAsync(Guid questionnaireId)
    {
        return await _leftSectionRepos.GetAll().Where(ls => ls.QuestionnaireId == questionnaireId)
            .OrderBy(ls => ls.SortOrder)
            .Select(ls => ls.Id).
            ToListAsync();
    }
}
