using System.Text.Json.Serialization;

namespace IIG.Web.Data.Models.Practices.Dictionary;
public class MyDictionaryTranslationDto : BaseConcreteTranslation
{
    public Guid DictionaryId { get; set; }

    public string WordTranslation { get; set; }
}