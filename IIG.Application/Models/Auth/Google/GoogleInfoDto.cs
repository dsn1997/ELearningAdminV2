using Newtonsoft.Json;

public class GoogleInfoDto
{
    [JsonProperty("sub")]
    public string Sub { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }
  
    [JsonProperty("given_name")]
    public string GivenName { get; set; }

    [JsonProperty("family_name")]
    public string FamilyName { get; set; }

    [JsonProperty("picture")]
    public string Picture { get; set; }

    [JsonProperty("email")]
    public string Email { get; set; }

    [JsonProperty("email_verified")]
    public bool EmailVerified { get; set; }

    [JsonProperty("locale")]
    public string Locale { get; set; }

    [JsonProperty("hd")]
    public string Hd { get; set; }
    
    public string Gender { get; set; }
    public string Birthday { get; set; }
}