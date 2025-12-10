using IIG.Core.Common.Models.Files;
using System.ComponentModel.DataAnnotations;

namespace IIG.Web.Data.Models;
public class Menu_TTeacherDto
{
    public Guid?  Id { get; set; }
    public string Name { get; set; }

    public string Position { get; set; }

    //public FileDataDto ImageFile { get; set; }
    public string ImageFileUrl { get; set; }

    public string Degree { get; set; }

    public string Experience { get; set; }

    public string Description { get; set; }
    public IEnumerable<Menu_TTeacherAchievementDto> Achievements { get; set; }
    public IEnumerable<Menu_TTeacherCertificateDto> Certificates { get; set; }
}


public class Menu_TTeacherAchievementDto
{
    public string Name { get; set; }
    public string ImageFileUrl { get; set; }
}
public class Menu_TTeacherCertificateDto
{
    public string Name { get; set; }
    public string ImageFileUrl { get; set; }

}


public class TTeacherDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Position { get; set; }
    public string Degree { get; set; }
    public string Experience { get; set; }
    public string Description { get; set; }
    public string ImageFileUrl { get; set; }

    public IEnumerable<TTeacherAchievementDto> Achievements { get; set; }
    public IEnumerable<TTeacherCertificateDto> Certificates { get; set; }
}

public class TTeacherAchievementDto
{
    public string Name { get; set; }
    public Guid? ImageFileId { get; set; }
    public string ImageFileUrl { get; set; }
}

public class TTeacherCertificateDto
{
    public string Name { get; set; }
    public Guid? ImageFileId { get; set; }
    public string ImageFileUrl { get; set; }
}