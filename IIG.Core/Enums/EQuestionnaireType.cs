using System.ComponentModel;

namespace IIG.Core.Common.Enums
{
    public enum EQuestionnaireType: short
    {
        [Description("Video")]
        Video = 1,

        [Description("Slide")]
        Slide = 2,

        [Description("MCQ")]
        MCQ = 3,

        [Description("Drag and Drop")]
        ImageDragDrop = 4,

        [Description("Pronunciation Recognition")]
        PronunciationRecognition = 5,

        [Description("Drop list")]
        Droplist = 6,

        [Description("Flashcard")]
        FlashCard = 7,

        [Description("MCQ Photo")]
        MCQImage = 8,

        [Description("True, False")]
        TrueFalse = 9,

        [Description("Fill in the blank")]
        FillInTheBlank = 10,

        [Description("Matching word")]
        Matching = 11,

        [Description("Matching image")]
        MatchingImage = 12,

        [Description("Record")]
        Record = 13,

        [Description("Read a text aloud")]
        ReadTextALoud = 14,

        [Description("Writing")]
        Writing = 15
    }
}
