

using IIG.Core.Common.Enums;
using System.Text.Json.Serialization;

namespace IIG.Application.Models.KeyCodes;

public class MockTestSectionResultDto
{
    public Guid SectionId { get; set; }
    public string SectionName { get; set; }

    public EMockTestSectionType SectionType { get; set; }

    [JsonIgnore]
    public int ExactScore { get; set; }

    public string Score { get; set; }

    public int MinScore { get; set; }

    public int MaxScore { get; set; }

    public int FromScore { get; set; }

    public int ToScore { get; set; }

    public string Comment { get; set; }
    public Guid? RankingScoreId { get; set; }

    [JsonIgnore]
    public int SortOrder { get; set; }

}