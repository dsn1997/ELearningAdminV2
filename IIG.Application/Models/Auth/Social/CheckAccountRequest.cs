namespace IIG.Application.Models.Social;

public class CheckAccountRequest
{
    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public string FullName { get; set; }

    public string GoogleId { get; set; }

    public string FacebookId { get; set; }

    public string Gender { get; set; }
   
    public DateTime? Birthday { get; set; }
   
    public string CurrentAddress { get; set; }
   
    public string JobName { get; set; }
}