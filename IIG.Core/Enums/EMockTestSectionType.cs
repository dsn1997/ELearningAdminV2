using System.ComponentModel;

namespace IIG.Core.Common.Enums;

public enum EMockTestSectionType
{
    [Description("Non-Stop")]
    NonStop = 1,

    [Description("Freestyle")]
    FreeStyle = 2,

    [Description("Record Non-Stop")]
    RecordNonstop = 3,
    [Description("Writing Non-Stop")]
    WritingNonstop = 4,
}