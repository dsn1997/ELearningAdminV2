using IIG.Core.Common.MongoDataModels.MockTests;
using IIG.Application.Models.KeyCodes;

namespace IIG.Application.Models;

public class WebUserCourseTestResultModel
{
    public Guid CourseTestId { get; set; }
    
    public Guid WebUserId { get; set; }
    
    public int TotalQuestion { get; set; }
    
    public int TotalCorrectAnswer { get; set; }
    
    public string RankingScore { get; set; }
    
    public DateTime? SubmittedDate { get; set; }    
    
    public IEnumerable<SectionCourseTestResultDto> Sections { get; set; }
    
    public MgMockTestMenuModel Menus { get; set; }
}

public class SectionCourseTestResultDto
{
    public Guid SectionId { get; set; }
   
    public string SectionName  { get; set; }
    
    public int ExactScore { get; set; }
    
    public string RankingScore { get; set; }
    
    public int MinScore { get; set; }
    
    public int MaxScore { get; set; }
}

public class CourseTestDeleteMongoDataInputDto
{
    public Guid CurrentUserId { get; set; }
    public Guid courseTestId { get; set; }
}