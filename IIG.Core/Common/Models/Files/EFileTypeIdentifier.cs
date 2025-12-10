using System.ComponentModel;

namespace IIG.Core.Common.Models.Files
{
    public enum EFileTypeIdentifier
    {
        [Description("image of course")]
        ImageCourse = 1,
       
        [Description("avatar user")]
        ImageUser = 2,
        
        [Description("image course teacher")]
        ImageCourseTeacher = 3,
        
        [Description("image news")]
        ImageNews = 4,
       
        [Description("practice video")]
        PracticeVideo = 5,
        
        [Description("practice slide")]
        PracticeSlide = 6,
       
        [Description("practice subtitle file")]
        PracticeSubtitleFile = 7,
        
        [Description("practice images")]
        PracticeImages = 8,
        
        [Description("practice audio files")]
        PracticeAudioFiles = 9,

        [Description("mocktest audio files")]
        MockTestAudioFiles = 10,
        
        [Description("default image files")]
        DefaultImageFiles = 11,
        
        [Description("policy audio files")]
        PolicyAudioFiles = 12,

        [Description("mock test image files")]
        MockTestImage = 13
    }
}