using IIG.Web.Data.Models.LiveClassType;
using System.Text.Json.Serialization;

namespace IIG.Web.Data.Models.CoursePrice;

public class CoursePriceDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int NumberOfMonth { get; set; }
    public decimal? SalePrice { get; set; }
    public LiveClassTypeDetailModel ClassTypeInfo { get; set; }

    [JsonIgnore]
    public Guid? ClassTypeId { get; set; }
}

public class CoursePriceShortDto
{
    public Guid Id { get; set; }
    public decimal Price { get; set; }
    public decimal? SalePrice { get; set; }
   
}