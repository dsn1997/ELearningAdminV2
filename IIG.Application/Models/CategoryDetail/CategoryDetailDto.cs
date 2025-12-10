using IIG.Core.Common.Models.Files;
using IIG.Web.Data.Models.SeftStudyProgram;
using System.ComponentModel.DataAnnotations;

namespace IIG.Web.Data.Models;

public class Menu_CategoryDetailDto
{
    public Guid? Id { get; set; }
    public Guid CategoryId { get; set; }

    public string Title { get; set; }
    public string NameNonAscii { get; set; }
    public string MenuName { get; set; }

    public string SortDescription { get; set; }

    public string Description { get; set; }

    public int? SortOrder { get; set; }
    public string LogoFileUrl { get; set; }

    public string Tag { get; set; }
    public bool? ShowInPage { get; set; }

    public List<Menu_CategoryDetailSectionDto> Sections { get; set; }
}


public class Menu_CategoryDetailSectionDto
{
    public Guid? Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    public int? Type { get; set; }

    public int? SortOrder { get; set; }
}
public class CategoryDetailDto
{
    public Guid? Id { get; set; }
    public Guid CategoryId { get; set; }
    public int Level { get; set; }
    public int? CategoryType { get; set; }

    public string Title { get; set; }
    public string Name { get; set; }
    public string SubName { get; set; }
    public string NameNonAscii { get; set; }

    public string SortDescription { get; set; }

    public string Description { get; set; }

    public int? SortOrder { get; set; }
    public string LogoFileUrl { get; set; }

    public string Tag { get; set; }
    public bool? ShowInPage { get; set; }
    public IEnumerable<CategoryDetailSectionDto> CategoryDetailSections { get; set; }
    public IEnumerable<CategoryDetail_CategoryDto> CategoryDetail_CategoryInfos { get; set; }
    public IEnumerable<SeftStudyProgramShortDto> SeftStudyPrograms { get; set; }


}

public class CategoryDetail_CategoryDto
{
    public Guid Id { get; set; }
    public Guid? CategoryDetailId { get; set; }
    public string Name { get; set; }
    public string SubName { get; set; }
    public string NameNonAscii { get; set; }

    public Guid? ParentId { get; set; }
    public int Level { get; set; }
    public int? CategoryType { get; set; }
    public int? SortOrder { get; set; }

    public IEnumerable<CategoryDetail_CategoryDto> Childs { get; set; }
    public IEnumerable<SeftStudyProgramShortDto> SeftStudyPrograms { get; set; }

}

public class CategoryDetailSectionDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public Guid? BannerFileId { get; set; }
    public short? Status { get; set; }
    public decimal? SortOrder { get; set; }
    public string Content { get; set; }
    public string BannerFileUrl { get; set; }
}

