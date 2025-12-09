using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IIG.Core.Common.Enums
{
    public enum EOrderTabMobile
    {
        [Description("Luyện tập")]
        Practice = 1,

        [Description("Thi thử")]
        MockTest = 2,

        [Description("Live class")]
        LiveClass = 3
    }
}
