using System.Text.Json.Serialization;

namespace IIG.Application.Models.Dictionary;
public class MyDictionaryTranslationDto : BaseConcreteTranslation
{
    public Guid DictionaryId { get; set; }

    public string WordTranslation { get; set; }
}