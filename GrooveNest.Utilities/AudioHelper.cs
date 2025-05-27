using NAudio.Wave;

namespace GrooveNest.Utilities
{
    public static class AudioHelper
    {
        public static int GetAudioDurationInSeconds(Stream audioStream)
        {
            if (audioStream.Position != 0)
                audioStream.Position = 0;

            try
            {
                using var reader = new Mp3FileReader(audioStream);
                return (int)reader.TotalTime.TotalSeconds;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to get audio duration.", ex);
            }
        }
    }
}
