using IIG.Core.Common.Enums;
using IIG.Web.Data.Models.SeftStudyProgram;

namespace IIG.Web.Data.Models.Category;

public class CategoryListModel
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


