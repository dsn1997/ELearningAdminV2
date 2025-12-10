namespace IIG.Application.Models
{
    public class WebUserInfoUpdateModel
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public DateTime? Birthday { get; set; }
        public string CurrentAddress { get; set; }
        public string JobName { get; set; }
    }
}