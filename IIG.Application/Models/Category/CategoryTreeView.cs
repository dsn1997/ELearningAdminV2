using IIG.Core.Common.Enums;

namespace IIG.Application.Models;

public class CategoryTreeView
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string NameNonAscii { get; set; }
    public string LinkUrl { get; set; }
    public int SortOrder { get; set; }
    public Guid? ParentId { get; set; }
    public ECategoryType CategoryType { get; set; }
    public int Level { get; set; }
    public List<CategoryTreeView> Children { get; set; } = new List<CategoryTreeView>();
}

public class CategoryShortDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string NameNonAscii { get; set; }
    public Guid? ParentId { get; set; }
}