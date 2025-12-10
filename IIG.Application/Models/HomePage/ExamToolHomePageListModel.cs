namespace IIG.Web.Data.Models.HomePage;
public class ExamToolHomePageListModel
{
    public Guid Id { get; set; }

    public decimal Price { get; set; }

    public decimal SalePrice { get; set; }

    public string Code { get; set; }

    public string Name { get; set; }

    public string ImageUrl { get; set; }

    public int NumberOfRatings { get; set; }

    public decimal AverageRating { get; set; }
    public string NameNonAscii { get; set; }
}