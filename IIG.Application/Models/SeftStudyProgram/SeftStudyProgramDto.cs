using IIG.Core.Common.Enums;
using IIG.Application.Models;
using IIG.Application.Models.MockTest;

namespace IIG.Application.Models;

public class SeftStudyProgramDetailDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Tag1 { get; set; }
    public string Tag2 { get; set; }
    public string Tag3 { get; set; }
    public string Tags { get; set; }

    public string ImageFileUrl { get; set; }
    public string Code { get; set; }
    public int? NumberOfStudent { get; set; }
    public string SaleTitle { get; set; }
    public string SaleContent { get; set; }
    public DateTime? SaleEndtime { get; set; }
    public float? AvgRate { get; set; }
    public int? TotalReview { get; set; }
    public int? Status { get; set; }
    public short ProgramType { get; set; }
    public IEnumerable<SeftStudyProgramClassDto> Classes { get; set; }

}


public class SeftStudyProgramPagingInputDto : Pagination
{
    public List<string> Tags { get; set; }
    public ESeftStudyProgramType? Type { get; set; }
    public List<Guid> CategoryIds { get; set; }
}

public class SeftStudyProgramShortDto
{
    public Guid Id { get; set; }
    public Guid CategoryId { get; set; }
    public string Name { get; set; }
    public string NameNonAscii { get; set; }
    public string Tags { get; set; }
    public string ImageFileUrl { get; set; }
    public short Type { get; set; }
    public int? NumberOfStudent { get; set; }
    public float? AvgRate { get; set; }
    public int? TotalReview { get; set; }
    public decimal? Price { get; set; }
    public decimal? SalePrice { get; set; }
    public short ProgramType { get; set; }
    public string LinkUrl { get; set; }
    public CoursePriceShortDto CoursePriceMinDetail { get; set; }
   public IEnumerable<Guid> CourseIds { get; set; }
}



public class SeftStudyProgramClassDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public short? Type { get; set; }
    public bool? IsShowDescription { get; set; }
    public bool? IsFreeLearningUnit1Lesson1 { get; set; }
    public int? TotalCourseTest { get; set; }
    //public short Status { get; set; }
    //public Guid? ParentId { get; set; }
    //public Guid? SeftStudyProgramClassId { get; set; }
    public Guid? CoursePriceId { get; set; }
    public Guid? CourseId { get; set; }
    public decimal Price { get; set; }
    public decimal? SalePrice { get; set; }
    public DateTime? SaleValidFrom { get; set; }
    public DateTime? SaleValidTo { get; set; }

}

