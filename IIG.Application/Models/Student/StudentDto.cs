using IIG.Core.Common.Models.Files;
using System.ComponentModel.DataAnnotations;

namespace IIG.Web.Data.Models;
public class Menu_TStudentDto
{
    public Guid?  Id { get; set; }
    public string Name { get; set; }

    //public FileDataDto ImageFile { get; set; }
    public string ImageFileUrl { get; set; }    
    public string Profression { get; set; }
    public string Exam { get; set; }
    public long? Result { get; set; }
}


public class TStudentDto
{
    public Guid? Id { get; set; }
    public string Name { get; set; }
    public string ImageFileUrl { get; set; }
    public string Profression { get; set; }
    public string Exam { get; set; }
    public long? Result { get; set; }
}