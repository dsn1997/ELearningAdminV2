using IIG.Core.Common.Enums;

namespace IIG.Web.Data.Models.Courses;
public class CourseSuggestionDto
{
    public Guid Id { get; set; }
    public Guid CoursePriceId { get; set; }
    public decimal? Price { get; set; }
    public decimal? SalePrice { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public string ImageUrl { get; set; }
    public int NumberOfRatings { get; set; }
    public decimal AverageRating { get; set; }
    public EProductType ProductType { get; set; }
    public string NameNonAscii { get; set; }
}