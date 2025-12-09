using System.ComponentModel;

namespace IIG.Core.Common.Enums;

/// <summary>
/// Default column sort is the first member of enum
/// </summary>

public enum EQuestionTypeOfWord : short
{
    [Description("Noun")]
    Noun = 1,

    [Description("Verb")]
    Verb,
    
    [Description("Adjective")]
    Adjective,

    [Description("Adverb")]
    Adverb,

    [Description("Noun phrase")]
    NounPhrase,

    [Description("Phrasal verb")]
    PhrasalVerb,

    [Description("Adjective phrase")]
    AdjectivePhrase,

    [Description("Idiom")]
    Idiom,
}