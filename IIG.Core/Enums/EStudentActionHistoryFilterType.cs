using System.ComponentModel;

namespace IIG.Core.Common.Enums
{
    public static class StudentActionHistoryFilterType
    {   
        [Description("Thời gian")]
        public const string CreatedAt = "CreatedAt";
        [Description("Họ và tên")]
        public const string FullName = "FullName";

        [Description("Số điện thoại")]
        public const string PhoneNumber = "PhoneNumber";

        [Description("Email")]
        public const string Email = "Email";

        [Description("Tên sản phẩm")]
        public const string ProductName = "ProductName";

        [Description("Người tạo")]
        public const string CreatedBy = "CreatedBy";
    }
}