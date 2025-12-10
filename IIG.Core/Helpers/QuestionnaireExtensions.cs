using IIG.Core.Common.Enums;
using System.Text.RegularExpressions;

namespace IIG.Core.Helpers;

public static class QuestionnaireExtensions
{
    public static EQuestionTypeOfWord TryGetQuestionTypeOfWord(this string questionTypeOfWordStr)
    {
        try
        {
            return questionTypeOfWordStr.GetValueFromDescription<EQuestionTypeOfWord>();
        }
        catch (Exception)
        {
            return EQuestionTypeOfWord.Noun;
        }
    }

    public static int CountAnswerFromText(this string input)
    {
        try
        {
            return Regex.Matches(input, @"\(\d+\)").Count;
        }
        catch (Exception)
        {
            return 0;
        }
    }
}