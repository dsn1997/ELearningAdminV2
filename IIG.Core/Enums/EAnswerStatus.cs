using System.ComponentModel;

namespace IIG.Core.Common.Enums;

public enum EAnswerStatus
{
    [Description("Chưa làm")] 
    NotAnswered = 1,

    [Description("Sai")] 
    InCorrect = 2,

    [Description("Đúng")] 
    Correct = 3
}