namespace IIG.Core.Common.Models.SpeakingAndWriting
{
    public interface IAudioFileProperty
    {
        Guid? AudioFileId { get; set; }
    }

    public interface IAudioFileDuration
    {
        long? AudioDuration { get; set; }
    }
}
