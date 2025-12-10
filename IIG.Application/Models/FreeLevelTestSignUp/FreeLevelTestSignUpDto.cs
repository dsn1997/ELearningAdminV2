using AutoMapper.Configuration.Annotations;
using IIG.Core.Common.Enums;
using IIG.Core.Common.Models.Files;
using System.ComponentModel.DataAnnotations;

namespace IIG.Web.Data.Models;

public class WebUserRegisterDto
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Phone number is required")]
    public string PhoneNumber { get; set; }
    public string PhoneNumberOther { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Email isn't in valid format")]
    public string Email { get; set; }
    public string Target { get; set; }

    public Guid? MockTestId { get; set; }

    [Ignore]
    public EWebUserRegisterType RegisterType { get; set; } // EWebUserRegisterType
    public string ClassName { get; set; }
    public string ProgramName { get; set; }


}


