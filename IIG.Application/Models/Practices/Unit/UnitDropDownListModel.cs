using System.Text.Json.Serialization;

namespace IIG.Application.Models.Unit;
public class UnitDropDownListModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    [JsonIgnore]
    public int SortOrder { get; set; }
}
