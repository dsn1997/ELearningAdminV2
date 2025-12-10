namespace IIG.Web.Data.Models.News;

public class NewsInfoDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Url { get; set; }

    public string Description { get; set; }

    public Guid CategoryId { get; set; }

    public string Content { get; set; }

    public string MetaTitle { get; set; }

    public string MetaKeywords { get; set; }

    public string MetaDescription { get; set; }

    public bool IsActive { get; set; }

    public bool IsHot { get; set; }

    public bool IsHome { get; set; }

    public int SortOrder { get; set; }

    public string Author { get; set; }

    public DateTime? Created { get; set; }

    public DateTime? Modified { get; set; }
    public Guid? ImageFileId { get; set; }
    public string ImageUrl { get; set; }
}