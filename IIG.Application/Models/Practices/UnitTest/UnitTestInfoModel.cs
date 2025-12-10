using IIG.Core.Common.Models.MockTests;
using Newtonsoft.Json;

namespace IIG.Web.Data.Models.Practices.UnitTest;
public class UnitTestInfoModel
    : IRedoSettingProperty
{
    public int TotalQuestions { get; set; }

    public int TotalSeconds { get; set; }

    public bool IsSubmitted { get; set; }

    [JsonIgnore]
    public string Title { get; set; }
    public int? RedoNumber { get; set; }
}
