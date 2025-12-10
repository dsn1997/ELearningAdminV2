using IIG.Core.Common.Enums;

namespace IIG.Application.Models;

public class CategoryListByAreaModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string NameNonAscii { get; set; }
    public string LinkUrl { get; set; }
    public int SortOrder { get; set; }
    public Guid? ParentId { get; set; }
    public ECategoryType CategoryType { get; set; }
    public int Level { get; set; }
    public string ImageUrl { get; set; }
}

public class CategoryMenuTopDetailDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string NameNonAscii { get; set; }
    public string LinkUrl { get; set; }
    public int SortOrder { get; set; }
    public Guid? ParentId { get; set; }
    public ECategoryType CategoryType { get; set; }
    public int Level { get; set; }
    public string ImageUrl { get; set; }
    public string Identifier { get; set; }
    public Guid? ImageFileId { get; set; }
    public string Value { get; set; }
    public int? PostType { get; set; }
    public string IntroductionPage { get; set; }
    public short ViewType { get; set; }
    public string LinkTreeUrl { get; set; }
    public List<CategoryMenuTopDetailDto> Children { get; set; } = new List<CategoryMenuTopDetailDto>();
    public IEnumerable<CategorySeftStudyProgramDto> SeftStudyPrograms { get; set; }
    public IEnumerable<CategoryCourseDto> Course { get; set; }

    public IEnumerable<CategoryDetailDto> Cad { get; set; } = new List<CategoryDetailDto>();
    public CategoryTranslationDto Ct { get; set; } = new CategoryTranslationDto();
}

public class CategoryTranslationDto
{
    public string Name { get; set; }
    public string SubName { get; set; }
    public string Description { get; set; }
}

public class CategoryFileDto
{

    public string FileName { get; set; }

    public string Extension { get; set; }

    public string StorageLocation { get; set; }

    public string DisplayName { get; set; }

    public Guid FileTypeId { get; set; }

    public string ThumbnailStorageLocation { get; set; }

    public string SmallStorageLocation { get; set; }

    public string MediumStorageLocation { get; set; }

    public string LargeStorageLocation { get; set; }

    public int? ImageOrder { get; set; }

    public long? AudioDuration { get; set; }
}



public class CategoryCourseDto
{
    public Guid CourseId { get; set; }
    public string Id { get { return CourseId.ToString().ToUpper(); } }

    public int? NumberOfRatings { get; set; }
    public double? AverageRating { get; set; }
    public decimal? Price { get; set; }
    public decimal? SalePrice { get; set; }
    public string LinkUrl { get; set; }

    public IEnumerable<CategoryCourseTranslationDto> NameTranslate { get; set; }

}

public class CategoryCourseTranslationDto
{
    public string LanguageCode { get; set; }
    public string Name { get; set; }
    public string Desciption { get; set; }
}

public class CategoryCourseStudyDto
{
    public Guid Id { get; set; }
    public string LinkUrl { get; set; }
}

public class CategorySeftStudyProgramDto
{
    public Guid Id { get; set; }
    public short ProgramType { get; set; }
}
public class CategoryMenuBottomDetailDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string NameNonAscii { get; set; }
    public string LinkUrl { get; set; }
    public string LinkTreeUrl { get; set; }
    public string IntroductionPage { get; set; }

    public int? SortOrder { get; set; }
    public Guid? ParentId { get; set; }
    public int Level { get; set; }
    public ECategoryType CategoryType { get; set; }
    public List<CategoryMenuBottomDetailDto> Children { get; set; } = new List<CategoryMenuBottomDetailDto>();

    public IEnumerable<CategoryDetailDto> Cad { get; set; } = new List<CategoryDetailDto>();

}