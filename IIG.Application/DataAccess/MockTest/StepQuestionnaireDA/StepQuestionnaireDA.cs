using IIG.Application.Models.StepQuestionnaires;
using IIG.Core.Base;
using IIG.Core.Entities;
using IIG.Core.Helper;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace IIG.Application.Data;

public class StepQuestionnaireDA : IStepQuestionnaireDA
{
    private readonly IAppFactory _appFactory;

    public StepQuestionnaireDA(IAppFactory appFactory)
    {
        _appFactory = appFactory;
    }

    public async Task<StepQuestionnaireDto> GetInfo(Guid stepId, Guid questionnaireId)
    {
        var result = await _appFactory.Repository<StepQuestionnaire>().GetAll().Where(p => p.StepId == stepId && p.QuestionnaireId == questionnaireId).Select(p => new
        {
            CourseId = p.CourseId,
            StepId = p.StepId,
            QuestionnaireId = p.QuestionnaireId,
            SortOrder = p.SortOrder ?? 0,
            LessonId = p.LessonId,
        }).FirstOrDefaultAsync();

        var resultDto = new StepQuestionnaireDto
        {
            CourseId = result?.CourseId ?? Guid.Empty,
            StepId = result?.StepId ?? Guid.Empty,
            QuestionnaireId = result?.QuestionnaireId ?? Guid.Empty,
            SortOrder = result?.SortOrder ?? 0,
        };
        if (result != null)
        {
            if (result.CourseId == Guid.Empty)
            {
                var courseId = (await _appFactory.Repository<Lesson>().GetAll().Include(x => x.Unit)
                    .ThenInclude(x => x.Course)
                    .Where(x => x.Id == result.LessonId).FirstOrDefaultAsync())?.Unit?.Course.Id;
                if (courseId != null)
                {
                    resultDto.CourseId = (Guid)courseId;
                   await  _appFactory.Repository<StepQuestionnaire>().ExecuteUpdateAsync(p => p.StepId == stepId && p.QuestionnaireId == questionnaireId, 
                                                                                   setters => setters.SetProperty(x => x.CourseId, (Guid)courseId)
                                                                                   );
                }
            }
            return resultDto;
        }
        return null;
    }

    public async Task<IEnumerable<StepQuestionnaireDto>> GetListByStepIdAsync(Guid stepId)
    {
        var resultDto = await _appFactory.Repository<StepQuestionnaire>().GetAll().Where(p => p.StepId == stepId).Select(p => new StepQuestionnaireDto
        {
            CourseId = p.CourseId,
            StepId = p.StepId,
            QuestionnaireId = p.QuestionnaireId,
            SortOrder = p.SortOrder ?? 0,
        }).ToListAsync();
        return resultDto;
    }
}