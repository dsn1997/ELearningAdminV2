using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IIG.Core.Common.Enums
{
    public enum ESelfCourseActionHistoryStatusType
    {
        [Description("Mua mới")]
        NewBuy = 0,

        [Description("Gia hạn thêm")]
        FurtherExtension = 1,

        [Description("Bắt đầu bảo lưu")]
        StartReservation = 2,

        [Description("Kết thúc bảo lưu")]
        EndReservation = 3,

        [Description("Khóa")]
        Lock = 4,

        [Description("Mở khóa")]
        Unlock = 5,

        [Description("Reset")]
        Reset = 6
    }

}
