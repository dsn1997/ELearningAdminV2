using IIG.Core.Common.Models.Files;
using System.ComponentModel.DataAnnotations;

namespace IIG.Web.Data.Models;
public class TStudentReviewDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string AvartarFileUrl { get; set; }
    public string BackgroundFileUrl { get; set; }
    public string ImageFileUrl { get; set; }
    public int? Platform { get; set; }
    public string PlatformName { get; set; }
    public string Content { get; set; }
    public DateTime? Date { get; set; }
}
