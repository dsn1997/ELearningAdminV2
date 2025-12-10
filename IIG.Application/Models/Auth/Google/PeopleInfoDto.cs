using Newtonsoft.Json;

namespace IIG.Application.Models.Google;

public class PeopleInfoDto
{
    [JsonProperty("resourceName")] 
    public string ResourceName { get; set; }

    [JsonProperty("etag")] 
    public string Etag { get; set; }

    [JsonProperty("genders")] 
    public List<GenderDto> Genders { get; set; } = new();

    [JsonProperty("birthdays")] 
    public List<BirthdayDto> Birthdays { get; set; } = new();
}

public class BirthdayDto
{
    [JsonProperty("metadata")] 
    public MetadataDto MetadataDto { get; set; }

    [JsonProperty("date")] 
    public DateDto DateDto { get; set; }
}

public class DateDto
{
    [JsonProperty("year")] 
    public int? Year { get; set; }

    [JsonProperty("month")] 
    public int? Month { get; set; }

    [JsonProperty("day")] 
    public int? Day { get; set; }
}

public class MetadataDto
{
    [JsonProperty("primary")] 
    public bool? Primary { get; set; }

    [JsonProperty("source")] 
    public SourceDto SourceDto { get; set; }
}

public class SourceDto
{
    [JsonProperty("type")] 
    public string Type { get; set; }

    [JsonProperty("id")] 
    public string Id { get; set; }
}

public class GenderDto
{
    [JsonProperty("metadata")] 
    public MetadataDto MetadataDto { get; set; }

    [JsonProperty("value")] 
    public string Value { get; set; }

    [JsonProperty("formattedValue")] 
    public string FormattedValue { get; set; }
}