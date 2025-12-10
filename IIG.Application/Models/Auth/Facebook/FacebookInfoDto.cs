using Newtonsoft.Json;

namespace IIG.Application.Models.Facebook;

public class FacebookInfoDto
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("email")]
    public string Email { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("picture")]
    public Picture Picture { get; set; }
    
    [JsonProperty("birthday")]
    public string Birthday { get; set; }
   
    [JsonProperty("gender")]
    public string Gender { get; set; }
}

public partial class Picture
{
    [JsonProperty("data")]
    public Data Data { get; set; }
}

public partial class Data
{
    [JsonProperty("height")]
    public long Height { get; set; }

    [JsonProperty("is_silhouette")]
    public bool IsSilhouette { get; set; }

    [JsonProperty("url")]
    public string Url { get; set; }

    [JsonProperty("width")]
    public long Width { get; set; }
}