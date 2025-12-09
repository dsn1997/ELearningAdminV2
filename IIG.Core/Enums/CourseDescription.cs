using System.ComponentModel;

namespace IIG.Core.Common.Enums;

public enum ECourseDescriptionType : short
{
    [Description("Quà tặng")]
    QUA_TANG = 0,

    [Description("Danh sách")]
    DANH_SACH,

    [Description("Slider")]
    SLIDER,

    [Description("Nội dung và hình ảnh")]
    NOI_DUNG_VA_HINH_ANH,

    [Description("Giáo viên")]
    GIAO_VIEN,

    [Description("Thành tích học viên")]
    THANH_TICH_HOC_VIEN,

    [Description("Cảm nhận học viên")]
    CAM_NHAN_HOC_VIEN,

    [Description("Câu hỏi thường gặp")]
    CAU_HOI_THUONG_GAP,

    [Description("Chương trình học")]
    CHUONG_TRINH_HOC,

    [Description("Khóa học liên quan")]
    KHOA_HOC_LIEN_QUAN,

    [Description("Mô tả khác")]
    MO_TA_KHAC,
}