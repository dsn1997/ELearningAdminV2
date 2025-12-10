using IIG.Core.Common.Enums;

namespace IIG.Web.Data.Models.ExamTools.AccountBank;

public class AccountBankListModel
{
    public Guid Id { get; set; }
  
    public string UserName { get; set; }

    public string Password { get; set; }

    public string Keycode { get; set; }
    
    public Guid ExtCategoryId { get; set; }
   
    public int LearningDuration { get; set; }
   
    public EAccountBankStatus Status { get; set; }
  
    public DateTime Created { get; set; }
}