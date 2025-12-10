using IIG.Core.Common.Models.Files;
using System.ComponentModel.DataAnnotations;

namespace IIG.Web.Data.Models;

public class HomePageInfoDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public Guid? ImageFileId { get; set; }
    public string StudentId { get; set; }
    public string TeacherId { get; set; }
    public string Description { get; set; }
    public string LinkUrl { get; set; }
    public string ImageFileUrl { get; set; }
}


