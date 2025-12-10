using IIG.Core.Common.Models.Files;
using System.ComponentModel.DataAnnotations;

namespace IIG.Web.Data.Models;
public class Menu_MockTestWrapperGroupDto
{
    public Guid? Id { get; set; }
    public string Name { get; set; }
    public Guid? ParentId { get; set; }
    public bool? IsDailyChallenge { get; set; }
    public int? SortOrder { get; set; }
    public List<Menu_MockTestWrapperGroupDto> Children { get; set; }
}

