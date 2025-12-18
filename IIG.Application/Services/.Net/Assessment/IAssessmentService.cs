using IIG.Application.Models;
using IIG.Application.Models.Assessment;
using IIG.Core.Common.MongoDataModels;


namespace IIG.Application.Services;

public interface IAssessmentService
{
    //Task<AssessmentResponse> CheckAnswerAsync(AssessmentRequest request);

    //Task<AssessmentResponse> CheckAnswerV2Async(AssessmentRequest request);

    //Task RedoAnswerAsync(AssessmentRedoRequest request);

    //Task RedoAnswerV2Async(AssessmentRedoRequest request);

    //Task<List<AssessmentSeeAnswerResponse>> SeeAnswerAsync(AssessmentSeeAnswerRequest request);

    IEnumerable<ChooseBaseModelResponse> VerifyAnswerAsync(IEnumerable<MgQuestionModel> questions, IEnumerable<ChooseBaseModelRequest> listChooseModel);

    //Task<MicrosoftPronunciationAssessmentResultDto> EvaluatePronunciationAsync(PronunciationPostRequest request);
}