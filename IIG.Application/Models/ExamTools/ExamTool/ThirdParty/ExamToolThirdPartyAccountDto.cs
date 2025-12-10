using IIG.Core.Common.Enums;

namespace IIG.Application.Models.ExamTool.ThirdParty;

public class ExamToolThirdPartyAccountDto
{
    public string KeyCode { get; set; }
    
    public string UserName { get; set; }
   
    public string Password { get; set; }
   
    public DateTime DateOfPurchase { get; set; }
    
    public EExamToolThirdPartyListAccount? Status { get; set; }
}